
using UnityEngine;
using UnityEngine.InputSystem;

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
        inputActions.Mobile.TouchPress.started += context => TouchStart(context);
        inputActions.Mobile.TouchPress.canceled += context => TouchEnd(context);

    }

    private void TouchStart(InputAction.CallbackContext context)
    {
        {
            Vector2 mousePos = inputActions.Mobile.TouchPos.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                
                    PlayerController.i.startPos = new(hit.point.x,0,hit.point.z);
                    Debug.Log("Start Pos: " + PlayerController.i.startPos);
                    PlayerController.i.line.positionCount = 2;
                    PlayerController.i.line.SetPosition(0, PlayerController.i.startPos);
                    PlayerController.i.line.SetPosition(1, PlayerController.i.startPos);
                
            }

        }
    
    }
    private void TouchEnd(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputActions.Mobile.TouchPos.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            {
                PlayerController.i.endPos = new(hit.point.x,0,hit.point.z);
                Debug.Log("End Pos: " + PlayerController.i.endPos);
        PlayerController.i.line.SetPosition(1, PlayerController.i.endPos);
            }
        }

      //  DeltaPos(PlayerController.i.startPos,PlayerController.i.endPos);
        
    }
    private void DeltaPos(Vector2 startPos, Vector2 endPos)
    {
        Vector2 deltaPos = endPos - startPos;
        Debug.Log("Delta Pos: " + deltaPos);
        PlayerController.i.Launch(deltaPos);
    }

}
