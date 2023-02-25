using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    private enum State {
        Roaming,
        ChaseTarget,
        //ShootingTarget,
        GoingBackToStart,
    }

    //private IAimShootAnims aimShootAnims;
    //private EnemyPathfindingMovement pathfindingMovement;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    //private float nextShootTime;
    private State state;

    private NavMeshAgent agent;
    private NavMeshHit navMeshHit;
    public float speed = 1.0f;
    //private bool gameOn = true;
    //private bool rotated;
    private Vector3 destination;

    private void Awake() {
        //pathfindingMovement = GetComponent<EnemyPathfindingMovement>();
        //aimShootAnims = GetComponent<IAimShootAnims>();
        state = State.Roaming;

        agent = GetComponent<NavMeshAgent>();
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        destination = GetRandomPositionOnNavMesh();
        agent.SetDestination(destination);

    }

    private void Start() {
        //startingPosition = transform.position;
        //roamPosition = GetRoamingPosition();
    }

    private void Update() {
        switch (state) {
        default:
        case State.Roaming:

                //agent.SetDestination(roamPosition);

                //agent.Move(transform.forward);

                
                float reachedPositionDistance = 10f;
                /*
                if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) {
                 //Reached Roam Position
                    roamPosition = GetRoamingPosition();
                 }
                */

                if (agent.remainingDistance < 0.5f)
                {
                    destination = GetRandomPositionOnNavMesh();
                    agent.SetDestination(destination);
                }


                FindTarget();
                break;
        case State.ChaseTarget:
            agent.destination = PlayerScript.GetInstance().Agent.transform.position;


                float catchRange = 5f;
            if (Vector3.Distance(transform.position, PlayerScript.GetInstance().Agent.transform.position) < catchRange) {
                    // Target within catch range

                Debug.Log("You got caught.");

                
            }

            float stopChaseDistance = 15f;
            if (Vector3.Distance(transform.position, PlayerScript.GetInstance().Agent.transform.position) > stopChaseDistance) {
                // Too far, stop chasing
                state = State.Roaming;
            }
            break;

                /*
        case State.GoingBackToStart:
                agent.destination = startingPosition;
            
            reachedPositionDistance = 10f;
            if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance) {
                // Reached Start Position
                state = State.Roaming;
            }
            break;
                */
        }
    }


    private Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20.0f;
        randomDirection += transform.position;
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(randomDirection, out navMeshHit, 20.0f, NavMesh.AllAreas);
        return navMeshHit.position;
    }

    private Vector3 GetRoamingPosition() {
        return startingPosition + UtilsClass.GetRandomDir() * Random.Range(10f, 70f);
    }

    private void FindTarget() {
        float targetRange = 15f;


        if (Vector3.Distance(transform.position, PlayerScript.GetInstance().Agent.transform.position) < targetRange) {
            // Player within target range
            state = State.ChaseTarget;
        }
    }

}
