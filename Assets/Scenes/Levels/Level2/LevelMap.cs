using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
    public static LevelMap instance;

    public int[,] map = new int[16,16];

    public int[,] GetLevelMap
    {
        get
        {
            return map;
        }
        set
        {
            map = value;
        }
    }
    
    private void Awake()
    {
        instance = this;
        SetMap();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(map);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetMap()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                map[i, j] = 1;
            }
        }

        map[1, 1] = 0;
        map[1, 2] = 0;
        map[1, 3] = 0;
        for (int i = 1; i <= 14; i++)
        {
            map[i, 4] = 0;
        }
        for (int i = 4; i <= 14; i++)
        {
            map[14, i] = 0;
        }
        for (int i = 5; i <= 8; i++)
        {
            map[4, i] = 0;
        }
        map[3, 8] = 0;
        for (int i = 8; i <= 11; i++)
        {
            map[2, i] = 0;
        }
        map[3, 11] = 0;
        map[4, 11] = 0;
        map[4, 12] = 0;
        map[5, 12] = 0;
        map[6, 12] = 0;
        for (int i = 6; i <= 11; i++)
        {
            map[11, i] = 0;
        }
        map[11, 10] = 0;
        map[12, 10] = 0;
        map[13, 10] = 0;
        map[14, 14] = 0;
    }
}
