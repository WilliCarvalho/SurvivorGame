using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent enemyNavMesh;
    [SerializeField] private Transform playerTransform;

    private void Awake()
    {        
        enemyNavMesh = GetComponent<NavMeshAgent>();
        enemyNavMesh.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyNavMesh.isStopped /*enemyNavMesh.isStopped == false*/)
        {
            enemyNavMesh.SetDestination(playerTransform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyNavMesh.isStopped = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemyNavMesh.isStopped = true;
    }
}
