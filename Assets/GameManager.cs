using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] Inventory plInv;
    [SerializeField] ItemDatabase itemDatabase;

    [SerializeField] MessageAction introMessage;
    [SerializeField] Interactable interact;

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        //clears the player's inventory and then adds the default items when starting a new game

        plInv.ClearInventory();

        plInv.AddItem(itemDatabase.GetItem(1));
        //plInv.AddItem(itemDatabase.GetItem(3));

        StartCoroutine(ShowIntroMessage());

        //DialogueSystem.Instance.InspectMessage(intro);

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
        }


    }

    public void Interact()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "MainScene")
            introMessage.Act(); //show intro message
    }

    public void Lose()
    {
        //interact.InspectActions[1].Act();
        losePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Win()
    {
        //interact.InspectActions[2].Act();
        winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void AgainButton()
    {
        SceneManager.LoadScene("MainScene");
        
    }

    public void QuitButton()
    {
        Application.Quit();

    }


    IEnumerator ShowIntroMessage()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null; //delays the coroutine until mouse clicked
        }

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "MainScene")
            interact.InspectActions[0].Act();

    }

}
