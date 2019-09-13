using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BattleScreenManager : MonoBehaviour
{
    private EventSystem eventSystem;
    private GameObject  canvas;
    private GameObject  battleWindow;
    private GameObject  actionsWindow;
    private GameObject  attackWindow;
    private GameObject  buttonAttack01;
    private GameObject  buttonAttack02;
    private GameObject  buttonAttack03;
    private GameObject  buttonAttack04;
    private Text        nextAction;
    private Text        attack01;
    private Text        attack02;
    private Text        attack03;
    private Text        attack04;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem    = EventSystem.current;
        canvas         = GameObject.Find("/Canvas");
        battleWindow   = canvas.transform.Find("BattleWindow").gameObject;
        actionsWindow  = battleWindow.transform.Find("ActionsWindow").gameObject;
        attackWindow   = battleWindow.transform.Find("AttackWindow").gameObject;
        buttonAttack01 = attackWindow.transform.Find("Attack01").gameObject;
        buttonAttack02 = attackWindow.transform.Find("Attack02").gameObject;
        buttonAttack03 = attackWindow.transform.Find("Attack03").gameObject;
        buttonAttack04 = attackWindow.transform.Find("Attack04").gameObject;
        nextAction     = battleWindow.transform.Find("NextAction").gameObject.GetComponent<Text>();
        attack01       = buttonAttack01.transform.Find("Text").GetComponent<Text>();
        attack02       = buttonAttack02.transform.Find("Text").GetComponent<Text>();
        attack03       = buttonAttack03.transform.Find("Text").GetComponent<Text>();
        attack04       = buttonAttack04.transform.Find("Text").GetComponent<Text>();

        nextAction.text = "What will you do next?";
        attack01.text   = GameState.GetAttackListPlayer()[001].GetAttackName();
        attack02.text   = GameState.GetAttackListPlayer()[002].GetAttackName();
        attack03.text   = GameState.GetAttackListPlayer()[003].GetAttackName();
        attack04.text   = GameState.GetAttackListPlayer()[004].GetAttackName();
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
                if (actionsWindow.activeSelf == true)
                {
                    eventSystem.SetSelectedGameObject(actionsWindow.transform.Find("Fight").gameObject);
                }

                if (attackWindow.activeSelf == true)
                {
                    eventSystem.SetSelectedGameObject(attackWindow.transform.Find("Attack01").gameObject);
                }
            }
        }

        // select new game button if nothing is selected (joystick)
        if (Math.Abs(Input.GetAxis("LeftStick Horizontal")) > 0.5f || Math.Abs(Input.GetAxis("LeftStick Vertical")) > 0.5f)
        {
            if (eventSystem.currentSelectedGameObject == null)
            {
                if (actionsWindow.activeSelf == true)
                {
                    eventSystem.SetSelectedGameObject(actionsWindow.transform.Find("Fight").gameObject);
                }

                if (attackWindow.activeSelf == true)
                {
                    eventSystem.SetSelectedGameObject(attackWindow.transform.Find("Attack01").gameObject);
                }
            }
        }
    }

    public void Fight()
    {
        actionsWindow.SetActive(false);
        attackWindow.SetActive(true);
        eventSystem.SetSelectedGameObject(attackWindow.transform.Find("Attack01").gameObject);
        nextAction.text = "Choose your attack...";
    }

    public void Team()
    {
        nextAction.text = "You are the only active team member.";
    }

    public void Items()
    {
        nextAction.text = "You do not have any battle items.";
    }

    public void Run()
    {
        actionsWindow.transform.Find("Fight").gameObject.SetActive(false);
        actionsWindow.transform.Find("Team").gameObject.SetActive(false);
        actionsWindow.transform.Find("Items").gameObject.SetActive(false);
        actionsWindow.transform.Find("Run").gameObject.SetActive(false);
        nextAction.text = "Got away safely.";

        StartCoroutine(WaitForSeconds());
    }

    public void Attack01()
    {
        // not implemented yet
    }

    public void Attack02()
    {
        // not implemented yet
    }

    public void Attack03()
    {
        // not implemented yet
    }

    public void Attack04()
    {
        // not implemented yet
    }

    public void BackToActions()
    {
        attackWindow.SetActive(false);
        actionsWindow.SetActive(true);
        eventSystem.SetSelectedGameObject(actionsWindow.transform.Find("Fight").gameObject);
        nextAction.text = "What will you do next?";
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("SampleScene");
    }
}
