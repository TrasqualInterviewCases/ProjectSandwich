using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Dictionary<Vector3,MovableObject> rotatableObjectPositions = new Dictionary<Vector3, MovableObject>();

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
            if (movableObject.objectType == ObjectType.Cap && childMovableObject.objectType == ObjectType.Cap)
            {
                Debug.Log("Congratulations you win!");
            }
            else
                Debug.Log("the Child type isn't Cap");
        }
        else
        Debug.Log("the child isn't a movableObject");
    }
}
