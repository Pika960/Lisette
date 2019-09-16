using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private Queue<string> dialogSentences;
    private Queue<string> dialogNames;

    private EventSystem eventSystem;
    private GameObject  canvas;
    private GameObject  dialogButton;
    private GameObject  dialogWindow;
    private GameObject  player;
    private Text        dialogName;
    private Text        dialogText;
    private TextAsset   textFile;

    // Start is called before the first frame update
    void Start()
    {
        canvas       = GameObject.Find("/Canvas");
        dialogWindow = canvas.transform.Find("DialogWindow").gameObject;
        dialogButton = dialogWindow.transform.Find("NextLine").gameObject;
        dialogName   = dialogWindow.transform.Find("DialogName").gameObject.GetComponent<Text>();
        dialogText   = dialogWindow.transform.Find("DialogText").gameObject.GetComponent<Text>();
        eventSystem  = EventSystem.current;
        player       = GameObject.Find("/Player");

        dialogNames     = new Queue<string>();
        dialogSentences = new Queue<string>();
    }

    // Update is called once per frame
    private void Update()
    {
        // nothing to do here
    }

    public void StartDialog(TextAsset dialogResource)
    {
        eventSystem.SetSelectedGameObject(dialogButton);
        dialogNames.Clear();
        dialogSentences.Clear();
        textFile = dialogResource;

        if (textFile != null)
        {
            string[] dialogLines = textFile.text.Split("\n"[0]);

            foreach(string line in dialogLines)
            {
                string[] split = line.Split('|');
                dialogNames.Enqueue(split[0]);
                dialogSentences.Enqueue(split[1]);
            }

            dialogWindow.gameObject.SetActive(true);
            player.gameObject.GetComponent<PlayerInputController>().enabled = false;

            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (dialogSentences.Count == 0)
        {
            eventSystem.SetSelectedGameObject(null);
            dialogWindow.gameObject.SetActive(false);
            player.gameObject.GetComponent<PlayerInputController>().enabled = true;

            return;
        }

        string name = dialogNames.Dequeue();
        string sentence = dialogSentences.Dequeue();

        dialogName.text = name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }
}
