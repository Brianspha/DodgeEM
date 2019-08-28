using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private List<string> words;
    // Start is called before the first frame update
    private void Start()
    {

    }
    private void OnEnable()
    {
        InitialiseWords();
        DestroyMe();
        Debug.LogError("OnEnable");
    }
    public void DestroyMe()
    {
        gameObject.GetComponent<TextMesh>().text = words[Random.Range(0, words.Count)];
        Destroy(gameObject, .25f);
    }
    private void InitialiseWords()
    {
        words = new List<string>
        {
            "Yasss",
            "Good Going",
            "Kill EM"
        };
    }
    // Update is called once per frame
    private void Update()
    {

    }
}
