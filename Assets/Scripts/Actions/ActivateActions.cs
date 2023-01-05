using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateActions : Actions

{
    [SerializeField] List<CustomGameObject> customGameObjects = new List<CustomGameObject>();
    //creating a new list as defined in the CustomGameObject class below.
    private bool activated;

    public override void Act()
    {
        if(!activated)
        {
                for (int i = 0; i < customGameObjects.Count; i++)
            {
                customGameObjects[i].GO.SetActive(customGameObjects[i].ActiveStatus);
                activated = true;
            }
                
        }
        else
        {
            
            for (int i = 0; i < customGameObjects.Count; i++)
            {
                customGameObjects[i].GO.SetActive(!customGameObjects[i].ActiveStatus); //do the opposite the 2nd time
                activated = false;
            }
            
        }
        
        
    }
}

[System.Serializable] //serializable class, interesting!
public class CustomGameObject
{
    [SerializeField] GameObject gO;
    [SerializeField] bool activeStatus;

    public GameObject GO { get { return gO; } }
    public bool ActiveStatus { get { return activeStatus; } }
}
