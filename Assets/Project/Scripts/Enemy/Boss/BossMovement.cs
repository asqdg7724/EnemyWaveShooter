using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    Transform targetPos;
    Transform thisPos;
    NavMeshAgent agent;

    GameObject player;
    Animator animator;

    private bool playerInRange = false;
    private bool slideInRange = false;
    public bool attacking = false;
    public bool sliding = false;
    public bool boost = false;

    public float attackRange = 3f;
    public float slideRange = 6f;
    private BossAttack bossAtk;
    public float slideDistance;


    private float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        bossAtk = GetComponentInChildren<BossAttack>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        thisPos = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = targetPos.position;
        slideDistance = Vector3.Distance(targetPos.position, thisPos.position);

        timer += Time.deltaTime;
        playerInRange = Physics.CheckSphere(thisPos.position, attackRange, 1 << 6);
        slideInRange = Physics.CheckSphere(thisPos.position, slideRange, 1 << 6);

        if (!playerInRange && !attacking && !sliding)
        {
            animator.SetBool("isMoving", true);
            
        }

        if (!playerInRange && attacking && !sliding)
        {
            bossAtk.AttackReady();
            attacking = false;
        }

        if (playerInRange && !attacking && !sliding)
        {
            StartCoroutine(Attack());
        }

        if (slideDistance >= 15f && !boost && !sliding)
        {
            agent.speed = 9;

            boost = true;

            if (slideInRange && !playerInRange && !sliding && boost)
            {
                sliding = true;

                if (slideInRange && sliding)
                {
                    StartCoroutine(SlideAttack());
                }
                
                else if (!slideInRange && !sliding && !boost)
                {
                    bossAtk.AttackReady();
                    animator.SetTrigger("Idle");
                }
            }
        } 
    }

    IEnumerator Attack()
    {
        agent.speed = 5;
        agent.enabled = false;
        boost = false;
        animator.SetBool("isMoving", false);
        animator.SetTrigger("Attack");
        bossAtk.Attack();
        attacking = true;
        yield return new WaitForSeconds(1f);
        agent.enabled = true;
    }

    IEnumerator SlideAttack()
    {
        boost = false;
        animator.SetTrigger("Slide");
        bossAtk.Attack();
        yield return new WaitForSeconds(3f);
        sliding = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, slideRange);
    }
}
