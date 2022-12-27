using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public static EnemySpawner e_spawner;
    public Queue<GameObject> e_queue = new Queue<GameObject>();
    public float xPos;
    public float zPos;
    private Vector3 randomVector;

    // Start is called before the first frame update
    void Start()
    {
        e_spawner = this;

        for(int i = 0; i < 10; i++)
        {
            GameObject t_object = Instantiate(enemy, this.gameObject.transform);
            e_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        StartCoroutine(EnemySpawn());
    }

    public void InsertQueue(GameObject p_object)
    {
        e_queue.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject t_object = e_queue.Dequeue();
        t_object.SetActive(true);

        return t_object;
    }

    IEnumerator EnemySpawn()
    {
        while(true)
        {
            if (e_queue.Count != 0)
            {
                xPos = Random.Range(-5, 5);
                zPos = Random.Range(-5, 5);
                randomVector = new Vector3(xPos, 0.0f, zPos);
                GameObject t_object = GetQueue();
                t_object.transform.position = gameObject.transform.position + randomVector;
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
