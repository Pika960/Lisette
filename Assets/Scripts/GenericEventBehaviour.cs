using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEventBehaviour : MonoBehaviour
{
    // private internal values
    ushort currentEventType;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name.Contains("EncounterEvent"))
        {
            currentEventType = 0;
        }

        else if(gameObject.name.Contains("ItemEvent"))
        {
            currentEventType = 1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(currentEventType == 0)
            {
                // logic specific for an encounter event
            }

            else if(currentEventType == 1)
            {
                // logic specific for an encounter event
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
                // logic specific for an encounter event
            }

            else if (currentEventType == 1)
            {
                // logic specific for an encounter event
            }
        }
    }
}
