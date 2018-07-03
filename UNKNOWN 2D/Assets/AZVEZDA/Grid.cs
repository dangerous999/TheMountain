using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public Transform player;
    public Transform enemy;
    public LayerMask NoWalk;
    public Vector2 gridSize;
    public float nodeRadius;
    NODE[,] grid;

    float nodeDiameter;
    int gridX, gridY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridX = Mathf.RoundToInt (gridSize.x / nodeDiameter);
        gridY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
        
    }

    void CreateGrid()
    {
        grid = new NODE[gridX, gridY];
        Vector2 bottonLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.up * gridSize.y / 2;
        Vector2 left = bottonLeft;
        for(int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector2 worldPoint = left + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, NoWalk));
                grid[x, y] = new NODE(walkable, worldPoint,x,y);
            } 
        }
    }

    public NODE NODEFromWorldPoint(Vector2 position2)
    {
        float percentx = (position2.x + gridSize.x / 2) / gridSize.x;
        float percenty = (position2.y + gridSize.y / 2) / gridSize.y;
        percentx = Mathf.Clamp01(percentx);
        percenty = Mathf.Clamp01(percenty);

        int x = Mathf.RoundToInt((gridX - 1) * percentx);
        int y = Mathf.RoundToInt((gridY - 1) * percenty);
        return grid[x, y];
    }

    public List<NODE>getNaighbours(NODE a)
    {
        List<NODE> neighbours = new List<NODE>();
        for(int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x==0 && y == 0)
                {
                    continue;
                }
                int checkX = a.gridX + x;
                int checky = a.gridY + y;

                if (checkX >= 0 && checkX < gridX && checky >= 0 && checky < gridY) 
                neighbours.Add(grid[checkX, checky]);
            }

        }
        return neighbours;
    }
    public List<NODE> path;
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(gridSize.x, gridSize.y));
        if (grid != null)
        {
            NODE playerNode = NODEFromWorldPoint(player.position);
            NODE enemyNode = NODEFromWorldPoint(enemy.position);
            foreach (NODE n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.green : Color.red;
                if (playerNode == n)
                {
                    Gizmos.color = Color.yellow;
                }
                if (enemyNode == n)
                {
                    Gizmos.color = Color.cyan;
                }
                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                
                Gizmos.DrawCube(n.position, Vector2.one * (nodeDiameter - .3f));
                
                
            }
        }
    }
}
