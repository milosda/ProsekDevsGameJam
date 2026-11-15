using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputAction DirVector;
    private InputAction Jump;

    private InputAction Dash;

    private InputAction KillYourself;


    private InputAction Menu;
    public GameObject GameMenu;
    void Awake()
    {
        DirVector = InputSystem.actions.FindAction("PLRMovementAction");

        Jump = InputSystem.actions.FindAction("Jump");

        Dash = InputSystem.actions.FindAction("Dash");

        KillYourself = InputSystem.actions.FindAction("KillYourself");

        Menu = InputSystem.actions.FindAction("Menu"); 
    }

    public Vector2 ReadPlrInput() 
    {
        return DirVector.ReadValue<Vector2>();
    }

    public bool ReadJump() 
    {
        return (Jump.ReadValue<float>() == 1f) ? true : false;
    }

    public bool ReadDash() 
    {
        return (Dash.ReadValue<float>() == 1f) ? true : false;
    }

    public bool ReadKillButton() 
    {
        return (KillYourself.ReadValue<float>() == 1f) ? true : false;
    }

    void Update()
    {
        if (Menu.ReadValue<float>() == 1f) 
        {
            GameMenu.SetActive(true);
        }
    }

}
