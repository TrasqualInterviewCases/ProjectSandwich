using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] LayerMask mask;

    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;

    private Camera cam;
    private RaycastHit hit;
    private RotatableObject curRotatableObject;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(camRay, out hit, Mathf.Infinity, mask);

            mouseStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            mouseEndPos = Input.mousePosition;
            curRotatableObject = hit.transform.GetComponent<RotatableObject>();
            if(mouseStartPos != mouseEndPos && curRotatableObject != null)
            {
                curRotatableObject.RotateObject(Direction());
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            curRotatableObject.isRotated = false; //close box collider?
            curRotatableObject = null;
        }
    }

    private Vector3 Direction()
    {
        if (IsVerticalDirection())
        {
            return mouseEndPos.y - mouseStartPos.y > 0 ? Vector3.up : Vector3.down;
        }
        else
        {
            return mouseEndPos.x - mouseStartPos.x > 0 ? Vector3.right : Vector3.left;
        }
    }

    private bool IsVerticalDirection()
    {
        return ((mouseEndPos.x - mouseStartPos.x) < (mouseEndPos.y - mouseStartPos.y));
    }
}
