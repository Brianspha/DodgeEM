using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject bullet;
    public float currentTime = 0, maxTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }
    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 0)
        {
            Spawn();
            currentTime = maxTime;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }
    private void Spawn()
    {
        bullet.GetComponent<Bullet>().currentFacing =transform.forward;
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
