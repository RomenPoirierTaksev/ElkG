using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GameObject target;

    GameObject[] enemies;

    WorldGrid grid;

    void Awake()
    {   
        grid = GetComponent<WorldGrid>();
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        target = GameObject.FindGameObjectWithTag("Player");

        foreach(GameObject e in enemies)
        {
            if(target != null && e != null)
            {
                List<Node> path = FindPath(e.transform.position, target.transform.position);
                EnemyMovement movement = e.GetComponent<EnemyMovement>();
                if(path != null && path.Count > 0)
                {
                    movement.path = path;
                    movement.startNode = path[0];
                }
            }
        }
    }
    List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.nodeFromWorldPoint(startPos);
        Node targetNode = grid.nodeFromWorldPoint(targetPos);

        Heap<Node> openSet = new Heap<Node>(grid.maxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.add(startNode);

        while(openSet.count > 0)
        {
            Node currentNode = openSet.removeFirst();
            closedSet.Add(currentNode);

            if(currentNode == targetNode)
            {
                return getPath(startNode, targetNode);
            }

            foreach(Node n in grid.getNeighbours(currentNode))
            {
                if(!n.walkable || closedSet.Contains(n))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, n);
                if(newMovementCostToNeighbour < n.gCost || !openSet.Contains(n))
                {
                    n.gCost = newMovementCostToNeighbour;
                    n.hCost = getDistance(n, targetNode);
                    n.parent = currentNode;

                    if (!openSet.Contains(n))
                    {
                        openSet.add(n);
                    }
                }
            }
        }
        return new List<Node>();
    }

    int getDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(dstX > dstY)
        {
            return 14*dstY + 10*(dstX - dstY);
        }
        else
        {
            return 14*dstX + 10*(dstY - dstX);
        }
    }

    List<Node> getPath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode.parent.next = currentNode;
            currentNode = currentNode.parent;
        }
        path.Add(startNode);
        path.Reverse();
        return path;
    }
}
