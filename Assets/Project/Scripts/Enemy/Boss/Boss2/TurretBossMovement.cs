using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBossMovement : MonoBehaviour
{
    Transform targetPos;
    public Transform thisPos;
    public GameObject fire;
    public GameObject explosion;

    bool attackPoint = false;
    bool attacking = false;


    // Start is called before the first frame update
    void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetPos);

        if (!attackPoint && !attacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        StartCoroutine(PointAttack());
    }

    IEnumerator PointAttack()
    {
        attacking = true;
        attackPoint = true;
        if (attacking && attackPoint)
        {
            fire.transform.position = targetPos.position;
            attackPoint = false;
        }
        fire.SetActive(true);
        yield return new WaitForSeconds(2f);
        fire.SetActive(false);
        explosion.transform.position = fire.transform.position;
        explosion.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        explosion.SetActive(false);
        attacking = false;
    }
}
