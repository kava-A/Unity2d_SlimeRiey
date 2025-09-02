using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 多类型对象池管理器
/// </summary>
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    // 存储所有对象池的字典，key为预制体，value为对应的对象队列
    private Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();

    // 对象池的父节点，用于场景层级管理
    private Transform poolParent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);

            // 创建对象池父节点
            poolParent = new GameObject("ObjectPools").transform;
            poolParent.SetParent(transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 初始化指定预制体的对象池
    /// </summary>
    /// <param name="prefab">预制体</param>
    /// <param name="initialSize">初始大小</param>
    public void InitPool(GameObject prefab, int initialSize)
    {
        // 如果已存在该预制体的对象池，则不再初始化
        if (pools.ContainsKey(prefab))
            return;

        // 创建该类型对象的专用父节点
        Transform typeParent = new GameObject(prefab.name + "Pool").transform;
        typeParent.SetParent(poolParent);

        // 初始化对象池队列
        Queue<GameObject> poolQueue = new Queue<GameObject>();
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab, typeParent);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }

        // 添加到字典管理
        pools.Add(prefab, poolQueue);
    }

    /// <summary>
    /// 从对象池获取对象(自动扩容）
    /// </summary>
    /// <param name="prefab">预制体</param>
    /// <returns>对象实例</returns>
    public GameObject GetObject(GameObject prefab)
    {
        // 如果对象池不存在，则初始化一个默认大小为5的对象池
        if (!pools.ContainsKey(prefab))
        {
            InitPool(prefab, 5);
        }

        Queue<GameObject> poolQueue = pools[prefab];

        // 如果对象池为空，自动扩容（实例化新对象）
        if (poolQueue.Count == 0)
        {
            GameObject obj = Instantiate(prefab, poolParent.Find(prefab.name + "Pool"));
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }

        // 从队列取出对象并激活
        GameObject target = poolQueue.Dequeue();
        target.SetActive(true);
        return target;
    }

    /// <summary>
    /// 将对象放回对象池
    /// </summary>
    /// <param name="prefab">预制体（用于查找对应对象池）</param>
    /// <param name="obj">要回收的对象</param>
    public void ReturnObject(GameObject prefab, GameObject obj)
    {
        // 如果对象池不存在，直接销毁对象
        if (!pools.ContainsKey(prefab))
        {
            Destroy(obj);
            return;
        }

        // 重置对象状态
        obj.SetActive(false);
        obj.transform.SetParent(poolParent.Find(prefab.name + "Pool"));
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;

        // 清除对象上的临时组件（如果有）
        foreach (var comp in obj.GetComponents<Component>())
        {
            // 保留Transform和用户指定的核心组件
            if (!(comp is Transform) && !(comp is MonoBehaviour))
            {
                Destroy(comp);
            }
        }

        // 放回队列
        pools[prefab].Enqueue(obj);
    }

    /// <summary>
    /// 清空指定预制体的对象池
    /// </summary>
    /// <param name="prefab">预制体</param>
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
    /// 清空所有对象池
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
