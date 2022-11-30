using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float distancePosition = 1f; //how far away from interactable player stops.

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

        player.SetDirection(transform.position);
    }
}
