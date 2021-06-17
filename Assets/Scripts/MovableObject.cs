using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField]
    private ObjectType type;

    [HideInInspector]
    public bool isBusy;
    //[HideInInspector]
    public float height;

    private MovableObject neighbor;
    private Vector3 originalPosition;
    private MoverBase mover;
    private IShaker shaker;

    [HideInInspector]
    public ObjectType objectType { get { return type; } }

    private void Start()
    {
        mover = GetComponent<MoverBase>();
        if (mover)
        {
            mover.OnMovementCompleted += OnMovementCompleteCallback;
        }
        shaker = GetComponent<IShaker>();
        height = GetComponent<BoxCollider>().bounds.size.y;
        originalPosition = transform.position;
    }

    public void MoveObject(Vector3 rotationDirection)
    {
        if (isBusy) { return; }

        isBusy = true;
        var neighborPos = new Vector3(transform.position.x, 0f, transform.position.z) + rotationDirection;

        if (!GameManager.rotatableObjectPositions.ContainsKey(neighborPos))
        {
            if (shaker != null)
            {
                shaker.Shake(rotationDirection);
            }
            return;
        }

        neighbor = GameManager.rotatableObjectPositions[neighborPos];
        var pointToMoveTo = neighborPos + new Vector3(0, neighbor.height + height, 0);

        mover.Move(pointToMoveTo, rotationDirection);
    }

    private void OnMovementCompleteCallback()
    {
        transform.SetParent(neighbor.transform);
        neighbor.height += height;
        GetComponent<Collider>().enabled = false;
        GameManager.rotatableObjectPositions.Remove(originalPosition);
        GameManager.CheckIfWinConditionIsMet(this);
        //Add to UndoList
    }

    public void UnRotateObject(/*RotationInfo*/)
    {
        //?? Rotate(List< rotationInfo(rotationPoint, rotationDirection, gameobject, parent = null)>().lastindexof)
    }
}
