using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth_UI : MonoBehaviour
{
    [SerializeField] private RectTransform ui_image;

    public void UpdateHealth(float healthPrecent)
    {
        ui_image.transform.localScale = new Vector3(healthPrecent, 1f);
    }

}
