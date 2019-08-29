using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public bool newLevel = false;
    private readonly float defaultY = .5f;


    public float levelEnemyHealth;
    private readonly float defaultLevelEnemyHealth = 2;
    public int maxLevel = 10;
    private readonly int defaultLevelKill = 1;
    public int currentLevelKill = 0;
    private int requiredKillLevel;
    public int totalKills = 0, emTokens = 0;
    public List<EnemySpawner> EnemySpawners;
    public List<Collectible> collectibles;
    public Text kills, tokens;
    public GameObject continuePrefab, transferPrefab;
    public bool carryOn;
    // Start is called before the first frame update
    private void Start()
    {
        decrementBy = .25f;
        level = 0;
        health = maxLife;
        levelIncrementor = 1;
        collectibles = new List<Collectible>();
        CreateLevel();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateScore();
        GameOver();
        if (newLevel)
        {
            SpawnContinuePrefab();
        }
    }

    private void UpdateScore()
    {
        kills.text = totalKills.ToString();
        tokens.text = emTokens.ToString();
    }

    private void GameOver()
    {
        newLevel = totalKills > 0 && totalKills % 100 == 0;
    }

    private void SpawnContinuePrefab()
    {
        continuePrefab.SetActive(true);
        currentLevelKill = 0;
    }

    public void ShowTransferUI()
    {
        transferPrefab.SetActive(true);
        continuePrefab.SetActive(false);
    }
    public void DestroyContinuePrefab()
    {
        continuePrefab.SetActive(false);
    }
    public void CreateLevel()
    {
        level++;
        levelEnemies = new List<GameObject>();
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
            spawner.SetEnemies(levelEnemies);
        }
        levelIncrementor += 1;
    }
    public void IncrementScore()
    {
        score += Random.Range(5, 15);
    }
    public void DecrementHealth()
    {
        health--;
    }
}
