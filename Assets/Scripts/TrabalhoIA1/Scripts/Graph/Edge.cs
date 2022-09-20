using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public Node firstNode;
    public Node lastNode;

    public Edge(Node from, Node to)
    {
        firstNode = from;
        lastNode = to;
    }
}
