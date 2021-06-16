using UnityEngine;
using DG.Tweening;

public class RotatableObject : MonoBehaviour
{
    [SerializeField]
    private ObjectType type;

    [HideInInspector]
    public bool isBusy;
    [HideInInspector]
    public float height;

    private RotatableObject neighbor;
    private Vector3 originalPosition;

    private void Start()
    {
        height = GetComponent<BoxCollider>().bounds.size.y;
        originalPosition = transform.position;
    }

    public void RotateObject(Vector3 rotationDirection)
    {
        if (isBusy) { return; }

        var neighborPos = new Vector3(transform.position.x,0f,transform.position.z) + rotationDirection;

        if (!GameManager.rotatableObjectPositions.ContainsKey(neighborPos))
        {
            isBusy = true;
            transform.DOShakeScale(0.5f, rotationDirection, 1, 1, true).OnComplete(()=> 
            {
                transform.DOPlayBackwards();
            });
            return;
        }

        neighbor = GameManager.rotatableObjectPositions[neighborPos];
        var pointToMoveTo = neighborPos + new Vector3(0, neighbor.height + height, 0);

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOJump(pointToMoveTo, 1, 1, 1f, false));
        s.Join(transform.DORotate(-Vector3.Cross(rotationDirection,transform.up) * 180, 1f)).OnComplete(()=> 
        {
            transform.SetParent(neighbor.transform);
            neighbor.height += height;
            GetComponent<Collider>().enabled = false;
            GameManager.rotatableObjectPositions.Remove(originalPosition);
        });

        //Check if win condition is met
        //Add to UndoList
    }

    public void UnRotateObject(/*RotationInfo*/)
    {
        //?? Rotate(List< rotationInfo(rotationPoint, rotationDirection, gameobject, parent = null)>().lastindexof)
    }
}
