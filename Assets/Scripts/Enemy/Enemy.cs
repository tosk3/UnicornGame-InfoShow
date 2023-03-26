using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public event EventHandler<OnDeathArgs> OnEnemyDeath;
    public class OnDeathArgs : EventArgs
    {
        public Vector3 position;
    }
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float distaceToPlayer;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackTimer;
    [SerializeField] private float timerMax;
    [SerializeField] private float secondsToDie;
    [SerializeField] private MagicMissleGun gun;

    [SerializeField] private EnemyBaseState currentState;


    [SerializeField] private float speachTimer;
    [SerializeField] private float bubbleTimer;
    [SerializeField] private float speachTimerMax;
    [SerializeField] private float bubbleTimerMax;
    [SerializeField] private bool readyToSpeak = false;
    [SerializeField] private TextMesh textObj;
    [SerializeField] private Vector3 localOffset = new Vector3(-5f, 10f, 0);
    [SerializeField] private Animator animator;

    [SerializeField] private List<string> speachLines = new List<string> 
    {
        "Sparkles, please!",
        "Sparkles, be reasonable!",
        "we're trying to help you!",
        "this is for your own good, Sparkles!",
        "Sparkles, please I have a family!",
        "You were there for my wedding!",
        "eat shit, Sparkles!",
        "Shoot him, not me!",
        "Sparkles, we were like family!",
        "you're an abomination!",
        "I'm your uncle!",
        "Sparkles, I was like a father to you!"
    };



    // Update is called once per frame
    void Update()
    {
        CalculateDistanceToPlayer();     
        if (currentState.ExitState(this, out EnemyState state))
        {
            ChangeState(state);
        }
        if (currentState.CheckRules(this))
        {
            currentState.ExecuteState(this);
        }

        if (RunSpeachTimer())
        {
            textObj = LevelManager.CreateWorldText(speachLines[UnityEngine.Random.Range(0, speachLines.Count)], this.transform.parent, localOffset, 12, Color.black);
            textObj.fontStyle.Equals(FontStyle.Bold);
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


    private void ChangeState(EnemyState state)
    {
        for (int i = 0; i < currentState.Transitions.Count; i++)
        {
            Debug.Log(currentState.Transitions[i].StateType == state);

            if (currentState.Transitions[i].StateType == state)
            {
                currentState = currentState.Transitions[i];
                return;
            }
        }   
    }

    public Transform GetTarget()
    {
        return playerTarget;
    }
    public void SetTarget(Transform playerTarget)
    {
        if (this.playerTarget == null)
        {
            this.playerTarget = playerTarget;
        }
    }
    public void AttackTarget()
    {
        if (RunTimer())
        {
            if(gun != null)
            {
                gun.Shoot(playerTarget.position);
            }
            else
            {
                playerTarget.GetComponentInParent<PlayerController>().TakeDamage(attackDamage); 
            }
           
        }
    }
    private bool RunTimer()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = timerMax;
            return true;
        }
        return false;
    }

    public void MoveToTarget(float direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, direction * speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation((transform.position - playerTarget.position).normalized);
    } 
    public float GetDistanceToPlayer()
    {
        return distaceToPlayer;
    }
    public void CalculateDistanceToPlayer()
    {
        distaceToPlayer = Vector3.Distance(transform.position, GetTarget().position);
    }

    public float GetHealth()
    {
        return health;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
    }
    public void Die()
    {
        if (rb.isKinematic)
        {
            Destroy(textObj);
            textObj = null;
            animator.enabled = false;
            rb.isKinematic = false;
            OnEnemyDeath?.Invoke(this, new OnDeathArgs() { position = this.transform.position });
            StartCoroutine(Coroutine_Death());
        }
      
    }
    private IEnumerator Coroutine_Death()
    {
        yield return new WaitForSeconds(secondsToDie);
        Destroy(gameObject);
    }
}
