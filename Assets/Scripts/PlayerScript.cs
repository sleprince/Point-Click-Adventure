using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI; //needed to use Unity's AI functions.

public class PlayerScript : MonoBehaviour
{

    private NavMeshAgent agent;
    private Camera mainCamera;

    private bool turning; //are they turning.
    private Quaternion targetRot; //rotation value of the target.

    private LineRenderer line; //to give a pseudo visual of what the raycast is doing.

    private PlayerAnimation playerAnim = new PlayerAnimation(); //with this way of doing it we don't need to attach the
    //animator in the editor.

    [SerializeField] private Animator pAnim;

    [SerializeField] private ParticleSystem effect; //an effect that will occur when the player clicks somewhere.

    public NavMeshAgent Agent { get { return agent; } } //public getter.
    
    public Texture2D cursorTexture; //these 3 parameters are for custom mouse cursor for look, pick up etc.
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private int i = -1;

    public List<MouseOptions> mouseOptions = new List<MouseOptions>();

    public int I { get { return i; } }
    /*
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        //getting the reference to the NavMeshAgent in Unity Editor that's attached
        //to the player.
        agent = GetComponent<NavMeshAgent>();

        //getting the reference to the MainCamera in the scene.
        //need to make sure the camera is tagged as MainCamera in editor.
        mainCamera = Camera.main;

        line = GetComponent<LineRenderer>();
        
        playerAnim.Init(GetComponentInChildren<Animator>()); //will search for components of type animator, since it's a
        //method we need to add parenthesis.
        
        //playerAnim.Init(pAnim);
        
        Cursor.SetCursor(mouseOptions[3].cursor, hotSpot, cursorMode); //set cursor to walk to start with.

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) //right mouse button to cycle between different cursors for different action types.
        {
            i++;
            if (i == 4)
                i = 0;
            Cursor.SetCursor(mouseOptions[i].cursor, hotSpot, cursorMode);
            

        }
        
        
        if (Input.GetMouseButtonDown(0) && !DialogueSystem.Instance.conversing) //if left mouse button clicked.
            //0 is left, 1 is right, 2 is middle.
        {
            OnClick();
        }

        if (turning && transform.rotation != targetRot) //if turning and not rotated towards the target.
        {
            //transform towards the target.
            //15f x time is so that the rotation is smooth all different spec PCs with different framerates.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 15f * Time.deltaTime);
        }
        
        //playerAnim
        playerAnim.UpdateAnimation(agent.velocity.sqrMagnitude); //to get float of movement speed from agent, don't need
        //magnitude normal version as sqr is near enough.
    }

    void OnClick()
    {
        Debug.Log("Left mouse button clicked!");

        //variable of type RaycastHit called hit that contains data about where the
        //raycast hit.
        RaycastHit hit; 

        //camToScreen is a variable of type Ray which is the Ray being cast.
        //ScreenPointToRay, this is from our screen position (mouse clicked position), it shoots a ray onto our
        //scene, that ray is called CamToScreen. Ray starts at camera and goes to mouse position.
        Ray camToScreen = mainCamera.ScreenPointToRay(Input.mousePosition);




        //Physics.Raycast, this is a boolean so if the ray hits something all of this
        //code will execute.
        //we pass in our ray, we use Infinity to cover the maximum distance and
        //out passes all the information into hit that is used below such as hit.point
        if (Physics.Raycast(camToScreen, out hit, Mathf.Infinity))
        {

            effect.transform.position = hit.point; //move particle effect to ray hit point and play.
            effect.Play();


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

                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);
                Invoke("DeleteLine", 0.5f);


            }

            else
            {
                effect.Stop();
                line.enabled = false;
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
        turning = false;

        agent.SetDestination(targetPosition);

        DialogueSystem.Instance.HideDialog();

    }

    public void SetDirection(Vector3 targetDirection)
    {
        turning = true;
        targetRot = Quaternion.LookRotation(targetDirection - transform.position);
    }

    public void DeleteLine()
    {
        line.enabled = false;
        effect.Stop();
    }

    [System.Serializable]
    public class MouseOptions

    {
        public  Texture2D cursor;
        public  string cursorType;




    } 
} //class end

