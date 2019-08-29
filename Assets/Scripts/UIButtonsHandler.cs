using UnityEngine;
using UnityEngine.UI;
public class UIButtonsHandler : MonoBehaviour
{
    public Text addressField;
    private GameManager manager;
    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public void Yes()
    {
        manager.ShowTransferUI();
        manager.newLevel = false;
        manager.totalKills += 1;
    }
    public void Transfer()
    {
    }
    public void ResetPlayerStats()
    {
        manager.CreateLevel();
        manager.DestroyContinuePrefab();
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

}
