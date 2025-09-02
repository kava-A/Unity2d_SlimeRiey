using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;//����Ԥ����
    public List<Transform> spawnPoints;//Ԥ��λ��
    public float spawnInterval = 5f;//���ɼ��ʱ��
    [Header("Boss����")]
    public GameObject bossEnemy;
    //ֱ�����͵㱻�򿪣���ֹͣ���ɵ���
    private void OnEnable()
    {
        EventDefine.OnEnemySpawnEvent += BossSpawn;
    }
    private void OnDisable()
    {
        EventDefine.OnEnemySpawnEvent -= BossSpawn;
    }
    private void Start()
    {
        int index = Random.Range(0, enemyPrefabs.Count);//range����ҿ�������ұ�ֱ�ӿ���.count������Ҫ�ټ�1
        int count = Random.Range(3, 10);
        PoolManager.Instance.InitPool(enemyPrefabs[index], count);
        if (spawnPoints.Count > 0 && enemyPrefabs.Count > 0)
        {
            StartCoroutine(EnemySpawn());
        }
        else
        {
            Debug.Log($"�޷�������ͨ���ˣ�\n�������ɵ������б�����Ϊ��{spawnPoints.Count},����Ԥ�����б�����Ϊ{enemyPrefabs.Count}");
        }
    }

    IEnumerator EnemySpawn()
    {
        while (GameManager.Instance.StopSpawnEnemy())
        {
            int index = Random.Range(0, spawnPoints.Count);
            SpawnNormalEnemy(spawnPoints[index].position);
            yield return new WaitForSeconds(spawnInterval);
        }

    }
    /// <summary>
    /// �Ӷ����ȡ�����˲�������ָ��λ��
    /// </summary>
    /// <param name="position">Ŀ��λ��</param>
    public void SpawnNormalEnemy(Vector2 position)
    {
        int index = Random.Range(0, enemyPrefabs.Count);
        GameObject enemy = PoolManager.Instance.GetObject(enemyPrefabs[index]);
        enemy.transform.position = position;
        enemy.SetActive(true);
    }
    public void BossSpawn(Transform targetPos)
    {
        Debug.Log("Boss�����¼�����");
        Instantiate(bossEnemy, targetPos.position, Quaternion.identity);

    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
