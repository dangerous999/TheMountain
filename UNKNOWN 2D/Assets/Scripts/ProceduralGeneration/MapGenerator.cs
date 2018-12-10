using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public int width;
    public int height;
    public int smoothIterations = 5;
    public int borderThickness = 5;
    public string seed;
    public bool useRandomSeed;
    public bool robotMode = false;
    
   
    /// <summary>
    /// Number of neighbours required around tile for it to survive
    /// </summary>
    public int neighbourThreshold = 4;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map; // 0 = empty, 1 = block

    void Start()
    {
        GenerateMap();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateMap();
        }
    }
    void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();
        for(int i = 0; i < smoothIterations; i++)
        {
            SmoothMap();
        }

        int[,] borderedMap = new int[width + borderThickness * 2, height + borderThickness * 2];

        for(int x = 0; x < borderedMap.GetLength(0); x++)
        {
            for(int y = 0; y < borderedMap.GetLength(1); y++)
            {
                if (x >= borderThickness && x < width + borderThickness && y >= borderThickness && y < height + borderThickness)
                {
                    borderedMap[x, y] = map[x - borderThickness, y - borderThickness];
                }
                else
                {
                    borderedMap[x, y] = 1;
                }
            }
        }

        MeshGenerator meshGen = GetComponent<MeshGenerator>();
        meshGen.GenerateMesh(borderedMap, 1);
    }

    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = DateTime.Now.Ticks.ToString();
        }

        System.Random prng = new System.Random(seed.GetHashCode());

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if (x == 0 || x == width -1 || y == 0 || y == height - 1) // Edges are always walls
                {
                    map[x, y] = 1; 
                }
                else
                {
                    map[x, y] = (prng.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);
                if (neighbourWallTiles > neighbourThreshold)
                {
                    map[x, y] = 1;
                }else if(robotMode) // Robot mode will generate more angular shapes, usually requires a bigger fill percent and more smoothing iterations
                {
                    if(neighbourWallTiles <= neighbourThreshold)
                        map[x, y] = 0;
                }else if(neighbourWallTiles < neighbourThreshold)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    int GetSurroundingWallCount(int x, int y)
    {
        int wallCount = 0;
        for(int neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
        {
            for(int neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != x || neighbourY != y) // dont look at org tile
                    {

                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++; // edge tile -> encourage walls around edge of map
                }

            }
        }

        return wallCount;
    }

    void OnDrawGizmos()
    {
        /*
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
                    Vector3 position = new Vector3(-width / 2 + x + 0.5f, 0, -height / 2 + y + 0.5f);
                    Gizmos.DrawCube(position, Vector3.one);
                }
            }
        }*/
    }

}
