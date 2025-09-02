using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;//敌人预制体
    public List<Transform> spawnPoints;//预设位置
    public float spawnInterval = 5f;//生成间隔时间
    [Header("Boss敌人")]
    public GameObject bossEnemy;
    //直到传送点被打开，才停止生成敌人
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
        int index = Random.Range(0, enemyPrefabs.Count);//range左闭右开，因此右边直接可以.count，不需要再减1
        int count = Random.Range(3, 10);
        PoolManager.Instance.InitPool(enemyPrefabs[index], count);
        if (spawnPoints.Count > 0 && enemyPrefabs.Count > 0)
        {
            StartCoroutine(EnemySpawn());
        }
        else
        {
            Debug.Log($"无法生成普通敌人！\n敌人生成点坐标列表数量为：{spawnPoints.Count},敌人预制体列表数量为{enemyPrefabs.Count}");
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
    /// 从对象池取出敌人并生成在指定位置
    /// </summary>
    /// <param name="position">目标位置</param>
    public void SpawnNormalEnemy(Vector2 position)
    {
        int index = Random.Range(0, enemyPrefabs.Count);
        GameObject enemy = PoolManager.Instance.GetObject(enemyPrefabs[index]);
        enemy.transform.position = position;
        enemy.SetActive(true);
    }
    public void BossSpawn(Transform targetPos)
    {
        Debug.Log("Boss生成事件触发");
        Instantiate(bossEnemy, targetPos.position, Quaternion.identity);

    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
