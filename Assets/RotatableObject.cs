using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    [SerializeField]
    private ObjectType type;

    [HideInInspector]
    public bool isRotated;

    public void RotateObject(Vector3 rotationDirection)
    {
        //Check if isRotated
        //RotateAround(CalculatedPoint(), direction)
        //Wait for rotation to complete then set isRotated = true;
        //SetParentOfObject
        //Check if win condition is met
        //Remove from rotatableObjectPositions List
        //Add to UndoList
    }

    private void CalculateRotationPoint(Vector3 rotationDirection)
    {
        //GetCurrentObjectHeight
        //GetGridPosition use List instead of grid for now
        //Add to rotatableObjectPositions List
    }

    public void UnRotateObject(/*RotationInfo*/)
    {
        //?? Rotate(List< rotationInfo(rotationPoint, rotationDirection, gameobject, parent = null)>().lastindexof)
    }
}
