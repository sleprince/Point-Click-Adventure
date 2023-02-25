using Cinemachine;
using UnityEngine;


public class SimpleCharController : MonoBehaviour
{
    public static SimpleCharController singleton; //static is a singleton, one instance only

    private float speed = 300f;
    private float jumpSpeed = 8f;
    private float gravity = 20f;

    public float sensitivity = 5f;
    private float rotY = 0;

    private Vector3 moveDirection = Vector3.zero;

    [SerializeField] CharacterController plController;
    [SerializeField] CharacterController vrController;

    public CinemachineVirtualCamera virtualCamera;

    public Animator animator;

    [SerializeField] PlayerAnimations playerAnim;

    //[SerializeField] public bool usingVR; //old way

    [SerializeField] GameManager game;


    void Awake()
    {
        singleton = this;
        //DontDestroyOnLoad(this);
    }

    public static SimpleCharController GetInstance()
    {
        return singleton;
    }

    void Start()
    {
        

        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;

        /*
        if (game.usingVR)
            virtualCamera.enabled = false; //turn off virtual cam if using VR
        else
            virtualCamera.enabled = true;
        */

 

    }

    void Update()
    {
        if (plController.isGrounded)
        {
            if (virtualCamera.enabled) //if virtual camera is active accept keyboard input
                moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));


            moveDirection = moveDirection.normalized * speed * 2f; //transform forward and right are needed to make character move in direction they are facing


            //moveDirection.y = virtualCamera.transform.localPosition.y; //move in the direction the camera is facing, doesn't work

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }


            float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity; //local needed to look l/r
            rotY += Input.GetAxis("Mouse Y") * sensitivity; //needs to not be local variable to be able to look u/d

            rotY = Mathf.Clamp(rotY, -90f, 90f); //important to limit how far you can look
            //transform.localEulerAngles = new Vector3(-rotY, rotX, 0); //causes the looking down immediately bug

            transform.localEulerAngles = new Vector3(0, rotX, 0); //move the character just horizontally

            virtualCamera.transform.localEulerAngles = new Vector3(-rotY, rotX, 0); //move the virtual camera

 
        moveDirection.y -= gravity * Time.deltaTime;



        plController.Move(moveDirection.normalized * Time.deltaTime * 2f);

        //move the vr camera in relation to the player.
        //vrController.Move(moveDirection.normalized * Time.deltaTime);


        playerAnim.UpdateAnimation(plController.velocity.sqrMagnitude * 5);




    }
}
