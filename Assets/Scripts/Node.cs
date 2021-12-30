using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node: IHeapItem<Node>
{
    public bool walkable;
    public Vector3 worldPoint;
    public Node parent;
    public Node next;

    public int gCost;
    public int hCost;

    public int gridX;
    public int gridY;

    int heapIndex;

    public Node(bool _walkable, Vector3 _worldPoint, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPoint = _worldPoint;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return hCost + gCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node node)
    {
        int compare = fCost.CompareTo(node.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(node.hCost);
        }
        return -compare;
    }

}
