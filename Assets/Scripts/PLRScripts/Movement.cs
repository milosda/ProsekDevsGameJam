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
    public float MaxMoveAcceleration = 10f;
    public float MaxAccelerationFinal = 2;
    public float MinJumpAcceleration = 5f;
    public float GravityAcceleration = 1f;
    public float MinDeacceleration = 0.999f;
    private float MaxDeacceleration = 0.9f;



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
        Rg2D.MovePosition(MovedObject.transform.position + new Vector3(CurrVelocity.x, Mathf.Clamp(CurrVelocity.y,-0.001f,0.001f), 0));
    }

    private void DecreaseAllSpeedAndAcceleration() 
    {
        CurrVelocity = new Vector2(CurrVelocity.x * 0.1f, CurrVelocity.y); // rychlost by měla být měněna pomocí modifier systému, ale to je na potom
        CurrAcceleration = new Vector2(CurrAcceleration.x * 0.1f, CurrAcceleration.y * 0.8f);
        CurrMoveAcceleration = new Vector2(CurrMoveAcceleration.x * Mathf.Clamp(CurrMoveAcceleration.x / MaxMoveAcceleration, MaxDeacceleration, MinDeacceleration), CurrMoveAcceleration.y * 0.9f);
        CurrAccelerationFinal = new Vector2(CurrAccelerationFinal.x * Mathf.Clamp(CurrAccelerationFinal.x / MaxAccelerationFinal, MaxDeacceleration, MinDeacceleration), CurrAccelerationFinal.y * 0.9f);
    }

    private void Gravity() 
    {
        Debug.Log(Grounded);
        if (Grounded == false && PLRInputJump == false && LastFrameGrounded == false)
        {
            CurrMoveAcceleration -= new Vector2(0, GravityAcceleration * Time.deltaTime);
            Debug.Log("Gravity");
        }
        else if (Grounded == true && PLRInputJump == false && LastFrameGrounded == false) 
        {
            LastFrameGrounded = true;
            CurrMoveAcceleration = new Vector2(CurrMoveAcceleration.x, 0);
            Debug.Log("Grounded");
        }
        /*
        else if (Grounded == true && PLRInputJump == false && LastFrameGrounded == true) 
        {
            tohle není potřeba ale je to tu pro mne protože jsem idiot a zapomínám jak tento kod funguje trust fr
        }
        */
    }


    private void IsGrounded() 
    {
        RaycastHit2D raycast = Physics2D.Raycast(new Vector2(MovedObject.transform.position.x,MovedObject.transform.position.y - 0.51f), Vector2.down,0.2f);

       
        /*
        if (raycast == null) 
        {
            if (raycast.collider.gameObject.tag == "Ground")
            {
                Debug.Log("yay");
                Grounded = true;
            }
        }
        */
    }



    private void Jump() 
    {
        if (PLRInputJump == true && Grounded == true) 
        {
            PLRJumping = true;
        }
        else if (PLRJumping == true) 
        {
            CurrMoveAcceleration += new Vector2(0, MinJumpAcceleration * Time.deltaTime);

            if (CurrMoveAcceleration.y > 0.8f) 
            {
                PLRJumping = false;
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

        IsGrounded();
        Jump();
        Gravity();
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
        return PLRJumping;
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

    




}
