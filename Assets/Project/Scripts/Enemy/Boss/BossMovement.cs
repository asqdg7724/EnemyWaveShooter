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
    private bool alreadyAttack = false;

    public float attackRange = 2f;
    private BossAttack bossAtk;

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

        timer += Time.deltaTime;
        playerInRange = Physics.CheckSphere(thisPos.position, attackRange, 1 << 6);

        if (!playerInRange && !alreadyAttack)
        {
            animator.SetBool("isMoving", true);
            alreadyAttack = true;
        }

        else if (playerInRange && alreadyAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        agent.enabled = false;
        animator.SetBool("isMoving", false);
        bossAtk.Attack();
        yield return new WaitForSeconds(2f);
        alreadyAttack = false;
        agent.enabled = true;
    }

    IEnumerator SlideAttack()
    {
        alreadyAttack = false;
        animator.SetBool("isMoving", true);
        yield return new WaitForSeconds(3f);
        alreadyAttack = true;
        agent.destination = targetPos.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
