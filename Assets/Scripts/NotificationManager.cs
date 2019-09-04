using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    private GameObject canvas;
    private GameObject notificationWindow;
    private GameObject player;
    private Text       notificationText;

    // Start is called before the first frame update
    void Start()
    {
        canvas             = GameObject.Find("/Canvas");
        notificationWindow = canvas.transform.Find("NotificationWindow").gameObject;
        notificationText   = notificationWindow.transform.Find("NotificationText").gameObject.GetComponent<Text>();
        player             = GameObject.Find("/Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (notificationWindow.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("FaceButton A"))
            {
                CloseNotificationWindow();
            }
        }
    }

    public void CloseNotificationWindow()
    {
        notificationWindow.gameObject.SetActive(false);
        player.gameObject.GetComponent<PlayerInputController>().enabled = true;
    }

    public void StartNotification(string message)
    {
        notificationWindow.gameObject.SetActive(true);
        player.gameObject.GetComponent<PlayerInputController>().enabled = false;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(message));
    }

    IEnumerator TypeSentence(string sentence)
    {
        notificationText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            notificationText.text += letter;
            yield return null;
        }
    }
}
