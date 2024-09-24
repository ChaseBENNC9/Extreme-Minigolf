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
        if (enabled)
        {
            Vector2 mousePos = inputActions.Mobile.TouchPos.ReadValue<Vector2>();
            Rigidbody rb = GameObject.Find("GolfBall").GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            int layerMast = 1 << 3;
            layerMast = ~layerMast;
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMast))
            {
                if (hit.collider.gameObject.name == "GolfBall")
                {
                    PlayerController.i.startPos = new(
                        hit.collider.gameObject.transform.position.x,
                        0,
                        hit.collider.gameObject.transform.position.z
                    );
                    PlayerController.i.line.enabled = true;
                    if (PlayerController.i.helpArrow != null)
                        PlayerController.i.helpArrow.SetActive(false);
                    PlayerController.i.line.positionCount = 1;
                    PlayerController.i.line.SetPosition(0, PlayerController.i.startPos);
                    PlayerController.i.endPos = PlayerController.i.startPos;
                }
                else if (hit.collider.gameObject.tag == "Floor")
                {
                    GameObject p = GameObject.Find("GolfBall");
                    PlayerController.i.startPos = new(
                        p.transform.position.x,
                        0,
                        p.transform.position.z
                    );
                    PlayerController.i.line.enabled = true;
                    if (PlayerController.i.helpArrow != null)
                        PlayerController.i.helpArrow.SetActive(false);
                    PlayerController.i.line.positionCount = 2;
                    PlayerController.i.line.SetPosition(0, PlayerController.i.startPos);
                    if (hit.point.z < PlayerController.i.gameObject.transform.position.z)
                    {
                        PlayerController.i.endPos = ScreenToWorld(
                            inputActions.Mobile.TouchPos.ReadValue<Vector2>()
                        );
                        PlayerController.i.line.SetPosition(1, new(hit.point.x, 0, hit.point.z));

                    }
                    else
                    {
                        PlayerController.i.line.enabled = false;
                    }


                }
                else if (hit.collider.gameObject.name == "Collider")
                {
                }
            }
            else
            {
                PlayerController.i.line.enabled = false;
            }
        }
    }

    private Vector3 ScreenToWorld(Vector2 mousePos)
    {
        int layerMast = 1 << 3;
        layerMast = ~layerMast;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMast))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.white, 10);
            if (hit.collider.gameObject.tag == "Floor")
            {
  
                if (hit.point.z < PlayerController.i.gameObject.transform.position.z)
                    return new(hit.point.x, 0, hit.point.z);
                else
                    return Vector3.zero;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 10);
                return Vector3.zero;
            }
        }
        return Vector3.zero;
    }

    private Vector3 PositionDifference(Vector3 start, Vector3 end)
    {
        return new(end.x - start.x, 0, end.z - start.z);
    }

    private void OnDrag(InputAction.CallbackContext context)
    {
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
        if (ScreenToWorld(inputActions.Mobile.TouchPos.ReadValue<Vector2>()) != Vector3.zero)
            PlayerController.i.endPos = ScreenToWorld(
                inputActions.Mobile.TouchPos.ReadValue<Vector2>()
            );
        else
            PlayerController.i.line.enabled = false;
    }

    private void TouchEnd(InputAction.CallbackContext context)
    {
        if (!enabled)
        {
            return;
        }

        if (
            PlayerController.i.startPos == Vector3.zero
            || PlayerController.i.line.GetPosition(0) == Vector3.zero
            || PlayerController.i.line.GetPosition(1) == Vector3.zero
            || PlayerController.i.endPos == Vector3.zero
            || PlayerController.i.line.enabled == false
        )
        {

            return;
        }

        if (
            PlayerController.i.startPos != PlayerController.i.endPos
            && PlayerController.i.startPos != Vector3.zero
        )
        {
            PlayerController.i.Launch(
                PositionDifference(PlayerController.i.startPos, PlayerController.i.endPos)
            );
            //PlayerController.i.startPos = PlayerController.i.endPos;
            PlayerController.i.line.SetPosition(1, PlayerController.i.endPos);
            PlayerController.i.line.SetPosition(0, PlayerController.i.endPos);
        }
    }
}
