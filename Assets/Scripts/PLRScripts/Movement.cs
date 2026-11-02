using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public PlayerInput Inputs;
    public GameObject MovedObject;
    private Rigidbody2D Rg2D;
    
    public 

    public bool CanMove;
    public bool CanLook;
    public bool CanCrouch;
    public bool Grounded { get; set; }
    private bool PLRInputJump;

    public float MinMoveAcceleration = 5f;
    public float MaxMoveAcceleration = 10f;
    public float MaxAccelerationFinal = 2;
    public float GravityAcceleration = 9.81f;


    public Vector2 CurrAcceleration;
    private Vector2 Dir;
    public Vector2 CurrMoveAcceleration { get; set; }
    public Vector2 CurrAccelerationFinal { get; set; }
    private Vector2 CurrVelocity;

    private void CalculateMovementAcceleration() 
    {
        CurrMoveAcceleration += new Vector2(MinMoveAcceleration * Time.deltaTime, 0) * Dir;
        CurrMoveAcceleration = new Vector2(Mathf.Clamp(CurrMoveAcceleration.x, -MaxMoveAcceleration, MaxMoveAcceleration),CurrMoveAcceleration.y);
    }

    private void CalculateFinalAcceleration() 
    {
        CalculateMovementAcceleration();
        CurrAccelerationFinal += (CurrAcceleration + CurrMoveAcceleration) * Time.deltaTime;
    }

    private void CalculateCurrVelocity()
    {
        CurrVelocity += CurrAccelerationFinal;
        Rg2D.MovePosition(MovedObject.transform.position + new Vector3(CurrVelocity.x, CurrVelocity.y, 0));
    }

    private void DecreaseAllSpeedAndAcceleration() 
    {
        CurrVelocity = new Vector2(CurrVelocity.x * 0.1f, CurrVelocity.y); // rychlost by měla být měněna pomocí modifier systému, ale to je na potom
        CurrAcceleration = new Vector2(CurrAcceleration.x * 0.1f, CurrAcceleration.y * 0.8f);
        CurrMoveAcceleration = new Vector2(CurrMoveAcceleration.x * Mathf.Clamp(CurrMoveAcceleration.x / MaxMoveAcceleration, 0.9f, 0.99f), CurrMoveAcceleration.y * 0.9f);
        CurrAccelerationFinal = new Vector2(CurrAccelerationFinal.x * Mathf.Clamp(CurrAccelerationFinal.x / MaxAccelerationFinal, 0.9f, 0.99f), CurrAccelerationFinal.y * 0.9f);
    }

    private void Gravity() 
    {
        if (Grounded == false && PLRInputJump == false) 
        {
            CurrMoveAcceleration -= new Vector2(0, GravityAcceleration * Time.deltaTime);
        }
    }



    private void Jump() 
    {
        
    }



    public void Move() 
    {
        if (CanMove == true) 
        {
            Dir = Inputs.ReadPlrInput();
            PLRInputJump = Inputs.ReadJump();
        }
        

        CalculateFinalAcceleration();
        CalculateCurrVelocity();
        DecreaseAllSpeedAndAcceleration();
    }

    public bool IsMoving() 
    {
        return (CurrAccelerationFinal.x > 0 || CurrAccelerationFinal.x < 0) ? true : false;
    }

    public bool IsRisingOrFalling() 
    {
        return (CurrAccelerationFinal.y > 0 || CurrAccelerationFinal.y < 0) ? true : false;
    }

    public bool IsJumping()
    {
        return PLRInputJump;
    }

    private void Awake()
    {
        Rg2D = GetComponent<Rigidbody2D>();

        CurrAcceleration = new Vector2(0, 0);
        CurrMoveAcceleration = new Vector2(0, 0);
        CurrAccelerationFinal = new Vector2(0, 0);

        CurrVelocity = new Vector2(0, 0);

        if (CanMove == true)
        {
            Dir = Inputs.ReadPlrInput();
            PLRInputJump = Inputs.ReadJump();
        }
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }




}
