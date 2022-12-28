using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    Animator animator;

    private BossMovement bossMove;
    private static BossAttack bossAtk;

    public float atkDelay = 1f;

    private float timer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossMove = GetComponent<BossMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        Invoke(nameof(AttackReady), atkDelay);
    }

    public void AttackReady()
    {
        animator.SetTrigger("Idle");
    }

    IEnumerator AttackReset()
    {
        yield return new WaitForSeconds(3f);

        animator.SetTrigger("Idle");
    }
}
