using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Edge> edgeList = new List<Edge>();
    public Node path = null;
    public float f, g, h;
    public Node cameFrom;

    public GameObject id;

    // h = cost to goal , g = cost from start , f = g + h


    public Node(GameObject obj)
    {
        id = obj;
        path = null;
    }
    public GameObject getId()
    {
        return id;
    }
}
