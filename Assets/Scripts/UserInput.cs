using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] LayerMask mask;

    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;

    private Camera cam;
    private MovableObject curRotatableObject;

    private bool isMovingObject;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(camRay, out hit, Mathf.Infinity, mask))
            {
                curRotatableObject = hit.transform.GetComponent<MovableObject>();
                mouseStartPos = Input.mousePosition;
            }
        }

        if (Input.GetMouseButton(0))
        {
            mouseEndPos = Input.mousePosition;
            if ((mouseStartPos - mouseEndPos).magnitude > 1f && curRotatableObject != null && !isMovingObject)
            {
                isMovingObject = true; //To prevent registiring a second movement while the current movement is in progress.
                curRotatableObject.MoveObject(Direction());
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (curRotatableObject != null)
            {
                curRotatableObject = null;
                isMovingObject = false;
            }
        }
    }

    private Vector3 Direction()
    {
        if (IsVerticalDirection())
        {
            return mouseEndPos.y - mouseStartPos.y > 0 ? Vector3.forward : Vector3.back;
        }
        else
        {
            return mouseEndPos.x - mouseStartPos.x > 0 ? Vector3.right : Vector3.left;
        }
    }

    private bool IsVerticalDirection()
    {
        return (Mathf.Abs((mouseEndPos.y - mouseStartPos.y)) > Mathf.Abs((mouseEndPos.x - mouseStartPos.x)));
    }
}
