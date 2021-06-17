using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Dictionary<Vector3,MovableObject> rotatableObjectPositions = new Dictionary<Vector3, MovableObject>();
    public static List<MovableObject> movedObjects = new List<MovableObject>();

    private void Start()
    {
        InitializePositionList();
    }

    private void InitializePositionList() //use dictionary instead?
    {
        MovableObject[] rotatableObjects = FindObjectsOfType<MovableObject>();
        foreach (var rotatable in rotatableObjects)
        {
            rotatableObjectPositions.Add(rotatable.transform.position,rotatable);
        }
    }

    public static void CheckIfWinConditionIsMet(MovableObject movableObject)
    {
        var childMovableObject = movableObject.transform.GetChild(1).GetComponent<MovableObject>();
        if (childMovableObject != null)
        {
            if (movableObject.ObjectType == ObjectType.Cap && childMovableObject.ObjectType == ObjectType.Cap)
            {
                Debug.Log("Congratulations you win!");
            }
            else
                Debug.Log("the Child type isn't Cap");
        }
        else
        Debug.Log("the child isn't a movableObject");
    }

    public void OnUndoButtonClicked()
    {
        if (movedObjects.Count == 0) return;

        movedObjects[movedObjects.Count - 1].UnMoveObject();
    }
}
