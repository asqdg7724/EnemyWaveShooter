using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxEnemyHp = 10;

    bool isDead = false;
    int enemyHp;
    public int enemyScore;

    public static EnemyHealth eh;
    private GameManager gmg;

    // Start is called before the first frame update
    void Awake()
    {
        eh = GetComponent<EnemyHealth>();
        enemyHp = maxEnemyHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            Die();
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        enemyHp -= amount;

        if (enemyHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        gameObject.SetActive(false);

        EnemySpawner.e_spawner.InsertQueue(gameObject);

        GameManager.gmg.ScoreUp(enemyScore);
    }
}
