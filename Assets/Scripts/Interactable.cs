using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    [SerializeField] Actions[] actions;

    //public List<ActionTypes> actionTypes = new List<ActionTypes>(4); 
    
    [SerializeField] MessageAction[] messageActions;
    [SerializeField] InspectAction[] inspectActions;
    [SerializeField] ActivateActions[] activateActions;
    [SerializeField] ItemActions[] itemActions;


    [SerializeField] float distancePosition = 1f; //how far away from interactable player stops.

    NavMeshAgent agent;

    [SerializeField] private PlayerScript _playerScript;

    void Start()
    {

        agent = PlayerScript.FindObjectOfType<NavMeshAgent>();


    }

    public Vector3 InteractPosition()
    {
        //returns where the interactable is located in the scene.
        return transform.position + transform.forward * distancePosition;
        //transform forward is where the blue transform arrow (z axis) is pointing on the interactable.
    }

    public void Interact(PlayerScript player)
    {
        Debug.Log(gameObject.name + " clicked by player");

        StartCoroutine(WaitforPlayerArriving(player));
    }

    //sending the script over to a function seems to be a good alternative to getinstance() I've been doing.
    IEnumerator WaitforPlayerArriving(PlayerScript player)
    {
        while(!player.CheckIfArrived())
        {
            yield return null; //delays the coroutine while they haven't arrived.
        }

        //it will only run the code below when the player arrives.
        Debug.Log("Player arrived");

        //move player back here

        player.Agent.destination = agent.destination + (new Vector3(0, 0, 0.2f));

        player.SetDirection(transform.position);



        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act();
        }

        //0 = Look
        //1 = Talk
        //2 = Use
        //3 = Walk/pick up
        
        
        if (_playerScript.I == 0)
        {
            for (int i = 0; i < inspectActions.Length; i++)
            {
                inspectActions[i].Act();
            }
        }
        
        if (_playerScript.I == 1)
        {
            for (int i = 0; i < messageActions.Length; i++)
            {
                messageActions[i].Act();
            }
        }
        
    }

    /*
    [System.Serializable]
    public class ActionTypes
    {
        [SerializeField] MessageAction[] messageActions;
        [SerializeField] InspectAction[] inspectActions;
        [SerializeField] ActivateActions[] activateActions;
        [SerializeField] ItemActions[] itemActions;
    }
    */
    
}//class end
