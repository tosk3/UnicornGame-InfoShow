using System;
using UnityEngine;

[Serializable]
public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletAmountPerShot;
    [SerializeField] private float shootingForce;
    [SerializeField] private float shootForceRadius;
    [SerializeField] private float offsetValue;

    public void CheckInput(Vector3 position)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(position);
        }
    }

    private void Shoot(Vector3 position)
    {
        Vector3 shootDir = (this.transform.position - position).normalized;

        for (int i = 0; i < bulletAmountPerShot; i++)
        {
            Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(0, offsetValue), UnityEngine.Random.Range(0, offsetValue), UnityEngine.Random.Range(0, offsetValue));
            GameObject bulletpf = Instantiate(bullet, this.transform.position+randomOffset, transform.parent.rotation); 
            bulletpf.GetComponent<Rigidbody>().AddForce(-(shootDir + shootDir/5) * shootingForce, ForceMode.Impulse);
        }
    }
}
