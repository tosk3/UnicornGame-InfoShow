using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyCounter : MonoBehaviour
{

    [SerializeField] private List<GameObject> fairyAlive;
    [SerializeField] private List<GameObject> fairyDead;
    [SerializeField] private int count = 0;

    public void SwapIcons()
    {
        if (count > fairyAlive.Count)
        {
            fairyAlive[count].SetActive(false);
            fairyDead[count].SetActive(true);
            count++;
        }
       
    }
}
