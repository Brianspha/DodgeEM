using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public float maxSpawnTime = 3;
    public float currentSpawnTime = 0;
    private List<GameObject> enemies;
    public bool startSpawning = false;
    private GameManager manager;
    // Start is called before the first frame update
    private void Start()
    {
        enemies = new List<GameObject>();
    }
    // Update is called once per frame
    private void Update()
    {
        CheckGameOver();
        if (startSpawning && !manager.newLevel)
        {
            if (currentSpawnTime <= 0)
            {
                Spawn();
                currentSpawnTime = maxSpawnTime;
            }
            else
            {
                currentSpawnTime -= Time.deltaTime;
            }
        }
    }
    public void CheckGameOver()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (manager.newLevel)
        {
            startSpawning = false;
            gameObject.SetActive(false);
        }
    }
    public void SetEnemies(List<GameObject> enemies)
    {
        this.enemies = enemies;
        startSpawning = true;
        gameObject.SetActive(true);
    }
    private void Spawn()
    {
        Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, Quaternion.identity);
    }
}
