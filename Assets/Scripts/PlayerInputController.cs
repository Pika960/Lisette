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
        if (Input.GetAxis("Horizontal") != 0f)
        {
            player.MovePlayer(0, 0, (int)(Input.GetAxisRaw("Horizontal")));
        }

        if(Input.GetAxis("Vertical") != 0f)
        {
            int vertical = (int)(Input.GetAxisRaw("Vertical"));

            if(vertical > 0f)
            {
                player.MovePlayer(0, vertical, 0);
            }

            else if(vertical < 0f)
            {
                if(is180SpinAllowed)
                {
                    player.MovePlayer(0, 0, 2);
                    StartCoroutine(Disable180Spin());
                }
            }
        }

        if(Input.GetKey(KeyCode.E))
        {
            player.InteractWithObject();
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator Disable180Spin()
    {
        is180SpinAllowed = false;
        yield return new WaitForSeconds(0.5f);
        is180SpinAllowed = true;
    }
}
