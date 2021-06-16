using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Dictionary<Vector3,RotatableObject> rotatableObjectPositions = new Dictionary<Vector3, RotatableObject>();

    private void Start()
    {
        InitializePositionList();
    }

    private void InitializePositionList() //use dictionary instead?
    {
        RotatableObject[] rotatableObjects = FindObjectsOfType<RotatableObject>();
        foreach (var rotatable in rotatableObjects)
        {
            rotatableObjectPositions.Add(rotatable.transform.position,rotatable);
        }
    }
}
