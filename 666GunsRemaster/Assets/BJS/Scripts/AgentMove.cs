using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    NavMeshAgent agent;
    public float chaseSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        //SetTargetPosition();
        SetAgentPosition();
    }

    void SetTargetPosition()
    {
        if (Input.GetMouseButton(0))
        {
            //target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void SetAgentPosition()
    {
        if (Vector3.Distance(this.transform.position, target.transform.position) < 8.0f)
        {
            agent.SetDestination(new Vector3(target.transform.position.x, target.transform.position.y,
                transform.position.z));
        }
    }
}
