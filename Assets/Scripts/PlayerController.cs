using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<string> speachLines;
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 position;
    }
    public event EventHandler<OnDeathArgs> OnDamageTaken;
    public class OnDeathArgs : EventArgs
    {
        public bool ded;
    }

    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float healthMax;
    [SerializeField] private Ray cameraRay;               
    [SerializeField] private RaycastHit cameraRayHit; 
    [SerializeField] private Vector3 right;
    [SerializeField] private Vector3 forward;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Weapon weapon;
    [SerializeField] private float shootTimer;
    [SerializeField] private float speachTimer;
    [SerializeField] private float bubbleTimer;
    [SerializeField] private float shootTimerMax;
    [SerializeField] private float speachTimerMax;
    [SerializeField] private float bubbleTimerMax;
    [SerializeField] private bool readyToShoot= false;
    [SerializeField] private bool readyToSpeak= false;
    [SerializeField] private TextMesh textObj;
    [SerializeField] private Vector3 localOffset = new Vector3(-5f, 10f, 0);
    [SerializeField] private bool play = true;



    private void Update()
    {
        if (play)
        {
            LookAtCamera();
            Move();
            if (RunTimer() || readyToShoot)
            {
                if (weapon.CheckInput(targetPosition))
                {
                    OnShoot?.Invoke(this, new OnShootEventArgs() { position = this.transform.position });
                    readyToShoot = false;
                }
            }
            if (RunSpeachTimer())
            {
                textObj = LevelManager.CreateWorldText(speachLines[UnityEngine.Random.Range(0, speachLines.Count)], this.transform.parent, localOffset, 12, Color.black);
                readyToSpeak = false;
            }
            if (textObj != null)
            {

                textObj.transform.position = this.transform.position + localOffset;
                textObj.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                Camera.main.transform.rotation * Vector3.up);

                if (RunBubbleTimer())
                {
                    Destroy(textObj);
                    textObj = null;
                }
            }
        }
       
    }
    public float GetHealthPrecent()
    {
        return health / healthMax;
    }

    public void EndGame()
    {
        play = false;
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
        Vector3 rightMovement2 = right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement2 = forward * speed * Time.deltaTime * Input.GetAxis("Vertical");
        transform.position += rightMovement2;
        transform.position += upMovement2;
    }

    public void TakeDamage(float attackDamage)
    {
        health -=attackDamage;
        OnDamageTaken?.Invoke(this, new OnDeathArgs() { ded = health < 0 ? true : false });

    }
    private bool RunTimer()
    {
        if (!readyToShoot)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootTimer = shootTimerMax;
                readyToShoot = true;
                return true;
            }
            return false;
        }
        return false;
        
    }
    private bool RunSpeachTimer()
    {
        if (!readyToSpeak)
        {
            speachTimer -= Time.deltaTime;
            if (speachTimer <= 0)
            {
                speachTimer = speachTimerMax;
                readyToSpeak = true;
                return true;
            }
            return false;
        }
        return false;

    }
    private bool RunBubbleTimer()
    {
            bubbleTimer -= Time.deltaTime;
            if (bubbleTimer <= 0)
            {
                bubbleTimer = bubbleTimerMax;
                return true;
            }
            return false;

    }
}
