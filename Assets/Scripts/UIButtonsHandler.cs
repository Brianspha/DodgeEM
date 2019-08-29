using System;
using System.Collections;
using System.Numerics;
using Nethereum.Contracts;
using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.DTOs;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Helpers.ContractDefinitions;

public class UIButtonsHandler : MonoBehaviour
{
    public InputField addressField;
    private GameManager manager;

    public Contract DodgeEMToken { get; private set; }
    public TransactionSignedUnityRequest TransactionSignedUnityRequest { get; private set; }
    public EthCallUnityRequest EthCallUnityRequest { get; private set; }
    public TransactionReceiptPollingRequest TransactionReceiptPollingRequest { get; private set; }

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        DodgeEMToken = manager.DodgeEMToken;
        TransactionSignedUnityRequest = manager.TransactionSignedUnityRequest;
        EthCallUnityRequest = manager.EthCallUnityRequest;
        TransactionReceiptPollingRequest = manager.TransactionReceiptPollingRequest;
    }

    private CallInput GetTransferTokenCallInput(string from, string receipient, int amount)
    {
        return DodgeEMToken.GetFunction("transferFrom").CreateCallInput(new object[] { from, receipient, amount });
    }



    public void Yes()
    {
        manager.ShowTransferUI();
        manager.newLevel = false;
        manager.totalKills += 1;
    }
    public void Transfer()
    {
        if (addressField.text.Length == 42)
        {
            StartCoroutine(StartTransfer());

        }
    }

    private IEnumerator StartTransfer()
    {
        var transferFromFunction = new TransferFromFunction();
        transferFromFunction.FromAddress = Variables.tokenOwnerAddress;
        transferFromFunction.Recipient = addressField.text;
        transferFromFunction.Amount = manager.emTokens*manager.tokenMultiplier;
        transferFromFunction.Gas = 8000000;
        transferFromFunction.GasPrice = 9000000000000;
        transferFromFunction.Sender = Variables.tokenOwnerAddress;
        yield return TransactionSignedUnityRequest.SignAndSendTransaction(transferFromFunction,Variables.ContractAddress);
        if(TransactionSignedUnityRequest.Result != null)
        {
            StartCoroutine(GetTokenOwnerBalance(addressField.text));
            StartCoroutine(manager.GetTokenOwnerBalance());
            manager.emTokens = 0;
            addressField.text = "";
        }
        else
        {
            Debug.LogError("Something went wrong");
            Debug.LogError(TransactionSignedUnityRequest.Exception);
        }
    }

    public void ResetPlayerStats()
    {
        manager.CreateLevel();
        manager.DeactivateContinuePrefab();
        manager.totalKills += 1;
        manager.newLevel = false;

    }
    public void ShowTransfer()
    {

    }
    public void Continue()
    {
        Debug.LogError("Clicked");
        ResetPlayerStats();
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
    public IEnumerator GetTokenOwnerBalance(string address)
    {
        yield return EthCallUnityRequest.SendRequest(GetBalanceOfCallInput(address), BlockParameter.CreateLatest());
        if (EthCallUnityRequest.Result != null)
        {
            BigInteger balance = GetBalanceOfFunction().DecodeSimpleTypeOutput<BigInteger>(EthCallUnityRequest.Result);
            Debug.LogError("User Balance: " + balance);
        }
        else
        {
            Debug.LogError(EthCallUnityRequest.Exception);
            Debug.LogError(EthCallUnityRequest.Result);
            Debug.LogError(EthCallUnityRequest);


        }

    }

}
