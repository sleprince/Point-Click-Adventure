using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;

public class CameraTrigger : MonoBehaviour
{
    //CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera switchCam;
    [SerializeField] private bool exiting;

    private void Start()
    {
        //mainCam = GameObject.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " colliided with " + other.gameObject.name);
        //collided with

        if (!exiting)
        {
            switchCam.Priority = 11;
            exiting = true;
        }
        else
        {
            switchCam.Priority = 9;
            exiting = false;
        }

    }


}