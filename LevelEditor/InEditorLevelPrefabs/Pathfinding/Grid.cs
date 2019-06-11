using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public Vector2 GridWorldSize;
    public float NodeRadius;
    Node[,] grid;
    public LayerMask unwalkableMask;

    public List<Node> path;
    public List<Vector3> pathVector;
    float nodediameter;
    int GridSizeX, GridSizeY;

    void Start()
    {
        nodediameter = NodeRadius * 2;
        GridSizeX = Mathf.RoundToInt(GridWorldSize.x / nodediameter);
        GridSizeY = Mathf.RoundToInt(GridWorldSize.y / nodediameter);

        CreateGrid();
    }

    public List<Node> ReturnNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                int CheckX = node.gridX + x;
                int CheckY = node.gridY + y;

                if (CheckX >= 0 && CheckX < GridSizeX && CheckY >= 0 && CheckY < GridSizeY)
                {
                    neighbours.Add(grid[CheckX, CheckY]);
                }
            }

        }
        return neighbours;
    }

    void CreateGrid()
    {
        grid = new Node[GridSizeX, GridSizeY];
        Vector3 WorldBottomleft = transform.position - Vector3.right * GridSizeX / 2 - Vector3.forward * GridWorldSize.y / 2;
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Vector3 Worldpoint = WorldBottomleft + Vector3.right * (x * nodediameter + NodeRadius) + Vector3.forward * (y * nodediameter + NodeRadius);
                bool walkable = !(Physics.CheckSphere(Worldpoint, NodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, Worldpoint, x, y);
            }
        }


    }

    public Node NodeFromWorldPoint(Vector3 WorldPosition)
    {
        float percentX = (WorldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float percentY = (WorldPosition.z + GridWorldSize.y / 2) / GridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((GridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((GridSizeY - 1) * percentY);
        return grid[x, y];

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));

        if (grid != null)
        {

            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                if (path != null)
                {
                    if (path.Contains(n))
                        Gizmos.color = Color.black;
                }
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (nodediameter - 0.1f));

            }
        }

    }

}
