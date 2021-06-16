using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<Vector3> rotatableObjectPositions = new List<Vector3>();

    private void Start()
    {
        InitializePositionList();
    }

    private void InitializePositionList() //use dictionary instead?
    {
        RotatableObject[] rotatableObjects = FindObjectsOfType<RotatableObject>();
        foreach (var rotatable in rotatableObjects)
        {
            rotatableObjectPositions.Add(rotatable.transform.position);
        }
    }
}
