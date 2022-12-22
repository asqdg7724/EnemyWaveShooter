using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform targetPos;
    Transform thisPos;
    NavMeshAgent navMeshAgent;

    GameObject player;
    private PlayerHp playerHp;

    public float dmgDelay = 0.5f;
    public int atkDamge = 10;

    private bool playerIn = false;
    private float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHp = player.GetComponent<PlayerHp>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        thisPos = GetComponent<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerIn = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerIn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = targetPos.position;

        timer += Time.deltaTime;

        if (timer >= dmgDelay && playerIn)
        {
            Attack();
        }

        if (playerHp.hp <= 0)
        {
            navMeshAgent.destination = thisPos.position;

        }
    }

    void Attack()
    {
        timer = 0f;

        if (playerHp.hp > 0)
        {
            playerHp.TakeDamage(atkDamge);
        }
    }
}
