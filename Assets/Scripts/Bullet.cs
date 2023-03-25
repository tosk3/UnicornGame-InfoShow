using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float secondsToDie;
    [SerializeField] private GameObject VFX_Pf;
    [SerializeField] private float damageAmount;
    [SerializeField] private LayerMask layerMask;

    private void OnCollisionEnter(Collision collision)
    {
        if ((layerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            collision.transform.GetComponentInParent<Enemy>().TakeDamage(damageAmount);
            Vector3 position = new Vector3(collision.transform.position.x, VFX_Pf.transform.position.y, collision.transform.position.z);
            Quaternion rot = UnityEngine.Random.rotation;
            rot.x = 0;
            rot.z = 0;
            GameObject VFXInstatiated = Instantiate(VFX_Pf, position, Quaternion.identity * rot);
        }
        StartCoroutine(Coroutine_Death());
    }

    private IEnumerator Coroutine_Death()
    {
        yield return new WaitForSeconds(secondsToDie);
        Destroy(gameObject);
    }
}
