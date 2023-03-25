using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float secondsToDie;
        [SerializeField] private float damageAmount;
        [SerializeField] private LayerMask layerMask;

        private void OnCollisionEnter(Collision collision)
        {
            if ((layerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                collision.transform.GetComponentInParent<PlayerController>().TakeDamage(damageAmount);
            }
            StartCoroutine(Coroutine_Death());
        }

        private IEnumerator Coroutine_Death()
        {
            yield return new WaitForSeconds(secondsToDie);
            Destroy(gameObject);
        }
    }
}
