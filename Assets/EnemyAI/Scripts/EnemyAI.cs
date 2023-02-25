using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    private enum State {
        Roaming,
        ChaseTarget
    }

    private Vector3 startingPosition;
    private State state;

    private NavMeshAgent agent;
    public float speed = 1.0f;
    private Vector3 destination;

    public GameManager game;

    private void Awake() {

        state = State.Roaming;

        agent = GetComponent<NavMeshAgent>();
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        destination = GetRandomPositionOnNavMesh();
        agent.SetDestination(destination);

    }

    private void Start() {

        //use a spawnPos
        //startingPosition = PlayerScript.GetInstance().Agent.transform.position;

    }

    private void Update() {
        switch (state) {
        default:
        case State.Roaming:         

                if (agent.remainingDistance < 0.5f)
                {
                    destination = GetRandomPositionOnNavMesh();
                    agent.SetDestination(destination);
                }

                FindTarget();
                break;
        case State.ChaseTarget:
            agent.destination = PlayerScript.GetInstance().Agent.transform.position;


                float catchRange = 2f;
            if (Vector3.Distance(transform.position, PlayerScript.GetInstance().Agent.transform.position) < catchRange) {
                    // Target within catch range

                    //Debug.Log("You got caught.");
                    game.Lose();
                    PlayerScript.GetInstance().Agent.transform.position = startingPosition;

            }

            float stopChaseDistance = 7f;
            if (Vector3.Distance(transform.position, PlayerScript.GetInstance().Agent.transform.position) > stopChaseDistance) {
                // Too far, stop chasing
                state = State.Roaming;
            }
            break;
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

    private void FindTarget() {
        float targetRange = 15f;


        if (Vector3.Distance(transform.position, PlayerScript.GetInstance().Agent.transform.position) < targetRange) {
            // Player within target range
            state = State.ChaseTarget;
        }
    }

}
