using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    Transform goal;
    float speed = 5f;
    float accuracy = 2f;
    float rotSpeed = 2f;

    public GameObject wpManager;
    GameObject[] waypoints;
    GameObject currentNode;
    int currentWp = 0;

    Graph graph;

    private void Start()
    {
        waypoints = wpManager.GetComponent<WayPointManager>().waypoints;
        graph = wpManager.GetComponent<WayPointManager>().graph;
        currentNode = waypoints[0];

        Invoke("GoToPoint5", 2);
    }

    public void GoToStart()
    {
        graph.AStar(currentNode, waypoints[0]);
        currentWp = 0;
    }
    public void GoToPoint5()
    {
        graph.AStar(currentNode, waypoints[4]);
        currentWp = 0;
        Debug.Log("go");
    }
    private void LateUpdate()
    {
        if (graph.pathList.Count == 0 || currentWp == graph.pathList.Count) return; // You are in the end of the path.

        currentNode = graph.getPathPoint(currentWp);

        if (Vector3.Distance(graph.pathList[currentWp].getId().transform.position , this.transform.position) < accuracy)
        {
            currentWp++;
        }

        if(currentWp < graph.pathList.Count)
        {
            goal = graph.pathList[currentWp].getId().transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);

            Vector3 direction = lookAtGoal - this.transform.position;
            transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}

/*
1 <-> 2
2 <-> 3
2 <-> 8
3 <-> 4
4 <-> 5
5 <-> 7
7 <-> 3
8 <-> 9
9 <-> 10
11 <-> 12
 * */
