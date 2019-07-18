using System.Collections;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    // private internal values
    private bool is180SpinAllowed;
    private PlayerCoreBehaviour player;

    // Awake is called before any Start method
    void Awake()
    {
        player = GetComponent<PlayerCoreBehaviour>();
    }

    // Start is called before the first frame update
    void Start()
    {
        is180SpinAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.IsMoving())
        {
            GetPlayerInput();
        }
    }

    // get user input from keyboard or controller
    private void GetPlayerInput()
    {
        if(Input.GetAxis("Keyboard Horizontal") != 0f)
        {
            MovePlayer((int)(Input.GetAxisRaw("Keyboard Horizontal")), 0);
        }

        if(Input.GetAxis("LeftStick Horizontal") != 0f)
        {
            MovePlayer((int)(Input.GetAxisRaw("LeftStick Horizontal")), 0);
        }

        if(Input.GetAxis("Keyboard Vertical") != 0f)
        {
            MovePlayer((int)(Input.GetAxisRaw("Keyboard Vertical")), 1);
        }

        if(Input.GetAxis("LeftStick Vertical") != 0f)
        {
            MovePlayer((int)(Input.GetAxisRaw("LeftStick Vertical")), 1);
        }

        if(Input.GetKey(KeyCode.E) || Input.GetButton("FaceButton A"))
        {
            player.InteractWithObject();
        }

        if(Input.GetKey(KeyCode.Escape) || Input.GetButton("MenuButton Start"))
        {
            Application.Quit();
        }
    }

    private void MovePlayer(int orientation, ushort axis)
    {
        if(axis == 0)
        {
            player.MovePlayer(0, 0, orientation);
        }

        if(axis == 1)
        {
            if(orientation > 0)
            {
                player.MovePlayer(0, orientation, 0);
            }

            else if(orientation < 0)
            {
                if(is180SpinAllowed)
                {
                    player.MovePlayer(0, 0, 2);
                    StartCoroutine(Disable180Spin());
                }
            }
        }
    }

    IEnumerator Disable180Spin()
    {
        is180SpinAllowed = false;
        yield return new WaitForSeconds(0.5f);
        is180SpinAllowed = true;
    }
}
