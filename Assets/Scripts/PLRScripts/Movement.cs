using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public PlayerInput Inputs;
    public GameObject MovedObject;
    private Rigidbody2D Rg2D;
    
    public bool CanMove;
    public bool CanLook;
    
    public bool Grounded { get; set; }
    public bool LastFrameGrounded { get; set; }
    private bool PLRInputJump;
    private bool PLRJumping;

    public float MinMoveAcceleration = 5f;
    public float MaxMoveAcceleration = 1f;
    public float MaxCurrAcceleration = 3f;
    public float MinJumpAcceleration = 5f;
    public float GravityAcceleration = 1f;
    public float MinDeacceleration = 0.999f;
    private float MaxDeacceleration = 0.9f;



    
    private Vector2 Dir;

    private Vector2 MoveAcceleration;
    private float GravAcceleration;
    public Vector2 OutsideAcceleration;
    private Vector2 CurrAcceleration;


    public Vector2 CurrVelocity;


    private void CalculateMoveAcceleration()
    {
        MoveAcceleration += new Vector2(MinMoveAcceleration, 0) * Dir * Time.deltaTime;
        MoveAcceleration = new Vector2(Mathf.Clamp(MoveAcceleration.x,-MaxMoveAcceleration,MaxCurrAcceleration), MoveAcceleration.y);
    }

    private void CalculateCurrAcceleration()
    {
        CalculateMoveAcceleration();
        CurrAcceleration = (MoveAcceleration + OutsideAcceleration) * Time.deltaTime;
    }


    private void CalculateCurrVelocity() 
    {
        CalculateCurrAcceleration();
        CurrVelocity += CurrAcceleration * Time.deltaTime;
        Rg2D.MovePosition(MovedObject.transform.position + new Vector3(CurrVelocity.x, CurrVelocity.y, 0));
    }

    private void DeAcceleration() 
    {
        CurrVelocity = new Vector2(CurrVelocity.x * 0.5f, ((PLRJumping == true) ? 0 : 1) * CurrVelocity.y);
        CurrAcceleration = new Vector2(CurrAcceleration.x * Mathf.Clamp(CurrAcceleration.x / MaxCurrAcceleration, MaxDeacceleration, MinDeacceleration), ((PLRJumping == true) ? 0 : 1) * CurrAcceleration.y);
        OutsideAcceleration = OutsideAcceleration * 0.8f * Time.deltaTime;
        MoveAcceleration = new Vector2(MoveAcceleration.x * Mathf.Clamp(MoveAcceleration.x / MaxMoveAcceleration, MaxDeacceleration, MinDeacceleration), ((PLRJumping == true) ? 0 : 1) * CurrAcceleration.y);
    
    }


    
    /*
    private void DecreaseAllSpeedAndAcceleration() 
    {
        CurrVelocity = new Vector2(CurrVelocity.x * 0.1f, CurrVelocity.y); // rychlost by měla být měněna pomocí modifier systému, ale to je na potom
        CurrAcceleration = new Vector2(CurrAcceleration.x * 0.1f, CurrAcceleration.y * 0.8f);
        CurrMoveAcceleration = new Vector2(CurrMoveAcceleration.x * Mathf.Clamp(CurrMoveAcceleration.x / MaxMoveAcceleration, MaxDeacceleration, MinDeacceleration), CurrMoveAcceleration.y * 0.9f);
        CurrAccelerationFinal = new Vector2(CurrAccelerationFinal.x * Mathf.Clamp(CurrAccelerationFinal.x / MaxAccelerationFinal, MaxDeacceleration, MinDeacceleration), CurrAccelerationFinal.y * 0.9f);
    }
    */

    private void Gravity() 
    {
       
        if (Grounded == false && PLRJumping == false && LastFrameGrounded == false)
        {
            MoveAcceleration -= new Vector2(0, GravityAcceleration * Time.deltaTime);
        }
        
        
    }


    private void IsGrounded() 
    {
        RaycastHit2D raycast = Physics2D.Raycast(new Vector2(MovedObject.transform.position.x,MovedObject.transform.position.y - 0.51f), Vector2.down,0.2f);

        if (raycast) 
        {
            if (raycast.collider.gameObject.tag == "Ground" && LastFrameGrounded == true) 
            {
                Grounded = true;
            }
            else if (raycast.collider.gameObject.tag == "Ground")
            {
                LastFrameGrounded = true;
                MoveAcceleration = new Vector2(MoveAcceleration.x, 0);
                CurrVelocity = new Vector2 (CurrVelocity.x, 0);
            }
        }
        
    }


    
    private void Jump() 
    {
        
        if (PLRInputJump == true && Grounded == true) 
        {
            PLRJumping = true;
            Grounded = false;
        }
        else if (PLRJumping == true) 
        {
            MoveAcceleration += new Vector2(0, MinJumpAcceleration * Time.deltaTime);

            Debug.Log(CurrVelocity);
            if (MoveAcceleration.y > 8f) 
            {

                PLRJumping = false;
                MoveAcceleration.y = 0;
            }
        }
    }
    


    public void Move() 
    {
        if (CanMove == true) 
        {
            Dir = Inputs.ReadPlrInput();
            PLRInputJump = Inputs.ReadJump();
        }

        Jump();
        IsGrounded();
        
        Gravity();
        CalculateCurrVelocity();
        DeAcceleration();

       
    }

    public bool IsMoving() 
    {
        return (CurrAcceleration.x > 0 || CurrAcceleration.x < 0) ? true : false;
    }

    public bool IsRisingOrFalling() 
    {
        return (CurrAcceleration.y > 0 || CurrAcceleration.y < 0) ? true : false;
    }

    public bool IsJumping()
    {
        return PLRJumping;
    }

    private void Awake()
    {
        Rg2D = GetComponent<Rigidbody2D>();

        CurrAcceleration = new Vector2(0, 0);
        OutsideAcceleration = new Vector2(0, 0);
        MoveAcceleration = new Vector2(0, 0);

        CurrVelocity = new Vector2(0, 0);

        Grounded = false;
        LastFrameGrounded = false;



        if (CanMove == true)
        {
            Dir = Inputs.ReadPlrInput();
            PLRInputJump = Inputs.ReadJump();
        }
        
        
    }

    




}
