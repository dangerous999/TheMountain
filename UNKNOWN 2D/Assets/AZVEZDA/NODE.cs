using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NODE{

    public bool walkable;
    public Vector3 position;
    public int G;
    public int h;
    public int gridX;
    public int gridY;
    public NODE parent;

    public NODE(bool _walkable,Vector3 _position, int _gridX, int _gridY)
    {
        walkable = _walkable;
        position = _position;
        gridX= _gridX;
        gridY= _gridY;
}
    public int fCost
    {
        get
        {
            return G + h;
        }
    }
}
