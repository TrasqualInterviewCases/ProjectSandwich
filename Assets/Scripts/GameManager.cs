using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UserInput userInput;
    [SerializeField]
    private GameObject winPanel;

    public static GameManager instance;

    public List<MovableObject> movableObjects = new List<MovableObject>();
    public Dictionary<Vector3,MovableObject> rotatableObjectPositions = new Dictionary<Vector3, MovableObject>();
    public List<MovableObject> movedObjects = new List<MovableObject>();

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    private void Start()
    {
        InitializeLists();
    }

    private void InitializeLists() //use dictionary instead?
    {
        MovableObject[] movables = FindObjectsOfType<MovableObject>();
        foreach (var movable in movables)
        {
            movableObjects.Add(movable);
            rotatableObjectPositions.Add(movable.transform.position, movable);
        }
    }

    public void CheckIfWinConditionIsMet()
    {
        if(movedObjects.Count == movableObjects.Count-1)
        {
        var highestObject = movableObjects.OrderByDescending(y => y.transform.position.y).First();
        var lowestObject = movableObjects.OrderByDescending(y => y.transform.position.y).Last();

            if (lowestObject.ObjectType == ObjectType.Cap && highestObject.ObjectType == ObjectType.Cap)
            {
                WinGame();
            }
        }
    }

    private void WinGame()
    {
        userInput.enabled = false;
        winPanel.SetActive(true);
    }

    public void OnUndoButtonClicked()
    {
        if (movedObjects.Count == 0) return;
        movedObjects[movedObjects.Count - 1].UnMoveObject();
    }

    public void OnReplayButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
