
//Attach this script to a GameObject to have it output messages when your mouse hovers over it.
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnMouseOverLabel : MonoBehaviour
{

    private GameObject labelText;

    void Start()
    {
        labelText = GameObject.Find("Button");
        
        labelText.SetActive(false);
    }
    
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");

        labelText.SetActive(true);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        
        labelText.SetActive(false);
    }
}