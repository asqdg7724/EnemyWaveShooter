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
    private bool alreadyAttack = true;

    public float attackRange = 2f;
    private BossAttack bossAtk;

    private float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        bossAtk = GetComponent<BossAttack>();
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
            agent.enabled = true;
            agent.destination = targetPos.position;
            animator.SetBool("isMoving", true);
            alreadyAttack = true;
        }

        else if (playerInRange && alreadyAttack)
        {
            alreadyAttack = false;
            agent.destination = thisPos.position;
            animator.SetBool("isMoving", false);
            bossAtk.Attack();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
