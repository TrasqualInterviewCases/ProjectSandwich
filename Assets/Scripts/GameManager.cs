using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UserInput userInput;
    [SerializeField]
    private GameObject winPanel;

    public List<MovableObject> movableObjects = new List<MovableObject>();
    public Dictionary<Vector3,MovableObject> rotatableObjectPositions = new Dictionary<Vector3, MovableObject>();
    public List<MovableObject> movedObjects = new List<MovableObject>();

    private Camera cam;
    private bool isWon;
    
    public static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        InitializeLists();
    }

    private void InitializeLists()
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
        if (movedObjects.Count == movableObjects.Count - 1)
        {
            var highestObject = movableObjects.OrderByDescending(y => y.transform.position.y).First();
            var lowestObject = movableObjects.OrderByDescending(y => y.transform.position.y).Last();

            if (lowestObject.ObjectType == ObjectType.Cap && highestObject.ObjectType == ObjectType.Cap)
            {
                isWon = true;
                StartCoroutine(WinGame());
            }
        }
    }

    private IEnumerator WinGame()
    {
        userInput.enabled = false;
        cam.transform.DOMove(new Vector3(0f, 18f, -23f), 2f, false);
        cam.transform.DORotate(new Vector3(32f, 0f, 0f), 2f);
        yield return new WaitForSeconds(2f);
        winPanel.SetActive(true);
    }

    public void OnUndoButtonClicked()
    {
        if (movedObjects.Count == 0 || isWon) return;
        movedObjects[movedObjects.Count - 1].UnMoveObject();
    }

    public void OnReplayButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
