using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform generatePoint;
    public GameObject finalZone;

    public float generateInterval = 5f;
    public int[] totalEnemy = new int[] { 1, 3, 5, 7 };
    public int index;
    private float countDown;
    public int livingEnemy;

    // Start is called before the first frame update
    void Start()
    {
        //countDown = generateInterval;
        index = 0;
        livingEnemy = 0;
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

        if(index >= totalEnemy.Length && livingEnemy <= 0)
        {
            finalZone.GetComponent<FinalZone>().Loss();
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
            livingEnemy += 1;
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void DecreaseEnemy()
    {
        livingEnemy -= 1;
    }
}
