using UnityEngine;
using DG.Tweening;

public class RotatableObject : MonoBehaviour
{
    [SerializeField]
    private ObjectType type;

    [HideInInspector]
    public bool isRotated;
    public float height;

    private RotatableObject neighbor;

    private void Start()
    {
        height = GetComponent<MeshRenderer>().bounds.size.y;
    }

    public void RotateObject(Vector3 rotationDirection)
    {
        if (isRotated) { return; }

        var neighborPos = new Vector3(transform.position.x,height/2f,transform.position.z) + rotationDirection;

        if (!GameManager.rotatableObjectPositions.ContainsKey(neighborPos))
        {
            isRotated = true;
            transform.DOShakeScale(0.5f, rotationDirection, 1, 1, true).OnComplete(()=> 
            {
                transform.DOPlayBackwards();
                isRotated = false; 
            });
            return;
        }

        neighbor = GameManager.rotatableObjectPositions[neighborPos];
        var pointToMoveTo = neighborPos + new Vector3(0, NewPositionY(), 0);

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOJump(pointToMoveTo, 1, 1, 1f, false));
        s.Join(transform.DORotate(-Vector3.Cross(rotationDirection,transform.up) * 180, 1f)).OnComplete(()=> 
        {
            transform.SetParent(neighbor.transform);
            neighbor.height += height;
            GetComponent<Collider>().enabled = false;
        });

        //Check if win condition is met
        //Remove from rotatableObjectPositions List
        //Add to UndoList
    }

    private float NewPositionY()
    {
        return neighbor.height > height ? neighbor.height : height;
    }

    public void UnRotateObject(/*RotationInfo*/)
    {
        //?? Rotate(List< rotationInfo(rotationPoint, rotationDirection, gameobject, parent = null)>().lastindexof)
    }
}
