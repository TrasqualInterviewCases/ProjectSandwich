using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField]
    private ObjectType type;

    [HideInInspector]
    public bool isBusy;
    [HideInInspector]
    public float height;

    private GameManager gm;
    private UserInput userInput;
    private MovableObject neighbor;
    private Vector3 originalPosition;

    private MoverBase mover; //Using base classes so we can use different types of movements and shakes without breaking the code.
    private ShakerBase shaker;

    [HideInInspector]
    public ObjectType ObjectType { get { return type; } }

    private void Start()
    {
        gm = GameManager.instance;
        userInput = FindObjectOfType<UserInput>();
        mover = GetComponent<MoverBase>();
        if (mover)
        {
            mover.OnMovementCompleted += OnMovementCompleteCallback;
        }
        shaker = GetComponent<ShakerBase>();
        if (shaker)
        {
            shaker.OnShakeCompleted += OnShakeCompleteCallBack;
        }
        height = GetComponent<BoxCollider>().bounds.size.y;
        originalPosition = transform.position;
    }

    public void MoveObject(Vector3 rotationDirection)
    {
        if (isBusy) { return; }


        var neighborPos = new Vector3(transform.position.x, 0f, transform.position.z) + rotationDirection;

        if (!gm.rotatableObjectPositions.ContainsKey(neighborPos))
        {
            if (shaker)
            {
                isBusy = true; //To Prevent user from interacting with this object while it's moving.
                shaker.Shake(rotationDirection);
            }
            return;
        }
        else
        {
            isBusy = true; //To Prevent user from interacting with this object while it's moving. Calling this twice so the movement works even if there is no Shaker script on object.
            neighbor = gm.rotatableObjectPositions[neighborPos];
            var pointToMoveTo = neighborPos + new Vector3(0, neighbor.height + height, 0);
            if (mover)
            {
                GetComponent<Collider>().enabled = false;
                userInput.enabled = false; //Disabling user input to prevent user from moving other objects while this one is moving.
                mover.Move(pointToMoveTo, rotationDirection);
            }
        }
    }

    private void OnMovementCompleteCallback(bool isForwardMovement)
    {
        if (isForwardMovement)
        {
            transform.SetParent(neighbor.transform);
            neighbor.height += height;
            gm.rotatableObjectPositions.Remove(originalPosition);
            gm.movedObjects.Add(this);
            gm.CheckIfWinConditionIsMet();
            isBusy = false;
            userInput.enabled = true;
        }
        else
        {
            transform.SetParent(null);
            neighbor.height -= height;
            GetComponent<Collider>().enabled = true;
            gm.rotatableObjectPositions.Add(originalPosition,this);
            gm.movedObjects.Remove(this);
            isBusy = false;
            userInput.enabled = true;
        }
    }

    private void OnShakeCompleteCallBack()
    {
        isBusy = false;
    }

    public void UnMoveObject()
    {
        isBusy = true;
        userInput.enabled = false;
        mover.UnMove();
    }
}
