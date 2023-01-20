using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItemPool : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    // Start is called before the first frame update
    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    // 게임오브젝트를 반환하는 함수
    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀의 비활성화 되어있는 게임 오브젝트에 접근
        foreach (GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                // 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // 발견하지 못하면 새롭게 생성하고 select 변수에 할당
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
