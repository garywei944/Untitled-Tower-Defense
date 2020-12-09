using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGenerator : MonoBehaviour
{
    public GameObject[] turretPrefabs = new GameObject[2]; //change number to total turret
    public float generateInterval = 20f;
    private float countDown;

    private System.Random r;

    // Start is called before the first frame update
    void Start()
    {
        r = new System.Random();

        //generate a turret at random position
        int x = r.Next(0, 16);
        int y = r.Next(0, 16);
        while (LevelMap.instance.GetLevelMap[x,y] == 0)
        {
            x = r.Next(0, 16);
            y = r.Next(0, 16);
        }

        int turretType = r.Next(0, 2);
        Generate(x, y, turretType);

        countDown = generateInterval;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            int x = r.Next(0, 16);
            int y = r.Next(0, 16);
            while (LevelMap.instance.GetLevelMap[x, y] == 0)
            {
                x = r.Next(0, 16);
                y = r.Next(0, 16);
            }

            int turretType = r.Next(0, 2);
            Generate(x, y, turretType);

            countDown = generateInterval;
        }
    }

    private void Generate(int x, int y, int type)
    {
        //TODO generate a new turret
        Vector3 pos = WorldCoor(x, y);
        Vector3 rotation = NearestPathNode(x, y);
        Instantiate(turretPrefabs[type], pos, Quaternion.LookRotation(rotation));
    }

    private Vector3 WorldCoor(int x, int y)
    {
        Vector3 pos = new Vector3();
        pos.y = 0.5f;
        pos.x = -70f + 5 * y;
        pos.z = 70f - 5 * x;
        return pos;
    }

    private Vector3 NearestPathNode(int x, int y)
    {
        int[] dx = new int[] { 0, 1, 0, -1 };
        int[] dy = new int[] { 1, 0, -1, 0 };

        int resultX = -1;
        int resultY = -1;
        int turretX = x;
        int turretY = y;
        
        //BFS
        ArrayList found = new ArrayList();
        ArrayList toFind = new ArrayList();

        toFind.Add(Coor(x, y));
        while(toFind.Count != 0)
        {
            int n = (int)toFind[0];
            toFind.RemoveAt(0);
            x = CoorX(n);
            y = CoorY(n);

            if(LevelMap.instance.GetLevelMap[x, y] == 0)
            {
                resultX = x;
                resultY = y;
                break;
            }
            for(int i = 0; i < 4; i++)
            {
                if (InRange(x + dx[i], y + dy[i]))
                    toFind.Add(Coor(x + dx[i], y + dy[i]));
            }
        }

        Vector3 res = WorldCoor(resultX, resultY) - WorldCoor(turretX, turretY);
        return res;
    }

    private bool InRange(int x, int y)
    {
        return x >= 0 && x <= 15 && y >= 0 && y <= 15;
    }

    private int Coor(int x, int y)
    {
        return x * 16 + y;
    }

    private int CoorX(int n)
    {
        return n / 16;
    }

    private int CoorY(int n)
    {
        return n % 16;
    }
}
