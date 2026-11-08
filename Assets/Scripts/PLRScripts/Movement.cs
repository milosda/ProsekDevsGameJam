using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public PlayerInput Inputs;
    public GameObject MovedObject;
    private Rigidbody2D Rg2D;
    private BoxCollider2D Collider;
    
    public bool CanMove;
    public bool CanLook;
    
    public bool Grounded { get; set; }
    private bool PLRInputJump;
    private bool PLRJumping;
    private Vector2 Dir;

    public float MinAcceleration = 8f;
    private float ChangingDirAcceleration = 30f;
    private float MaxMoveVelocity = 40f;
    public float JumpForce = 100f;


    
    private void Move()
    {
        if (Dir.x != Rg2D.linearVelocity.x / Mathf.Abs(Rg2D.linearVelocity.x) && Dir.x != 0) 
        {
            Rg2D.AddForce(Dir * new Vector2(ChangingDirAcceleration*Mathf.Abs(((MinAcceleration+Rg2D.linearVelocityX)/MaxMoveVelocity)),0),ForceMode2D.Impulse); //kdyžtak dotweakovat movement kk?
        }


        if (Rg2D.linearVelocityX < MaxMoveVelocity) 
        {
            Rg2D.AddForce(Dir * MinAcceleration);
            
        }
        
    }

    private void Jump() 
    {
        if (Grounded == true && PLRInputJump == true && PLRJumping == false) 
        {
            PLRJumping = true;
            Rg2D.AddForce(Vector2.up * JumpForce,ForceMode2D.Impulse);



        }
    }

    private void IsGrounded() 
    {
        RaycastHit2D Raycast = Physics2D.Raycast(new Vector2(MovedObject.transform.position.x + Collider.size.x / 1.8f, MovedObject.transform.position.y - Collider.size.y / 1.8f), Vector2.down,0.2f);
        RaycastHit2D Raycast2 = Physics2D.Raycast(new Vector2(MovedObject.transform.position.x - Collider.size.x / 1.8f, MovedObject.transform.position.y - Collider.size.y / 1.8f), Vector2.down,0.2f);


        

        

        if (Raycast) 
        {
            if (Raycast.collider.gameObject.tag == "Ground" ) 
            {
                Grounded = true;
                PLRJumping = false;
            }
            return;
        }

        if (Raycast2) 
        {
            if (Raycast2.collider.gameObject.tag == "Ground") 
            {
                Grounded = true;
                PLRJumping = false;
            }
            return;
        }

        Grounded = false;
    }


    private void Awake()
    {
        Rg2D = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();

        if (CanMove == true)
        {
            Dir = Inputs.ReadPlrInput();
            PLRInputJump = Inputs.ReadJump();
        }
    }


    private void Update()
    {
        if (CanMove == true)
        {
            Dir = Inputs.ReadPlrInput();
            PLRInputJump = Inputs.ReadJump();
        }
    }




    private void FixedUpdate()
    {
        
        Debug.Log(Grounded);
        Debug.Log(PLRInputJump);
        Debug.Log(PLRJumping);
        

        IsGrounded();
        Move();
        Jump();

    }




}
