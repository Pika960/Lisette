using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private EventSystem eventSystem;
    private GameObject  canvas;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        canvas = GameObject.Find("/Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        // disable selection if mouse movement is detected
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            eventSystem.SetSelectedGameObject(null);
        }

        // select new game button if nothing is selected (keyboard)
        if (Input.GetAxis("Keyboard Horizontal") != 0 || Input.GetAxis("Keyboard Vertical") != 0)
        {
            if (eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(canvas.transform.Find("NewGame").gameObject);
            }
        }

        // select new game button if nothing is selected (joystick)
        if (Math.Abs(Input.GetAxis("LeftStick Horizontal")) > 0.5f || Math.Abs(Input.GetAxis("LeftStick Vertical")) > 0.5f)
        {
            if (eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(canvas.transform.Find("NewGame").gameObject);
            }
        }
    }

    public void StartNewGame()
    {
        // load the demo stage
        SceneManager.LoadScene("SampleScene");
    }

    public void ContinueGame()
    {
        // nothing to do here at the moment
    }

    public void DisplayOptions()
    {
        // options scene does not exist at the moment
    }

    public void DisplayControls()
    {
        // controls scene does not exist at the moment
    }

    public void DisplayCredits()
    {
        // credits scene does not exist at the moment
    }

    public void QuitGame()
    {
        // close the game
        Application.Quit();
    }
}
