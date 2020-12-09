using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject pointPrefab;
    public Transform environment;

    private int[,] map;

    // Start is called before the first frame update
    void Start()
    {
        map = new int[16,16];
        getRandomMap();
        GenerateNode();
        GeneratePoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GeneratePoint()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                if (map[i, j] == 0)
                    Instantiate(pointPrefab, new Vector3(-70 + i * 5 + 2, 0, 70 - j * 5 - 2), environment.rotation);
            }
        }
    }

    private void GenerateNode()
    {
        for(int i = 0; i < 16; i++)
        {
            for(int j = 0; j < 16; j++)
            {
                if(map[i,j] == 1)
                    Instantiate(nodePrefab, new Vector3(-70 + i * 5 + 2, 0, 70 - j * 5 - 2), environment.rotation);
            }
        }
    }

    private void getRandomMap()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                map[i,j] = 1;
            }
        }
        map[1,1] = 0;
        map[14,14] = 0;
        System.Random r = new System.Random();
        for (int i = 1; i <= 14; i++)
        {
            for (int j = 1; j <= 14; j++)
            {
                if ((i == 14 && j == 14) || (i == 1 && j == 1))
                {
                    continue;
                }
                if (r.NextDouble() < 0.5f)
                {
                    map[i,j] = 0;
                }
            }
        }

        int[,] temp = FindPath();
        List<int> res = new List<int>();
        GeneratePath(temp, 17, 254, res);

        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                map[i,j] = 1;
            }
        }
        map[1,1] = 0;
        map[14,14] = 0;

        for (int i = 0; i < res.Count; i++)
        {
            map[res[i] / 16, res[i] % 16] = 0;
        }
    }

    private void GeneratePath(int[,] path, int start, int end, List<int> p)
    {
        int k = path[start,end];
        if (k == -1)
            return;
        GeneratePath(path, start, k, p);
        p.Add(k);
        GeneratePath(path, k, end, p);
    }

    private int[,] FindPath()
    {
        int N = 16 * 16;

        //build graph
        int[,] graph = new int[N,N];
        int[,] path = new int[N,N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                graph[i,j] = 999; //not reachable
                path[i,j] = -1;
            }
        }
        int[] dx = new int[] { -1, 0, 1, 0 };
        int[] dy = new int[] { 0, -1, 0, 1 };

        for (int w = 0; w < 16; w++)
        {
            for (int l = 0; l < 16; l++)
            {
                for (int i = 0; i < 4; i++)
                {
                    int x = w + dx[i];
                    int y = l + dy[i];
                    graph[16 * w + l,16 * w + l] = 0;
                    if (IsValidPosition(x, y))
                    {
                        if (map[x,y] == 1)
                        {
                            graph[16 * w + l,16 * x + y] = 1;
                        }
                        else
                        {
                            graph[16 * w + l,16 * x + y] = 0;
                        }
                    }
                }
            }
        }

        //find path
        for (int k = 0; k < N; k++)
        {
            for (int i = 0; i < N; i++)
            {
                if (graph[i,k] == 999)
                    continue;
                for (int j = 0; j < N; j++)
                {
                    if (graph[k,j] != 99 && graph[i,j] > graph[i,k] + graph[k,j])
                    {
                        graph[i,j] = graph[i,k] + graph[k,j];
                        path[i,j] = k;
                    }
                }
            }
        }

        return path;
    }

    private bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < 16 && y >= 0 && y < 16;
    }
}
