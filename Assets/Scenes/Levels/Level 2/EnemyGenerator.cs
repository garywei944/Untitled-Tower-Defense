using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform generatePoint;

    public float generateInterval = 5f;
    public int[] totalEnemy = new int[] { 1, 3, 5, 7 };
    private int index;
    private float countDown;
    // Start is called before the first frame update
    void Start()
    {
        //countDown = generateInterval;
        index = 0;
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
        if (index >= totalEnemy.Length)
            return;
        StartCoroutine(GenerateSingleEnemy(index));
        Debug.Log("AAA");
        index++;
    }

    private IEnumerator GenerateSingleEnemy(int idx)
    {
        for(int i = 0; i < totalEnemy[idx]; i++)
        {
            Instantiate(enemyPrefab, generatePoint.position, generatePoint.rotation);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
