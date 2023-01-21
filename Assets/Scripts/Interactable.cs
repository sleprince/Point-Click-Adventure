using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    //depricated
    //[SerializeField] Actions[] actions;

    [SerializeField] Actions[] messageActions;
    [SerializeField] Actions[] inspectActions;
    [SerializeField] Actions[] activateActions;
    [SerializeField] Actions[] itemActions;


    [SerializeField] float distancePosition = 1f; //how far away from interactable player stops.

    NavMeshAgent agent;

    PlayerScript pScript;

    void Start()
    {

        agent = PlayerScript.FindObjectOfType<NavMeshAgent>();
        pScript = PlayerScript.FindObjectOfType<PlayerScript>();


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
        
            //depricated 
            /*
            Actions[] inspect = actions.OfType<InspectAction>().ToArray();
            Actions[] message = actions.OfType<MessageAction>().ToArray();
            Actions[] use = actions.OfType<ActivateAction>().ToArray();
            Actions[] pickUp = actions.OfType<ItemAction>().ToArray();
            
            Actions[] animate = actions.OfType<AnimateAction>().ToArray();
            */
            
            
            //_playerScript.I values and what cursor type they mean
            //0 = Look
            //1 = Talk
            //2 = Pick Up/Give
            //3 = Walk/Use
            
            //creates an array of arrays
            Actions[][] allActions = new Actions[5][];

            allActions[0] = inspectActions;
            allActions[1] = messageActions;
            allActions[2] = itemActions;
            allActions[3] = activateActions;
            

            for (int i = 0; i < allActions.Count(); i++) //so i = 0 to 3
            {
                

                if (pScript.I == i) // _playerScript.I is the int that determines which type of mouse cursor is being used
                {
                    for (int j = 0; j < allActions[i].Count(); j++) //the count of actions for that category
                    {
                        if (allActions[i][j] != null) //if there are any of the type of actions
                        {
                            allActions[i][j].Act(); //do the actions

                        }
                    }
                }
                
                
            }
            
    }

    /*
    [System.Serializable]
    public class ActionTypes
    {
        [SerializeField] MessageAction[] messageActions;
        [SerializeField] InspectAction[] inspectActions;
        [SerializeField] ActivateAction[] ActivateAction;
        [SerializeField] ItemAction[] ItemAction;
    }
    */

}//class end
