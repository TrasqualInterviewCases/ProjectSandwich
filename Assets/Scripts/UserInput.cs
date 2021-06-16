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

        if (Input.GetMouseButtonUp(0))
        {
            if(curRotatableObject != null)
            {
                curRotatableObject.isBusy = false; //close box collider?
                curRotatableObject = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            mouseEndPos = Input.mousePosition;
            if(hit.collider != null)
            {
                curRotatableObject = hit.transform.GetComponent<RotatableObject>();
            }
            if (mouseStartPos != mouseEndPos && curRotatableObject != null)
            {
                curRotatableObject.RotateObject(Direction());
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
