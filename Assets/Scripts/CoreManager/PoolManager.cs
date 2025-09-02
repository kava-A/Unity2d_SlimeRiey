using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����Ͷ���ع�����
/// </summary>
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    // �洢���ж���ص��ֵ䣬keyΪԤ���壬valueΪ��Ӧ�Ķ������
    private Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();

    // ����صĸ��ڵ㣬���ڳ����㼶����
    private Transform poolParent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);

            // ��������ظ��ڵ�
            poolParent = new GameObject("ObjectPools").transform;
            poolParent.SetParent(transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ��ʼ��ָ��Ԥ����Ķ����
    /// </summary>
    /// <param name="prefab">Ԥ����</param>
    /// <param name="initialSize">��ʼ��С</param>
    public void InitPool(GameObject prefab, int initialSize)
    {
        // ����Ѵ��ڸ�Ԥ����Ķ���أ����ٳ�ʼ��
        if (pools.ContainsKey(prefab))
            return;

        // ���������Ͷ����ר�ø��ڵ�
        Transform typeParent = new GameObject(prefab.name + "Pool").transform;
        typeParent.SetParent(poolParent);

        // ��ʼ������ض���
        Queue<GameObject> poolQueue = new Queue<GameObject>();
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab, typeParent);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }

        // ��ӵ��ֵ����
        pools.Add(prefab, poolQueue);
    }

    /// <summary>
    /// �Ӷ���ػ�ȡ����(�Զ����ݣ�
    /// </summary>
    /// <param name="prefab">Ԥ����</param>
    /// <returns>����ʵ��</returns>
    public GameObject GetObject(GameObject prefab)
    {
        // �������ز����ڣ����ʼ��һ��Ĭ�ϴ�СΪ5�Ķ����
        if (!pools.ContainsKey(prefab))
        {
            InitPool(prefab, 5);
        }

        Queue<GameObject> poolQueue = pools[prefab];

        // ��������Ϊ�գ��Զ����ݣ�ʵ�����¶���
        if (poolQueue.Count == 0)
        {
            GameObject obj = Instantiate(prefab, poolParent.Find(prefab.name + "Pool"));
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }

        // �Ӷ���ȡ�����󲢼���
        GameObject target = poolQueue.Dequeue();
        target.SetActive(true);
        return target;
    }

    /// <summary>
    /// ������Żض����
    /// </summary>
    /// <param name="prefab">Ԥ���壨���ڲ��Ҷ�Ӧ����أ�</param>
    /// <param name="obj">Ҫ���յĶ���</param>
    public void ReturnObject(GameObject prefab, GameObject obj)
    {
        // �������ز����ڣ�ֱ�����ٶ���
        if (!pools.ContainsKey(prefab))
        {
            Destroy(obj);
            return;
        }

        // ���ö���״̬
        obj.SetActive(false);
        obj.transform.SetParent(poolParent.Find(prefab.name + "Pool"));
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;

        // ��������ϵ���ʱ���������У�
        foreach (var comp in obj.GetComponents<Component>())
        {
            // ����Transform���û�ָ���ĺ������
            if (!(comp is Transform) && !(comp is MonoBehaviour))
            {
                Destroy(comp);
            }
        }

        // �Żض���
        pools[prefab].Enqueue(obj);
    }

    /// <summary>
    /// ���ָ��Ԥ����Ķ����
    /// </summary>
    /// <param name="prefab">Ԥ����</param>
    public void ClearPool(GameObject prefab)
    {
        if (pools.ContainsKey(prefab))
        {
            foreach (var obj in pools[prefab])
            {
                Destroy(obj);
            }
            pools[prefab].Clear();
            pools.Remove(prefab);
        }
    }

    /// <summary>
    /// ������ж����
    /// </summary>
    public void ClearAllPools()
    {
        foreach (var pool in pools.Values)
        {
            foreach (var obj in pool)
            {
                Destroy(obj);
            }
            pool.Clear();
        }
        pools.Clear();
    }
}
