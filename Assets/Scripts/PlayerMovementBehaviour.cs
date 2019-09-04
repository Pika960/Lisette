using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementBehaviour : MonoBehaviour
{
    // global public values
    public float moveSpeed = 0.5f;
    public float moveTime  = 0.3f;
    public float turnTime  = 0.3f;

    // private internal values
    private bool      canMove;
    private bool      isMoving;
    private float     invMoveTime;
    private LayerMask layer;

    // protected internal values
    protected string sceneName;

    // Awake is called before any Start method
    protected virtual void Awake()
    {
        canMove     = false;
        isMoving    = false;
        invMoveTime = 1f / moveTime;
        sceneName   = SceneManager.GetActiveScene().name;
        layer       = 1 << LayerMask.NameToLayer("Default");
    }

    protected virtual void StartMove(Vector3 newDirection)
    {
        if(!isMoving)
        {
            RaycastHit hit;
            Move(newDirection, out hit);
        }
    }

    protected virtual void StartRotation(int newRotation)
    {
        if(!isMoving)
        {
            StartCoroutine(Turning(newRotation));
        }
    }

    protected virtual void Move(Vector3 newDirection, out RaycastHit hit)
    {
        if(Physics.Linecast(transform.position, newDirection, out hit, layer))
        {
            GameObject nextGameObjectInScene = hit.collider.gameObject;

            bool isEvent    = nextGameObjectInScene.CompareTag("Event");
            bool isWalkable = nextGameObjectInScene.CompareTag("Walkable");

            if(!isEvent && !isWalkable)
            {
                canMove = false;
                return;
            }
        }

        canMove = true;
        StartCoroutine(Movement(newDirection));
    }

    protected virtual void InteractWithObject()
    {
        RaycastHit hit;

        if(Physics.Linecast(transform.position, (transform.position + transform.forward), out hit, layer))
        {
            GameObject nextGameObjectInScene = hit.collider.gameObject;

            if(nextGameObjectInScene.CompareTag("Door"))
            {
                Vector3 newDirection = nextGameObjectInScene.transform.position;
                float   dot          = Vector3.Dot(transform.forward, (newDirection - transform.position).normalized);

                //if(dot >= (1f - float.Epsilon))
                if(dot >= 0.9f)
                {
                    nextGameObjectInScene.tag = "Walkable";
                    StartMove(newDirection + transform.forward);
                    nextGameObjectInScene.tag = "Door";
                }
            }
        }
    }

    protected IEnumerator Movement(Vector3 end)
    {
        isMoving = true;

        float remainingDistance = (transform.position - end).sqrMagnitude;

        while(remainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, invMoveTime * Time.deltaTime);

            transform.position = newPosition;
            remainingDistance  = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        GameState.SetPlayerPosition(sceneName, gameObject.transform.position);
        isMoving = false;
    }

    protected IEnumerator Turning(int newRotation)
    {
        isMoving = true;

        int   degrees   = newRotation * 90;
        float rate      = 1f / turnTime;
        float timeCount = 1f;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation   = transform.rotation * Quaternion.Euler(0, degrees, 0);

        while(timeCount > float.Epsilon)
        {
            timeCount -= Time.deltaTime * rate;
            transform.rotation = Quaternion.Slerp(endRotation, startRotation, timeCount);

            yield return null;
        }

        GameState.SetPlayerRotation(sceneName, gameObject.transform.rotation);
        isMoving = false;
    }

    public bool CanMove()
    {
        return canMove;
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
