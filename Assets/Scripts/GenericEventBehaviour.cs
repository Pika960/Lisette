using UnityEngine;

public class GenericEventBehaviour : MonoBehaviour
{
    // private internal values
    private ushort currentEventType;

    // public values
    public TextAsset dialogResource;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name.Contains("DialogEvent"))
        {
            currentEventType = 0;
        }

        else if (gameObject.name.Contains("EncounterEvent"))
        {
            currentEventType = 1;
        }

        else if (gameObject.name.Contains("ItemEvent"))
        {
            currentEventType = 2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (currentEventType == 0)
            {
                // logic specific for a dialog event
                Debug.Log("OnTriggerEnter: Dialog");

                GameState.DialogResource = dialogResource;
                FindObjectOfType<DialogManager>().StartDialog();
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

    void OnTriggerStay(Collider other)
    {
        // I don't think we have something to do here
    }

    void OnTriggerExit(Collider other)
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
