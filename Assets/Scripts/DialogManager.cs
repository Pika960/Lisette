using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private Queue<string> dialogSentences;
    private Queue<string> dialogNames;
    
    private GameObject canvas;
    private GameObject dialogWindow;
    private GameObject player;
    private Text       dialogName;
    private Text       dialogText;
    private TextAsset  textFile;

    // Start is called before the first frame update
    void Start()
    {
        canvas       = GameObject.Find("/Canvas");
        dialogWindow = canvas.transform.Find("DialogWindow").gameObject;
        dialogName   = dialogWindow.transform.Find("DialogName").gameObject.GetComponent<Text>();
        dialogText   = dialogWindow.transform.Find("DialogText").gameObject.GetComponent<Text>();
        player       = GameObject.Find("/Player");

        dialogNames     = new Queue<string>();
        dialogSentences = new Queue<string>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (dialogWindow.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("FaceButton A"))
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialog(TextAsset dialogResource)
    {
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
