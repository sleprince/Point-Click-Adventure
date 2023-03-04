using Cinemachine;
using UnityEngine;

public class CameraAction : Actions
{
    //CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera switchCam;
    [SerializeField] private bool exiting;

    //[SerializeField] BoxCollider trigger;

    private void Start()
    {
        //mainCam = GameObject.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();

    }

    public override void Act()
    {
        if (!exiting)
        {
            switchCam.Priority = 11;
            exiting= true;
        }
        else
            switchCam.Priority = 9;
            exiting= false;


    }


}