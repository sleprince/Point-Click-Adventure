using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //needed to use Unity's AI functions.

public class PlayerScript : MonoBehaviour
{

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //getting the reference to the NavMeshAgent in Unity Editor that's attached
        //to the player.
        agent = GetComponent<NavMeshAgent>();

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

    }
}
