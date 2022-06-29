using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }
    [HideInInspector] public int fruitCount;
    [Header("General Settings")]
    [SerializeField] GameObject Character;
    [SerializeField] float distanceFromCharacter;

    [Header("Fruit Spawn Settings")]
    [SerializeField] SpawnerSwitch[] fruitSpawner;
    [SerializeField] ObjectPooler[] fruitPool;
    [SerializeField] [Min(0)] float fruitSpawnTime;
    [SerializeField] [Min(0)] int fruitCountMax;

    [Header("Enemy Spawn Settings")]
    [SerializeField] GameObject[] enemySpawner;
    [SerializeField] ObjectPooler enemyPool;

    List<Enemy> enemyCount = new List<Enemy>();

    private void Start()
    {
        StartCoroutine(SpawnFruit());

        instance = this;
    }

    int GetRandomIndex(int arrayLength)
    {
        int result = Random.Range(0, arrayLength);
        return result;
    }

    IEnumerator SpawnFruit()
    {
        yield return new WaitForSeconds(fruitSpawnTime);
        if (fruitCount < fruitCountMax)
        {
            int spawnerIndex = GetRandomIndex(fruitSpawner.Length);            
            while (fruitSpawner[spawnerIndex].isOccupied || Vector2.Distance(fruitSpawner[spawnerIndex].transform.position, Character.transform.position) < distanceFromCharacter)
            {
                spawnerIndex = GetRandomIndex(fruitSpawner.Length);
            }

            GameObject fruit = fruitPool[GetRandomIndex(fruitPool.Length)].GetPooledObject();
            fruit.transform.position = fruitSpawner[spawnerIndex].gameObject.transform.position;
            fruit.gameObject.SetActive(true);
            if (fruit.name == "cherry(Clone)")
            {
                Fruit_Cherry fruitSwitch = fruit.GetComponent<Fruit_Cherry>();
                fruitSwitch.spawner = fruitSpawner[spawnerIndex];
            }
            else if (fruit.name == "banana(Clone)")
            {
                Fruit_Banana fruitSwitch = fruit.GetComponent<Fruit_Banana>();
                fruitSwitch.spawner = fruitSpawner[spawnerIndex];
            }
            else
            {
                Fruit_Watermelon fruitSwitch = fruit.GetComponent<Fruit_Watermelon>();
                fruitSwitch.spawner = fruitSpawner[spawnerIndex];
            }
            fruitSpawner[spawnerIndex].isOccupied = true;
            fruitCount++;
        }
        StartCoroutine(SpawnFruit());
    }

    public void SpawnEnemy()
    {
        foreach (Enemy enemy in enemyCount)
        {
            enemy.speed += enemy.challengeIncrease;
        }

        if (enemyCount.Count < enemyPool.countSpawned)
        {
            int spawnerIndex = GetRandomIndex(enemySpawner.Length);
            while (Vector2.Distance(enemySpawner[spawnerIndex].transform.position, Character.transform.position) < distanceFromCharacter)
            {
                spawnerIndex = GetRandomIndex(enemySpawner.Length);
            }
            GameObject obj = enemyPool.GetPooledObject();
            obj.transform.position = enemySpawner[spawnerIndex].gameObject.transform.position;
            obj.gameObject.SetActive(true);

            Enemy enemy = obj.GetComponent<Enemy>();
            enemyCount.Add(enemy);
        }
    }
}