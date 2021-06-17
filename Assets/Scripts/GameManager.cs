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
}
