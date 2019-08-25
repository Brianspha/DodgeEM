using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    // Use this for initialization
    public float destructionTime = 1f;
    bool destrucYet = false;
    void Awake() { }

    void Start()
    {
        destrucYet = true;
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        destructionTime -= Time.deltaTime;
        if (destructionTime <= 0)
        {
            DestroyMe();
            destrucYet = false;
        }
    }
}