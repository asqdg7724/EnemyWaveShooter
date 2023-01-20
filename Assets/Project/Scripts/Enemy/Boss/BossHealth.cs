using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxBossHp = 600;

    bool isDead = false;
    int bossHp;
    public int enemyScore;

    public static EnemyHealth eh;
    private GameManager gmg;

    // Start is called before the first frame update
    void Awake()
    {
        eh = GetComponent<EnemyHealth>();
        bossHp = maxBossHp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        bossHp -= amount;

        if (bossHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        GameManager.gmg.OpenGameClear();

        gameObject.SetActive(false);
    }
}
