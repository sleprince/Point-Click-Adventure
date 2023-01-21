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
    [SerializeField] Actions[] actions;

    //public List<ActionTypes> actionTypes = new List<ActionTypes>(4); 
    
    [SerializeField] MessageAction[] messageActions;
    [SerializeField] InspectAction[] inspectActions;
    [SerializeField] ActivateAction[] ActivateAction;
    [SerializeField] ItemAction[] ItemAction;


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


        //string[] action = new string[4] {"InspectAction", "MessageAction", "ActivateAction", "ItemAction"};


       // for (int i = 0; i < actions.Count(); i++)
        //{
            //actions[i].Act();
            
            //_playerScript.I values and what cursor type they mean
            //0 = Look
            //1 = Talk
            //2 = Use/Give
            //3 = Walk/pick up

            Actions[] inspect = actions.OfType<InspectAction>().ToArray();
            Actions[] message = actions.OfType<MessageAction>().ToArray();
            Actions[] use = actions.OfType<ActivateAction>().ToArray();
            Actions[] pickUp = actions.OfType<ItemAction>().ToArray();
            
            Actions[] animate = actions.OfType<AnimateAction>().ToArray();
            

            //creates an array of arrays
            Actions[][] allActions = new Actions[5][];

            allActions[0] = inspect;
            allActions[1] = message;
            allActions[2] = use;
            allActions[3] = pickUp;
            
            allActions[4] = animate;

            for (int i = 0; i < allActions.Count(); i++)
            {
                

                if (pScript.I == i) // _playerScript.I is the int that determines which type of mouse cursor is used
                {
                    for (int j = 0; j < allActions[i].Count(); j++)
                    {
                        if (allActions[i][j] != null) //if there are any of the type of actions
                        {
                            allActions[i][j].Act();

                            //animations if present, can trigger on any of the mouse cursors
                            //allActions[4][j].Act();
                        }
                    }
                }
                
                
            }
            
            //run remaining actions here if there are any. animate action can happen on any if it's there.
            //for (int i = 0; i < allActions[4].Count(); i++)
            //{
           //     actions[i].Act();
           // }

            

            //}

        /*public enum ArrayNames{
            InspectAction,
            MessageAction,
            ActivateAction,
            ItemAction
        }
    */





       

        //string[] action = new string[4] {"inspectActions", "messageActions", "ActivateAction", "ItemAction"};
        
        //string action1 = inspectActions[0].ToString();
        //action[0].Act();
        
        //MethodInfo acts = inspectActions[0].GetType().GetMethod("Act");
        
        //acts.Invoke(inspectActions[0], null);
        
        
        //$"{action[0]}"[0].
        
        /*
        Debug.Log(action[0]);

        for (int j = 0; j < action.Length; j++)
        {

            if (_playerScript.I == j)
            {
                for (int i = 0; i < $"{action[j]}".Length; i++)
                {
                    //$"{action[j]}"[i].Invoke("")
                    //inspectActions[i].Act();

                    MethodInfo acts = $"{action[j]}"[i].GetType().GetMethod("Act");

                    acts.Invoke($"{action[j]}"[i], null);



                }
            }

        }
            */
    
        /*
        if (_playerScript.I == 1)
        {
            for (int i = 0; i < messageActions.Length; i++)
            {
                messageActions[i].Act();
            }
        }
        */
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
    


        
    //Samething but use a Dictionary instead of an array and enums instead of strings
    public enum ArrayNames{
        
        message,
        inspect
    }
    
}//class end
