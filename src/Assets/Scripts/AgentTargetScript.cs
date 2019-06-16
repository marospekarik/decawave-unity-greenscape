using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentTargetScript : MonoBehaviour
{
    public Transform target;
    private Vector3 startingPos;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startingPos = agent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(agent.transform.position, target.transform.position) <= 3)
        {
            agent.transform.position = startingPos;
        } else
        {
            agent.SetDestination(target.position);
        }
    }
}
