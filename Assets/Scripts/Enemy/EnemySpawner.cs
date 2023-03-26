using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public event EventHandler<OnSpawnArgs> OnSpawn;
    public class OnSpawnArgs : EventArgs
    {
       public Enemy enemySpawned;
    }

    [SerializeField] private Transform playerTarget;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<Enemy> fairies;
    [SerializeField] private List<Enemy> enemiesOnScreen;
    [SerializeField] private float spawnTimer;
    [SerializeField] private float maxSpawnTimer;
    [SerializeField] private float spawnRadius;
    [SerializeField] private bool play = true;


    private void Update()
    {
        if (play)
        {
            if (RunTimer())
            {
                SpawnSphereOnEdgeRandomly();
            }
        }  
    }
    public void EndGame()
    {
        play = false;
        foreach (Enemy enemy in enemiesOnScreen)
        {
            Destroy(enemy);           
        }
        enemiesOnScreen.Clear();
        foreach (Enemy enemy in fairies)
        {
            Destroy(enemy);   
        }
        fairies.Clear();
    }
    private bool RunTimer()
    {
        spawnTimer -=Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = maxSpawnTimer;
            return true;
        }
        return false;
    }

    private void SpawnSphereOnEdgeRandomly()
    {

        Vector3 randomPos = UnityEngine.Random.insideUnitSphere * spawnRadius;
        randomPos += playerTarget.position;
        randomPos.y = 0.2f;

        Vector3 direction = randomPos - playerTarget.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(playerTarget.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / playerTarget.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * spawnRadius + playerTarget.position.x;
        randomPos.z = Mathf.Sin(dotProductAngle * (UnityEngine.Random.value > 0.5f ? 1f : -1f)) * spawnRadius + playerTarget.position.z;

        if (Physics.Raycast(randomPos + new Vector3(0f,2f,0f), -Vector3.up, out RaycastHit hit) && hit.transform.tag == "Ground")
        {
            GameObject enemyObj = Instantiate(PickRandomPrefab(), randomPos, Quaternion.identity);
            enemiesOnScreen.Add(enemyObj.GetComponent<Enemy>());
            enemiesOnScreen[enemiesOnScreen.Count - 1].SetTarget(playerTarget);
            enemyObj.transform.position = randomPos;

            OnSpawn?.Invoke(this, new OnSpawnArgs() { enemySpawned = enemyObj.GetComponent<Enemy>() });
        }
       
    }
    private GameObject PickRandomPrefab()
    {
        return enemyPrefabs[UnityEngine.Random.Range(0,enemyPrefabs.Count)];
    }

    public List<Enemy> GetEnemiesOnScreen()
    {
        return enemiesOnScreen;
    }
    public List<Enemy> GetFairies()
    {
        return fairies;
    }
    public void DecreaseSpawnTime(float amount)
    {
        maxSpawnTimer -= amount * 0.00001f;

        if (maxSpawnTimer <= 0)
        {
            maxSpawnTimer = 0.1f;
        }
    }
}
