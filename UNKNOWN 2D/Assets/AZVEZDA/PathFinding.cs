using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

    Grid grid;
    public Transform seek, target;
    void Awake()
    {
        grid = GetComponent<Grid>();

    }
    private void Update()
    {
        FindPath(seek.position, target.position);
    }

    void FindPath(Vector2 startPos, Vector2 targetPos)
    {
        NODE startNode = grid.NODEFromWorldPoint(startPos);
        NODE targettNode = grid.NODEFromWorldPoint(targetPos);

        List<NODE> setO = new List<NODE>();
        HashSet<NODE> setClosed = new HashSet<NODE>();
        setO.Add(startNode);

        while(setO.Count> 0)
        {
            NODE current = setO[0];
            for (int i = 1; i < setO.Count; i++)
            {
                if (setO[i].fCost<current.fCost || setO[i].fCost == current.fCost && setO[i].h < current.h)
                {
                    current = setO[i];
                }
            }
            setO.Remove(current);
            setClosed.Add(current);

            if (current == targettNode)
            {
                retrace(startNode,targettNode);
                return;
            }
            foreach (NODE neighbour in grid.getNaighbours(current))
            {
                if(!neighbour.walkable || setClosed.Contains(neighbour))
                {
                    continue;
                }
                int MovementCost = current.G + GetDistance(current, neighbour);
                if(MovementCost<neighbour.G|| !setO.Contains(neighbour))
                {
                    neighbour.G = MovementCost;
                    neighbour.h = GetDistance(neighbour, targettNode);
                    neighbour.parent = current;
                    if (!setO.Contains(neighbour))
                    {
                        setO.Add(neighbour);
                    }
                }
            }
        }
    }

    void retrace(NODE start, NODE end)
    {
        List<NODE> path = new List<NODE>();
        NODE current = end;
        while(current != start)
        {
            path.Add(current);
            current = current.parent;
        }
        path.Reverse();
        grid.path=path;
    }

    int GetDistance(NODE one, NODE two)
    {
        int dX = Mathf.Abs(one.gridX - two.gridX);
        int dY = Mathf.Abs(one.gridY - two.gridY);

        if (dX > dY)
        {
            return 14 * dY + 10 * (dX - dY);
        }
        return 14 * dX + 10 * (dY - dX);
    }

}
