using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    [System.Serializable]
    public struct Link
    {
        public enum direction { Uni , Bi}
        public GameObject node1;
        public GameObject node2;
        public direction dir;
    }
    public GameObject[] waypoints;
    public Link[] links;

    public Graph graph = new Graph();
    void Start()
    {
        if(waypoints.Length> 0)
        {
            foreach(GameObject waypoint in waypoints)
            {
                graph.AddNode(waypoint);
            }
            foreach (Link link in links)
            {
                graph.AddEdge(link.node1, link.node2);

                if(link.dir == Link.direction.Bi)
                {
                    graph.AddEdge(link.node2, link.node1);
                }
            }
        }
    }
}
