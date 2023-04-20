using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent enemyNavMesh;
    private Transform playerTransform;
    private bool followPlayer;

    private void Awake()
    {
        enemyNavMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            enemyNavMesh.SetDestination(playerTransform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerTransform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
        if(other.tag == "Player")
        {
            followPlayer = true;
        }
    }
}
