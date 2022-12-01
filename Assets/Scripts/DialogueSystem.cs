using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; } //awesome line of code that makes it unnecesary to use getcomponent etc to use things
    //from this script in another script.

    [SerializeField] TMPro.TextMeshProUGUI messageText; //note what's needed to get TMP text working for future reference.
    [SerializeField] GameObject panel; //the dialogue panel we made earlier.

    private List<string> currentMessages = new List<string>();
    private int msgId = 0;

    private void Awake()
    {
        Instance = this; //for the public staticness of the script.
    }

    // Use this for initialization
    void Start ()
    {
        panel.SetActive(false);	//so that dialogue panel is not visible to begin with.
	}

    public void ShowMessages(List<string> messages)
    {
        currentMessages = messages; //we will pass our messages into here from our interactable.

        panel.SetActive(true);

        StartCoroutine(ShowMultipleMessages());
    }

    IEnumerator ShowMultipleMessages()
    {
        messageText.text = currentMessages[msgId]; //changing the TMP text to be the current message.

        while(msgId < currentMessages.Count)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                msgId++;

                if (msgId < currentMessages.Count)
                    messageText.text = currentMessages[msgId]; //message text updates to next message.
            }

            yield return null;
        }

		panel.SetActive(false);
        msgId = 0;
    }
}
