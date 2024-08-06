using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    private GolfGameInputActions inputActions;

    void Awake()
    {
        inputActions = new GolfGameInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        inputActions.Mobile.TouchPos.performed += context => TouchStart(context);
        inputActions.Mobile.TouchPress.canceled += context => TouchEnd(context);
        inputActions.Mobile.Drag.started += context => OnDrag(context);
        inputActions.Debug.Reload.performed += context => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void TouchStart(InputAction.CallbackContext context)
    {
        {
            Debug.Log("TouchStart ");
            Vector2 mousePos = inputActions.Mobile.TouchPos.ReadValue<Vector2>();
            Rigidbody rb = GameObject.Find("GolfBall").GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.name == "GolfBall")
                {
                    Debug.Log("GolfBall hit");
                    PlayerController.i.startPos = new(
                        hit.collider.gameObject.transform.position.x,
                        0,
                        hit.collider.gameObject.transform.position.z
                    );
                    PlayerController.i.line.enabled = true;
                    PlayerController.i.line.positionCount = 1;
                    PlayerController.i.line.SetPosition(0, PlayerController.i.startPos);
                    PlayerController.i.endPos = PlayerController.i.startPos;
                }
                else if(hit.collider.gameObject.tag == "Floor")
                    {
                        GameObject p = GameObject.Find("GolfBall");
                        Debug.Log("p " + p.transform.position);
                        PlayerController.i.startPos = new(
                            p.transform.position.x,
                            0,
                            p.transform.position.z
                        );
                        Debug.Log("Floor hit " + PlayerController.i.startPos);
                        PlayerController.i.line.enabled = true;
                        PlayerController.i.line.positionCount = 2;
                        PlayerController.i.line.SetPosition(0, PlayerController.i.startPos);
                        PlayerController.i.endPos = new(hit.point.x, 0, hit.point.z);
                        Debug.Log("EndPos " + PlayerController.i.endPos + " " + hit.point + " StartPos " + PlayerController.i.startPos);
                        PlayerController.i.line.SetPosition(1, new(hit.point.x, 0, hit.point.z));
                    }
                
            }
            else
            {
                PlayerController.i.line.enabled = false;
                Debug.Log("No hit");
            }
        }
    }

    private Vector3 ScreenToWorld(Vector2 mousePos)
    {
        Debug.Log("ScreenToWorld");
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.tag == "Floor")
            {
                Debug.Log(hit.collider.gameObject.name + ", " + hit.collider.gameObject.tag);
                return new(hit.point.x, 0, hit.point.z);
            }
            else
            {
                Debug.Log(hit.collider.gameObject.name + ", " + hit.collider.gameObject.tag);
                return Vector3.zero;

            }
        }
        return Vector3.zero;
    }

    private Vector3 PositionDifference(Vector3 start, Vector3 end)
    {
        Debug.Log("PositionDifference");
        return new(end.x - start.x, 0, end.z - start.z);
    }

    private void OnDrag(InputAction.CallbackContext context)
    {
        Debug.Log("OnDrag");
        if (
            PlayerController.i.startPos == Vector3.zero
            || PlayerController.i.line.GetPosition(0) == Vector3.zero
        )
        {
            return;
        }
        PlayerController.i.line.positionCount = 2;
        PlayerController.i.line.SetPosition(
            1,
            ScreenToWorld(inputActions.Mobile.TouchPos.ReadValue<Vector2>())
        );
        if(ScreenToWorld(inputActions.Mobile.TouchPos.ReadValue<Vector2>()) != Vector3.zero)
            PlayerController.i.endPos = ScreenToWorld(inputActions.Mobile.TouchPos.ReadValue<Vector2>());
        else
            PlayerController.i.line.enabled = false;
    }

    private void TouchEnd(InputAction.CallbackContext context)
    {
        Debug.Log("TouchEnd");

        if (
            PlayerController.i.startPos == Vector3.zero
            || PlayerController.i.line.GetPosition(0) == Vector3.zero
        )
        {
            Debug.Log("Return InputManager:125");

            return;
        }

        if (
            PlayerController.i.startPos != PlayerController.i.endPos
            && PlayerController.i.startPos != Vector3.zero
        )
        {

            Debug.Log("Launch InputManager:134");
            PlayerController.i.Launch(
                PositionDifference(PlayerController.i.startPos, PlayerController.i.endPos)
            );
            //PlayerController.i.startPos = PlayerController.i.endPos;
            PlayerController.i.line.SetPosition(1, PlayerController.i.endPos);
            PlayerController.i.line.SetPosition(0, PlayerController.i.endPos);
        }
    }
}
