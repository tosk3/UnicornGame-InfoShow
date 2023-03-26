using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer_UI : MonoBehaviour
{
    [SerializeField] private RectTransform ui_image;
    public LevelManager levelManager;

    private void Update()
    {    
        ui_image.transform.localScale = new Vector3(levelManager.TimerProgress(),1f);
    }
}
