using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {
    Grid grid;
    public Transform seeker;
    public Transform target;
    public List<Vector3> PathLocations;
    

    public List<Vector3> ReturnPath()
    {
        return PathLocations;
    }
    void Awake()
    {
        grid = GetComponent<Grid>();
    }
  public  void FindPath(Vector3 StartPos, Vector3 TargetPos)
    {
        Node startnode = grid.NodeFromWorldPoint(StartPos);
        Node targetnode = grid.NodeFromWorldPoint(TargetPos);

        List<Node> Openset = new List<Node>();
        HashSet<Node> ClosedSet = new HashSet<Node>();

        Openset.Add(startnode);

        while(Openset.Count > 0)
        {
            Node CurrentNode = Openset[0];
            for(int o = 1; o < Openset.Count; o++)
            {
                if(Openset[o].fCost() < CurrentNode.fCost() || Openset[o].fCost() == CurrentNode.fCost() && Openset[o].Hcost < CurrentNode.Hcost)
                {
                    CurrentNode = Openset[o];
                }
            }
            Openset.Remove(CurrentNode);
            ClosedSet.Add(CurrentNode);
            if(CurrentNode == targetnode)
            {

                GeneratePath(startnode, targetnode);
                return;
            }
            foreach(Node neighbour in grid.ReturnNeighbours(CurrentNode))
            {
                if(!neighbour.walkable || ClosedSet.Contains(neighbour))
                {
                    continue;
                }
                int NewMovementCostToNeighbour = CurrentNode.Gcost + ReturnDistance(CurrentNode, neighbour);
                if(NewMovementCostToNeighbour < neighbour.Gcost||!Openset.Contains(neighbour))
                {
                    neighbour.Gcost = NewMovementCostToNeighbour;
                    neighbour.Hcost = ReturnDistance(neighbour, targetnode);
                    neighbour.parent = CurrentNode;
                    if (!Openset.Contains(neighbour))
                        Openset.Add(neighbour);

                        

                }
            }
            
        }
    }
	private List<Vector3> GeneratePath(Node start,Node end)
    {
        List<Vector3> path = new List<Vector3>();
        Node currentnode = end;

        while(currentnode != start)
        {
            path.Add(currentnode.WorldPosition);
         
            currentnode = currentnode.parent;
        }
        path.Reverse();
        PathLocations = path;
        return path;
    }


   int ReturnDistance(Node nodeA,Node nodeB)  
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(distX >distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);
    }
}
