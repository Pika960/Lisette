using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericEventBehaviour : MonoBehaviour
{
    // private internal values
    private bool   isAlreadyTriggered;
    private string objectName;
    private string sceneName;
    private ushort currentEventType;

    // public values
    public TextAsset dialogResource;

    // Start is called before the first frame update
    void Start()
    {
        objectName = gameObject.name;
        sceneName  = SceneManager.GetActiveScene().name;
        isAlreadyTriggered = GameState.checkIfTriggered(sceneName, objectName);

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
        if (!isAlreadyTriggered)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isAlreadyTriggered = GameState.checkIfTriggered(sceneName, objectName);

                if (currentEventType == 0)
                {
                    // logic specific for a dialog event
                    Debug.Log("OnTriggerEnter: Dialog");
                    FindObjectOfType<DialogManager>().StartDialog(dialogResource);
                }

                else if (currentEventType == 1)
                {
                    // logic specific for an encounter event
                    Debug.Log("OnTriggerEnter: Encounter");
                }

                else if (currentEventType == 2)
                {
                    // logic specific for an item event
                    Debug.Log("OnTriggerEnter: Item");
                }
            }
        }

        else
        {
            Debug.Log("OnTriggerEnter: Already triggered");
        }
    }

    void OnTriggerStay(Collider other)
    {
        // I don't think we have something to do here
    }

    void OnTriggerExit(Collider other)
    {
        if (!isAlreadyTriggered)
        {
            if (other.gameObject.CompareTag("Player"))
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
}
