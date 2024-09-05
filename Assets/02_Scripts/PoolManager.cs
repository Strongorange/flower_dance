using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    public List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; // 프리팹의 수에 맞게 크기 N의 pools 배열이 생성됨 ex) 3개의 프리팹이 있다고 가정시 pools = [null, null, null]

        for (int i = 0; i < pools.Length; i++) // 각 pools 인덱스에 빈 리스트를 할당 pools = [ [], [], [] ]
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀의 비활성화된 게임 오브젝트 접근
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // 해당 풀에 비활성화된 오브젝트가 없으면 새롭게 생성하고 select 변수에 할당
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
