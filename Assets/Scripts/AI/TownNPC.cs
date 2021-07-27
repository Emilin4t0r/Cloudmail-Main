using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TownNPC : MonoBehaviour {
    
    public GameObject shipFloor;
    NavMeshAgent agent;
    Bounds areaBounds;
    bool navigating;

    private void Start() {
        areaBounds = shipFloor.GetComponent<BoxCollider>().bounds;
        agent = transform.GetComponent<NavMeshAgent>();
        GetNewDestination();        
    }

    private void FixedUpdate() {
        if (Vector3.Distance(transform.position, agent.destination) < 1f && !navigating) {            
            print("Waiting");
            StartCoroutine(WaitAndMove());
        }
        Debug.DrawLine(transform.position, agent.destination, Color.green, 0.1f);
    }

    IEnumerator WaitAndMove() {
        navigating = true;
        float randSecs = Random.Range(1f, 3f);
        yield return new WaitForSeconds(randSecs);
        GetNewDestination();
    }

    void GetNewDestination() {
        navigating = false;
        float randomPosX = Random.Range(-areaBounds.extents.x * 0.8f, areaBounds.extents.x * 0.8f);
        float randomPosZ = Random.Range(-areaBounds.extents.z * 0.8f, areaBounds.extents.z * 0.8f);
        agent.SetDestination(new Vector3(randomPosX, 1, randomPosZ));
        print("Moving to new position");
    }
}
