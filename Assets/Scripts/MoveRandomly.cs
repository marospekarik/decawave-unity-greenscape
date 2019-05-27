using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveRandomly : MonoBehaviour
{
    public float timeforNewPath;
    public int step = 20;
    NavMeshAgent nav;
    NavMeshPath path;
    bool inCoRoutine;
    bool validPath;
    Vector3 target;
    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }
    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-step, step);
        float z = Random.Range(-step, step);
        Vector3 pos = new Vector3(x, 0, z);
        return pos;

    }
    IEnumerator DoSomething()
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(timeforNewPath);
        GetNewPath();
        validPath = nav.CalculatePath(target, path);
        if (!validPath)
        {
            Debug.Log("Found an invalid path");
            yield return new WaitForSeconds(1);

        }
        while (!validPath)
        {
            yield return new WaitForSeconds(1);
            GetNewPath();
            validPath = nav.CalculatePath(target, path);
        }
        inCoRoutine = false;

    }
    void GetNewPath()
    {
        target = getNewRandomPosition();
        nav.SetDestination(getNewRandomPosition());
    }
    // Update is called once per frame
    void Update()
    {
        if(!inCoRoutine)
        {
            StartCoroutine(DoSomething());
        }
    }
}
