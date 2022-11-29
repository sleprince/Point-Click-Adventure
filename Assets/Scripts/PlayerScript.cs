using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //needed to use Unity's AI functions.

public class PlayerScript : MonoBehaviour
{

    private NavMeshAgent agent;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        //getting the reference to the NavMeshAgent in Unity Editor that's attached
        //to the player.
        agent = GetComponent<NavMeshAgent>();

        //getting the reference to the MainCamera in the scene.
        //need to make sure the camera is tagged as MainCamera in editor.
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //if left mouse button clicked.
            //0 is left, 1 is right, 2 is middle.
        {
            OnClick();
        }
    }

    void OnClick()
    {
        Debug.Log("Left mouse button clicked!");

        //variable of type RaycastHit called hit that contains data about where the
        //raycast hit.
        RaycastHit hit; 

        //camToScreen is a variable of type Ray which is the Ray being cast.
        //ScreenPointToRay, this is from our screen position, it shoots a ray onto our
        //scene, that ray is called CamToScreen
        Ray camToScreen = mainCamera.ScreenPointToRay(Input.mousePosition);


        //Physics.Raycast, this is a boolean so if the ray hits something all of this
        //code will execute.
        //we pass in our ray, we use Infinity to cover the maximum distance and
        //out passes all the information into hit that is used below such as hit.point
        if (Physics.Raycast(camToScreen, out hit, Mathf.Infinity))
        {
            //if raycast hits something move player.
            if (hit.collider != null)
            {
                //temporary variable interactive is the Interactable class script that is attached to the the 
                //object the raycast is hitting.
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                //if the player has clicked on an interactable beacuse it's not null.
                if (interactable != null)
                {
                    //move player to the interactable *first.
                    MovePlayer(interactable.InteractPosition());
                    interactable.Interact(this); //can use "this" because we are sending this playerscript over.
                }
                else
                {   //the hit point is sent over as targetPosition.
                    MovePlayer(hit.point); //hit.point will be where the player moves to.
                }
            }

        }

    }

    public bool CheckIfArrived()
    {
        //true or false that there is no path pending and the agent has arrived.
        return (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
    }

    void MovePlayer(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);

    }
}
