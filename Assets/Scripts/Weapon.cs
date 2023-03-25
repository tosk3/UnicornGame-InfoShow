using System;
using UnityEngine;

[Serializable]
public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletAmountPerShot;
    [SerializeField] private float shootingForce;
    [SerializeField] private float shootForceVariation;
    [SerializeField] private float offsetValue;

    public bool CheckInput(Vector3 position)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(position);
            return true;
        }
        return false;
    }

    private void Shoot(Vector3 position)
    {
        Vector3 shootDir = (position - this.transform.position).normalized;

        for (int i = 0; i < bulletAmountPerShot; i++)
        {
            Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(0, offsetValue), UnityEngine.Random.Range(0, offsetValue), UnityEngine.Random.Range(0, offsetValue));
            GameObject bulletpf = Instantiate(bullet, this.transform.position+randomOffset, transform.parent.rotation); 
            bulletpf.GetComponent<Rigidbody>().AddForce(( shootDir +
                new Vector3(UnityEngine.Random.Range(0, shootDir.x),
                UnityEngine.Random.Range(0, shootDir.y),
                UnityEngine.Random.Range(0, shootDir.z))) 
                * (shootingForce + UnityEngine.Random.Range(0,shootForceVariation)), ForceMode.Impulse);
        }
    }
}
