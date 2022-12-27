using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackEnemy : MonoBehaviour
{
    Transform targetPos;
    Transform thisPos;
    NavMeshAgent agent;

    GameObject player;
    private PlayerHp playerHp;
    Animator animator;

    public GameObject attackCol;
    public float dmgDelay = 0.5f;
    public float atkDelay = 1.5f;
    public int atkDamge = 10;

    private bool playerInRange;
    private bool alreadyAttack = true;

    public float attackRange = 1.3f;
    private float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHp = player.GetComponent<PlayerHp>();
        animator = GetComponent<Animator>();
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
        timer += Time.deltaTime;
        playerInRange = Physics.CheckSphere(thisPos.position, attackRange, 1 << 6);

        if (!playerInRange && alreadyAttack)
        {
            agent.destination = targetPos.position;
            animator.SetBool("isMoving", true);
        }

        else if (playerInRange && alreadyAttack)
        {
            alreadyAttack = false;
            agent.destination = thisPos.position;
            animator.SetBool("isMoving", false);
            animator.SetTrigger("Attack");
            Attack();
        }
    }

    void Attack()
    {
        
        attackCol.SetActive(true);
        Invoke(nameof(AttackReset), atkDelay);
    }

    void AttackReset()
    {
        attackCol.SetActive(false);
        animator.SetTrigger("Idle");
        alreadyAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
