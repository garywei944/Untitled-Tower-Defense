using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform generatePoint;

    public float generateInterval = 5f;
    public int totalEnemy = 1;
    private float countDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0)
        {
            GenerateEnemy();
            countDown = generateInterval;
        }
    }

    void GenerateEnemy()
    {
        Instantiate(enemyPrefab, generatePoint.position, generatePoint.rotation);
    }
}
