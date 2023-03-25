using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 position;
    }

    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private Ray cameraRay;               
    [SerializeField] private RaycastHit cameraRayHit; 
    [SerializeField] private Vector3 right;
    [SerializeField] private Vector3 forward;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Weapon weapon;

    private void Update()
    {
        LookAtCamera();
        Move();
        if (weapon.CheckInput(targetPosition))
        {
            OnShoot?.Invoke(this, new OnShootEventArgs() { position = this.transform.position }) ;
        }
    }
    private void LookAtCamera()
    {
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {       
            if (cameraRayHit.transform.tag == "Ground")
            {      
                targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                transform.LookAt(targetPosition);
            }
        }
    }

    private void Move()
    {
        //Vector3 rightMovement = transform.right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        //Vector3 upMovement = transform.forward * speed * Time.deltaTime * Input.GetAxis("Vertical");
        //transform.position += rightMovement;
        //transform.position += upMovement;

        Vector3 rightMovement2 = right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement2 = forward * speed * Time.deltaTime * Input.GetAxis("Vertical");
        transform.position += rightMovement2;
        transform.position += upMovement2;

    }

    public void TakeDamage(float attackDamage)
    {
        health -=attackDamage;
    }
}
