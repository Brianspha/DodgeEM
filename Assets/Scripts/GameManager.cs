using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public float MinX, MaxX, MinZ, MaxZ;
    public float decrementBy { get; private set; }
    public int level { get; private set; }
    public int score { get; private set; }
    public List<GameObject> enemies;
    public List<GameObject> levelEnemies { get; private set; }
    private float health { get; set; }
    private int levelIncrementor = 0;
    private readonly float maxLife = 10;
    public bool gameOver = false;
    private readonly float defaultY = .5f;
    public float levelEnemyHealth;
    private readonly float defaultLevelEnemyHealth = 2;
    public int maxLevel = 10;
    private readonly int defaultLevelKill = 1;
    public int currentLevelKill = 0;
    private int requiredKillLevel;
    public List<EnemySpawner> EnemySpawners;
    public List<Collectible> collectibles;
    // Start is called before the first frame update
    private void Start()
    {
        decrementBy = .25f;
        level = 0;
        health = maxLife;
        levelIncrementor = 1;
        collectibles = new List<Collectible>();
        NewLevel();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.LogError("Current Collectibles: " + collectibles.Count);
        GameOver();
        if (gameOver)
        {
            // Debug.LogError("GameOver");
            NewLevel();
            gameOver = false;
            currentLevelKill = 0;
        }
    }

    private void GameOver()
    {
        gameOver = currentLevelKill == requiredKillLevel;
        //Debug.LogError("Gameover: " + gameOver);
        Debug.LogError("Required Kills: " + requiredKillLevel);
    }

    public void NewLevel()
    {
        levelEnemies = new List<GameObject>();
        level++;
        level = level >= maxLevel ? maxLevel : level;
        health = maxLife;
        requiredKillLevel = level * levelIncrementor;
        for (int i = 0; i < level * levelIncrementor; i++)
        {
            GameObject tempSpawned = enemies[Random.Range(0, enemies.Count)];
            tempSpawned.GetComponent<Enemy>().health = defaultLevelEnemyHealth * level + defaultLevelEnemyHealth;
            levelEnemies.Add(tempSpawned);
        }
        foreach (EnemySpawner spawner in EnemySpawners)
        {
            spawner.setEnemies(levelEnemies);
        }
        levelIncrementor += 2;
    }
    public void IncrementScore()
    {
        score += Random.Range(5, 15);
        Debug.Log("Score: " + score);
    }
    public void DecrementHealth()
    {
        health--;
        //Debug.LogError("Player Health: " + health);
    }
}
