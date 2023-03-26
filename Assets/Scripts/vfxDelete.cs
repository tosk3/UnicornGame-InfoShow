using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxDelete : MonoBehaviour
{
    public float secondsToDie;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Coroutine_Death());
    }

    private IEnumerator Coroutine_Death()
    {
        yield return new WaitForSeconds(secondsToDie);
        Destroy(gameObject);
    }
}
