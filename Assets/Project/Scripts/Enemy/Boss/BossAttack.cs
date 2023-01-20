using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    Animator animator;

    private BossMovement bossMove;
    private static BossAttack bossAtk;
    public SphereCollider attackCol;

    GameObject player;

    private PlayerHp playerHp;

    public float dmgDelay = 0.5f;
    public float atkDelay = 1f;

    private float timer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        bossMove = GetComponentInParent<BossMovement>();
        attackCol = GetComponent<SphereCollider>();
        playerHp = player.GetComponent<PlayerHp>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void Attack()
    {
        attackCol.enabled = true;
        Invoke(nameof(AttackReady), atkDelay);
    }

    public void AttackReady()
    {
        animator.SetTrigger("Idle");
        attackCol.enabled = false;
    }

    IEnumerator AttackReset()
    {
        yield return new WaitForSeconds(3f);

        animator.SetTrigger("Idle");

        attackCol.enabled = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerHp.TakeDamage(10);
        }
    }

    void AttackSlide()
    {
        animator.SetTrigger("Slide");

        attackCol.enabled = true;
    }
}
