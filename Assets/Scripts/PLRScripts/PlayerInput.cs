using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputAction DirVector;
    private InputAction Jump;

    void Awake()
    {
        DirVector = InputSystem.actions.FindAction("PLRMovementAction");

        Jump = InputSystem.actions.FindAction("Jump");
    }

    public Vector2 ReadPlrInput() 
    {
        return DirVector.ReadValue<Vector2>();
    }

    public bool ReadJump() 
    {
        return (Jump.ReadValue<float>() == 1f) ? true : false;
    }

}
