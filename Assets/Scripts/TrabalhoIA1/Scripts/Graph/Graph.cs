using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<Edge> edges = new List<Edge>();
    List<Node> nodes = new List<Node>();

    public List<Node> pathList = new List<Node>();

    public Graph() { }

    public void AddNode(GameObject id)
    {
        Node node = new Node(id);
        nodes.Add(node);
    }
    public void AddEdge(GameObject fromNode , GameObject toNode )
    {
        Node from = FindNode(fromNode);
        Node to = FindNode(toNode);

        if(from != null && to != null)
        {
            Edge edge = new Edge(from, to);
            edges.Add(edge);
            from.edgeList.Add(edge);
        }
    }
    public GameObject getPathPoint(int index)
    {
        return pathList[index].getId();
    }
    Node FindNode(GameObject id)
    {
        foreach (Node node in nodes)
        {
            if(node.getId() == id) return node;
        }
        return null;
    }

    public bool AStar(GameObject startId, GameObject endId)
    {
        Node start = FindNode(startId);
        Node end = FindNode(endId);

        if(start == null || end == null) return false;

        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();

        float tentative_g_score = 0; // distance go so far
        bool tentative_is_better;

        start.g = 0;
        start.h = distance(start,end);
        start.f = start.h;

        open.Add(start);

        while (open.Count > 0)
        {
            int i = lowestNode(open);
            Node thisNode = open[i];

            if (thisNode.getId() == endId)
            {
                ReconstructPath(start,end);
                return true;
            }

            open.RemoveAt(i);
            closed.Add(thisNode);
            Node neighbour;

            foreach (Edge edge in thisNode.edgeList)
            {
                neighbour = edge.lastNode;

                if (closed.IndexOf(neighbour) > -1) continue;

                tentative_g_score = thisNode.g + distance(thisNode,neighbour);
                if(open.IndexOf(neighbour) == -1)
                {
                    open.Add(neighbour);
                    tentative_is_better = true;
                }
                else if (tentative_g_score < neighbour.g)
                {
                    tentative_is_better = true;
                }
                else tentative_is_better = false;

                if(tentative_is_better)
                {
                    neighbour.cameFrom = thisNode;
                    neighbour.g = tentative_g_score;
                    neighbour.h = distance(thisNode,end);
                    neighbour.f = neighbour.g + neighbour.h;
                }
            }
        }
        return false;
    }

    //Heuristic below
    public void ReconstructPath(Node startId, Node endId)
    {
        pathList.Clear();
        pathList.Add(endId);
        Node path = endId.cameFrom;
        while (path != startId && path!= null)
        {
            pathList.Insert(0, path);
            path = path.cameFrom;
        }
        pathList.Insert(0, startId);
    }
    float distance(Node a, Node b)
    {
        return (Vector3.SqrMagnitude(a.getId().transform.position - b.getId().transform.position));
    }
    int lowestNode(List<Node> lowestNode)
    {
        float lowestN = 0;
        int count = 0;
        int iteratorCount = 0;

        lowestN = lowestNode[0].f;

        for (int i = 1; i < lowestNode.Count; i++)
        {
            if (lowestNode[i].f < lowestN) // if the code not work put <= there.
            {
                lowestN = lowestNode[i].f;
                iteratorCount = count;
            }
            count ++;
        }
        return iteratorCount; // return the node with the lowest value
    }
}
