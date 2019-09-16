using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    private EventSystem eventSystem;
    private GameObject  canvas;
    private GameObject  notificationButton;
    private GameObject  notificationWindow;
    private GameObject  player;
    private Text        notificationText;

    // Start is called before the first frame update
    void Start()
    {
        canvas             = GameObject.Find("/Canvas");
        eventSystem        = EventSystem.current;
        notificationWindow = canvas.transform.Find("NotificationWindow").gameObject;
        notificationButton = notificationWindow.transform.Find("CloseWindow").gameObject;
        notificationText   = notificationWindow.transform.Find("NotificationText").gameObject.GetComponent<Text>();
        player             = GameObject.Find("/Player");
    }

    // Update is called once per frame
    void Update()
    {
        // nothing to do here
    }

    public void CloseNotificationWindow()
    {
        eventSystem.SetSelectedGameObject(null);
        notificationWindow.gameObject.SetActive(false);
        player.gameObject.GetComponent<PlayerInputController>().enabled = true;
    }

    public void StartNotification(string message)
    {
        eventSystem.SetSelectedGameObject(notificationButton);
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
