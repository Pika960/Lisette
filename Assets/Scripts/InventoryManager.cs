using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private EventSystem eventSystem;
    private GameObject  canvas;
    private GameObject  inventoryButton;
    private GameObject  inventoryContainer;
    private GameObject  inventoryWindow;
    private GameObject  player;
    private Text        inventoryAmount;
    private Text        inventoryItems;
    private Text        inventoryTitle;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas             = GameObject.Find("/Canvas");
        eventSystem        = EventSystem.current;
        inventoryWindow    = canvas.transform.Find("InventoryWindow").gameObject;
        inventoryButton    = inventoryWindow.transform.Find("CloseWindow").gameObject;
        inventoryContainer = inventoryWindow.transform.Find("InventoryContainer").gameObject;
        inventoryTitle     = inventoryWindow.transform.Find("InventoryTitle").gameObject.GetComponent<Text>();
        inventoryAmount    = inventoryContainer.transform.Find("Amount").gameObject.GetComponent<Text>();
        inventoryItems     = inventoryContainer.transform.Find("Items").gameObject.GetComponent<Text>();
        player             = GameObject.Find("/Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryWindow.activeSelf == true)
        {
            if (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.I) || 
                Input.GetButtonDown("FaceButton A"))
            {
                CloseInventoryWindow();
            }
        }
    }

    public void CloseInventoryWindow()
    {
        inventoryWindow.gameObject.SetActive(false);
        player.gameObject.GetComponent<PlayerInputController>().enabled = true;
    }

    public void OpenInventory()
    {
        eventSystem.SetSelectedGameObject(inventoryButton);
        inventoryWindow.gameObject.SetActive(true);
        player.gameObject.GetComponent<PlayerInputController>().enabled = false;

        Dictionary<string, int> inventory = GameState.GetInventory();
        string itemNames  = "\t\tItems\n\n";
        string itemAmount = "Amount\n\n";

        foreach (KeyValuePair<string, int> entry in inventory)
        {
            itemNames  += ("\t- " + entry.Key + "\n");
            itemAmount += ("x"    + entry.Value + "\n");
        }

        inventoryTitle.text  = "Inventory";
        inventoryItems.text  = itemNames;
        inventoryAmount.text = itemAmount;
    }
}
