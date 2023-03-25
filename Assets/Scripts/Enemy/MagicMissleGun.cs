using UnityEngine;

public class MagicMissleGun : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shootingForce;
    [SerializeField] private float shootTimer;
    [SerializeField] private float shootTimerMax;
    [SerializeField] private float shootForceVariation;
    [SerializeField] private float offsetValue;

    public void Shoot(Vector3 position)
    {
        Vector3 shootDir = (position - this.transform.position).normalized;

        Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(0, offsetValue), UnityEngine.Random.Range(0, offsetValue), UnityEngine.Random.Range(0, offsetValue));
        GameObject bulletpf = Instantiate(projectile, this.transform.position + randomOffset, transform.parent.rotation);
        bulletpf.GetComponent<Rigidbody>().AddForce((shootDir + shootDir / 5) * (shootingForce + UnityEngine.Random.Range(0, shootForceVariation)), ForceMode.Impulse);

    }
}
