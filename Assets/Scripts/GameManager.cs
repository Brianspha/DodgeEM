using Nethereum.Contracts;
using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.DTOs;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
    public BigInteger totalDodgeEMTokens;//@dev initially 0 but will be updated as soon as the game starts from token contract value
    public float levelEnemyHealth;
    private readonly float defaultLevelEnemyHealth = 2;
    public int maxLevel = 10;
    private readonly int defaultLevelKill = 1;
    public int currentLevelKill = 0;
    private int requiredKillLevel;
    public int totalKills = 0, emTokens = 0;
    public List<EnemySpawner> EnemySpawners;
    public Text kills, tokens;
    public GameObject continuePrefab, transferPrefab;
    public bool carryOn;
    public Contract DodgeEMToken;
    public EthCallUnityRequest EthCallUnityRequest;
    private PlayerMovement player;
    public int tokenMultiplier = 100;

    public TransactionReceiptPollingRequest TransactionReceiptPollingRequest { get; private set; }

    public TransactionSignedUnityRequest TransactionSignedUnityRequest;
    // Start is called before the first frame update
    private void Start()
    {
        decrementBy = .25f;
        level = 0;
        health = maxLife;
        levelIncrementor = 1;
        TransactionSignedUnityRequest = new TransactionSignedUnityRequest(Variables.NodeAddress, Variables.PrivateKey);
        EthCallUnityRequest = new EthCallUnityRequest(Variables.NodeAddress);
        TransactionReceiptPollingRequest = new TransactionReceiptPollingRequest(Variables.NodeAddress);
        DodgeEMToken = new Contract(null, Variables.ABI, Variables.ContractAddress);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        StartCoroutine(GetTokenOwnerBalance());
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateScore();
        GameOver();
        if (newLevel)
        {
            player.canMove = false;
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
        DeactivateContinuePrefab();
    }
    public void DeactivateContinuePrefab()
    {
        continuePrefab.SetActive(false);
    }
    public void DeactivateTransferPrefab()
    {
        transferPrefab.SetActive(false);
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
        player.canMove = true;
        DeactivateContinuePrefab();
        DeactivateTransferPrefab();

    }
    public void IncrementScore()
    {
        score += Random.Range(5, 15);
    }
    public void DecrementHealth()
    {
        health--;
    }

    /// <summary>
    /// Get the function which allows us to check the token owners balance
    /// which will aid in determining how many tokens we spawn in game
    /// </summary>
    /// <returns>Function</returns>
    public Function GetBalanceOfFunction()
    {
        return DodgeEMToken.GetFunction("balanceOf");
    }
    /// <summary>
    /// Creates paramaters fr the Balanceof function
    /// </summary>
    /// <returns></returns>
    public CallInput GetBalanceOfCallInput(string address)
    {
        return GetBalanceOfFunction().CreateCallInput(new object[] { address });
    }
    private int DecodeTokenOwnerBalance(string result)
    {
        return GetBalanceOfFunction().DecodeSimpleTypeOutput<int>(result);
    }
    public IEnumerator GetTokenOwnerBalance()
    {
        yield return EthCallUnityRequest.SendRequest(GetBalanceOfCallInput(Variables.tokenOwnerAddress), BlockParameter.CreateLatest());
        if (EthCallUnityRequest.Result != null)
        {
            BigInteger balance = GetBalanceOfFunction().DecodeSimpleTypeOutput<BigInteger>(EthCallUnityRequest.Result);
            Debug.LogError("Balance: " + balance);
            totalDodgeEMTokens = balance;
            CreateLevel();
        }
        else
        {
            Debug.LogError(EthCallUnityRequest.Exception);
            Debug.LogError(EthCallUnityRequest.Result);
            Debug.LogError(EthCallUnityRequest);


        }

    }

}
