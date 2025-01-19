using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class NavAgentTest : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject point;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(new Vector3(point.transform.position.x, 0, point.transform.position.z));
    }
}
