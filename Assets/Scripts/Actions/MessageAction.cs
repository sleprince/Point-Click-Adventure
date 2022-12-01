using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageAction : Actions
{
    [TextArea(5, 3)] //makes the text input field in Editor be multiline and have word wrap.
    [SerializeField] List<string> message; //now our messages are an array of messages.

    public override void Act()
    {
        //Debug.Log(message);
        DialogueSystem.Instance.ShowMessages(message);
    }
}
