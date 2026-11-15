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
    private PlayerState PlayerStateScript;
    public Animator PLRAnimator;
    private SpriteRenderer PLRSprite;
    
    public bool CanMove;
    public bool CanLook;
    
    public bool Grounded { get; set; }
    private bool PLRInputJump;
    private bool PLRJumping;
    private bool PLRInputDash;
    private bool PLRDashing;

    private Vector2 Dir;
    private Vector2 FacingDir;

    public float MinAcceleration = 8f;
    private float ChangingDirAcceleration = 30f;
    private float MaxMoveVelocity = 40f;
    public float JumpForce = 100f;
    public float DashForce = 50f;
    private float DashCooldown = 4f;


    
    private void Move()
    {



        if (Dir.x != Rg2D.linearVelocity.x / Mathf.Abs(Rg2D.linearVelocity.x) && Dir.x != 0) 
        {
            Rg2D.AddForce(Dir * new Vector2(ChangingDirAcceleration*Mathf.Abs(((MinAcceleration+Rg2D.linearVelocityX)/MaxMoveVelocity)),0),ForceMode2D.Impulse); //kdyžtak dotweakovat movement kk?
        }


        if (Mathf.Abs(Rg2D.linearVelocityX) < MaxMoveVelocity) 
        {
            Rg2D.AddForce(Dir * MinAcceleration);
            
        }

       

        if (Mathf.Abs(Rg2D.linearVelocityX) > 0) 
        {
            PLRAnimator.SetBool("Running", true);
        }
        else 
        {
            PLRAnimator.SetBool("Running", false);
        }

        
    }

    private void Jump() 
    {
        Debug.Log(Grounded);
        Debug.Log(PLRInputJump);

        if (Grounded == true && PLRInputJump == true && PLRJumping == false) 
        {
            
            PLRJumping = true;
            Rg2D.AddForce(Vector2.up * JumpForce*5,ForceMode2D.Impulse);



        }
        
        if (PLRJumping == true) 
        {
            PLRAnimator.SetBool("Jumping", true);
        }
        else 
        {
            PLRAnimator.SetBool("Jumping", false);
        }
        
    }

    private void PLRFaceDir()
    {
        if (Dir.x == 1) 
        {
            FacingDir = Vector2.right;
            PLRSprite.flipX = false;
            
        }
        else if (Dir.x == -1)
        {
            FacingDir = Vector2.left;
            PLRSprite.flipX = true;
        }
    }

    private void Dash() 
    {
        DashCooldown -= Time.deltaTime;
        if (DashCooldown <= 0 && PLRInputDash == true)
        {
            
            Rg2D.AddForce(FacingDir * DashForce, ForceMode2D.Impulse);
            DashCooldown = 4f;
        }

    }


    private void IsGrounded() 
    {
        RaycastHit2D Raycast = Physics2D.Raycast(new Vector2(MovedObject.transform.position.x + Collider.size.x / 1.8f, MovedObject.transform.position.y - Collider.size.y / 1.8f), Vector2.down,0.5f);
        RaycastHit2D Raycast2 = Physics2D.Raycast(new Vector2(MovedObject.transform.position.x - Collider.size.x / 1.8f, MovedObject.transform.position.y - Collider.size.y / 1.8f), Vector2.down,0.5f);



        


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
        PlayerStateScript = GetComponent<PlayerState>();
        PLRSprite = GetComponent<SpriteRenderer>();


        if (CanMove == true && PlayerStateScript.DeathState == "Alive")
        {
            Dir = Inputs.ReadPlrInput();
            PLRInputJump = Inputs.ReadJump();
            FacingDir = Vector2.right;
        }
    }


    private void Update()
    {
        if (CanMove == true && PlayerStateScript.DeathState == "Alive")
        {
            Dir = Inputs.ReadPlrInput();
            PLRInputJump = Inputs.ReadJump();
            PLRInputDash = Inputs.ReadDash();
            PLRFaceDir();
        }
    }




    private void FixedUpdate()
    {
        IsGrounded();
        Move();
        Jump();
        Dash();

    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Grounded = true;
            PLRJumping = false;
            return;
        }

        Grounded = false;
        
    }
    */





}
