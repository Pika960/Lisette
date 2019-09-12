using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericEventBehaviour : MonoBehaviour
{
    // private internal values
    private bool       isAlreadyTriggered;
    private string     objectName;
    private string     sceneName;
    private ushort     currentEventType;
    private GameObject canvas;
    private GameObject dialogWindow;

    // public values
    public int       itemAmount;
    public string    itemName;
    public TextAsset dialogResource;
    public TextAsset notificationResource;

    // Start is called before the first frame update
    void Start()
    {
        canvas       = GameObject.Find("/Canvas");
        dialogWindow = canvas.transform.Find("DialogWindow").gameObject;
        objectName   = gameObject.name;
        sceneName    = SceneManager.GetActiveScene().name;

        if (objectName.Contains("DialogEvent"))
        {
            currentEventType = 0;
        }

        else if (objectName.Contains("EncounterEvent"))
        {
            currentEventType = 1;
        }

        else if (objectName.Contains("ItemEvent"))
        {
            currentEventType = 2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAlreadyTriggered = GameState.CheckIfTriggered(sceneName, objectName);

            if (!isAlreadyTriggered)
            {
                if (currentEventType == 0)
                {
                    // logic specific for a dialog event
                    Debug.Log("OnTriggerEnter: Dialog");

                    if (dialogResource.text.Length != 0)
                    {
                        FindObjectOfType<DialogManager>().StartDialog(dialogResource);
                    }
                }

                else if (currentEventType == 1)
                {
                    // logic specific for an encounter event
                    Debug.Log("OnTriggerEnter: Encounter");

                    if (notificationResource.text.Length != 0)
                    {
                        FindObjectOfType<NotificationManager>().StartNotification(notificationResource.text);
                    }
                }

                else if (currentEventType == 2)
                {
                    // logic specific for an item event
                    Debug.Log("OnTriggerEnter: Item");

                    if (itemName.Length != 0 && itemAmount != 0 && 
                        dialogResource.text.Length != 0 && notificationResource.text.Length != 0)
                    {
                        GameState.ManageInventory(itemName, itemAmount);
                        StartCoroutine(SpawnMultipleDialogs());
                    }

                    else
                    {
                        GameState.ManageInventory("8F", 1);
                    }
                }
            }

            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        // I don't think we have something to do here
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isAlreadyTriggered)
            {
                if (currentEventType == 0)
                {
                    // logic specic for a dialog event
                    Debug.Log("OnTriggerExit: Dialog");
                }

                if (currentEventType == 1)
                {
                    // logic specific for an encounter event
                    Debug.Log("OnTriggerExit: Encounter");
                }

                else if (currentEventType == 2)
                {
                    // logic specific for an item event
                    Debug.Log("OnTriggerExit: Item");
                }
            }
        }
    }

    IEnumerator SpawnMultipleDialogs()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialogResource);

        yield return new WaitUntil(() => dialogWindow.activeSelf == false);

        string notificationMessage;
        notificationMessage  = notificationResource.text;
        notificationMessage += ("\n\n\t- " + itemName + " (x" + itemAmount + ")");
        FindObjectOfType<NotificationManager>().StartNotification(notificationMessage);
    }
}
