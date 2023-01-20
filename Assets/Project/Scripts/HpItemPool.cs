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

    // ���ӿ�����Ʈ�� ��ȯ�ϴ� �Լ�
    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ Ǯ�� ��Ȱ��ȭ �Ǿ��ִ� ���� ������Ʈ�� ����
        foreach (GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                // �߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // �߰����� ���ϸ� ���Ӱ� �����ϰ� select ������ �Ҵ�
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
