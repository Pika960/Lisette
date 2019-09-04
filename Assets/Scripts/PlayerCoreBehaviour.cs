using UnityEngine;

public class PlayerCoreBehaviour : PlayerMovementBehaviour
{
    // Awake is called before any Start method
    protected override void Awake()
    {
        base.Awake();

        gameObject.transform.position = GameState.GetPlayerPosition(sceneName);
        gameObject.transform.rotation = GameState.GetPlayerRotation(sceneName);
    }

    public void MovePlayer(int horizontal, int vertical, int rotation)
    {
        Vector3 endPosition = Vector3.zero;

        if(vertical != 0)
        {
            endPosition = transform.position + transform.forward * vertical;
            base.StartMove(endPosition);
        }

        else if(horizontal != 0)
        {
            endPosition = transform.position + transform.right * horizontal;
            base.StartMove(endPosition);
        }

        else if(rotation != 0)
        {
            base.StartRotation(rotation);
        }
    }

    public new void InteractWithObject()
    {
        base.InteractWithObject();
    }
}
