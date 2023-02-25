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
        startingPosition = transform.position;
        //roamPosition = GetRoamingPosition();
    }

    private void Update() {
        switch (state) {
        default:
        case State.Roaming:

            //agent.transform.Translate(roamPosition);

            float reachedPositionDistance = 10f;
                //if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) {
                // Reached Roam Position
                //    roamPosition = GetRoamingPosition();
                // }



                //StartCoroutine("SamplePos");

                if (agent.remainingDistance < 0.5f)
                {
                    destination = GetRandomPositionOnNavMesh();
                    agent.SetDestination(destination);
                }



                //transform.Translate(Vector3.forward * speed * Time.deltaTime);

                FindTarget();
                break;
        case State.ChaseTarget:
            agent.transform.Translate(PlayerScript.GetInstance().Agent.transform.position);


            float attackRange = 30f;
            if (Vector3.Distance(transform.position, PlayerScript.GetInstance().Agent.transform.position) < attackRange) {
                    // Target within attack range

                Debug.Log("You got caught.");

                
            }

            float stopChaseDistance = 80f;
            if (Vector3.Distance(transform.position, PlayerScript.GetInstance().Agent.transform.position) > stopChaseDistance) {
                // Too far, stop chasing
                state = State.GoingBackToStart;
            }
            break;
        case State.GoingBackToStart:
            agent.transform.Translate(startingPosition);
            
            reachedPositionDistance = 10f;
            if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance) {
                // Reached Start Position
                state = State.Roaming;
            }
            break;
        }
    }

    /*IEnumerator SamplePos()
    {
        while (gameOn)
        {

            Vector3 moveDirection = transform.forward * speed * Time.deltaTime;

            if (NavMesh.SamplePosition(transform.position + moveDirection, out navMeshHit, 0.1f, NavMesh.AllAreas))
            {
                yield return new WaitForSeconds(3);

                rotated = false;

                if (!rotated)
                {
                    transform.Rotate(0, Random.Range(0, 180), 0);
                    rotated = true;

                    yield return new WaitForSeconds(3);
                }



            }
            if (rotated)
                transform.Translate(Vector3.forward * speed * Time.deltaTime);


            


        }



    }

    */

    private Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10.0f;
        randomDirection += transform.position;
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(randomDirection, out navMeshHit, 10.0f, NavMesh.AllAreas);
        return navMeshHit.position;
    }

    //private Vector3 GetRoamingPosition() {
    //    return startingPosition + UtilsClass.GetRandomDir() * Random.Range(10f, 70f);
   // }

    private void FindTarget() {
        Vector3 targetRange = new Vector3(50f,0,50f);


        if (Vector3.Distance(transform.position, PlayerScript.GetInstance().Agent.transform.position) < targetRange.magnitude) {
            // Player within target range
            state = State.ChaseTarget;
        }
    }

}
