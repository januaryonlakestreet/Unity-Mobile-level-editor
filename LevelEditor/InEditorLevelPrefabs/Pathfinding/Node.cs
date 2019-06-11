using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node  {
    public bool walkable;
    public Vector3 WorldPosition;
    public Node parent;
    public int Gcost;
    public int Hcost;
    public int gridX;
    public int gridY;
    public int fCost()
    {
        return Gcost + Hcost;
    }
    public Node(bool _walkable,Vector3 _worldpos, int _gridX,int _gridY)
    {
        gridX = _gridX;
        gridY = _gridY;
        walkable = _walkable;
        WorldPosition = _worldpos;
    }
}
