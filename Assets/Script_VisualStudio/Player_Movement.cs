using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Player_Movement : MonoBehaviour
{

    [Header("Player Settings")]

    public float playerSpeed = 5;
    public float jumpForce = 10;
    public float wallJumpXforce = 5;
    public float wallJumpYforce = 8;
    public float wallJumpXspeed = 0;
    public int jumpAmount = 0;
    bool canPlayerDoubleJump;
    bool doubleJumpCheck;

    [Header("Gravity Settings")]
    public float baseGravity = 2;
    public float maxFallSpeed = 10f;
    public float maxSpeedMultiplier = 2f;
    public float maxWalledSpeed = 5f;
    


    bool isGrounded;
    bool isGroundedYleft;
    bool isGroundedYright;

    [Header("Ground Check")]
    public Transform groundCheckTranformer;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
    public LayerMask groundLayer;

    Rigidbody2D rigidBody2D;
    float horizzontalMovement = 0;

    [Header("Wall Check")]
    public Transform leftgroundCheckYTranformer;
    public Transform rightgroundCheckYTranformer;
    public Vector2 groundCheckYSize = new Vector2(0.5f, 0.1f);
    public LayerMask groundYLayer;


    float verticalMovement = 0;

    [Header("Components")]
    public Animator animator;
    public SpriteRenderer playRenderer;
    public AudioSource audioSource;

    [Header("SFX")]
    public AudioClip jumpSFX;


    public void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {

        animator.SetFloat("speedY", rigidBody2D.linearVelocityY);
        animator.SetFloat("speed", Mathf.Abs(rigidBody2D.linearVelocityX));

        if (Mathf.Abs(rigidBody2D.linearVelocityX) > 0.01f)
        {
            bool needFlip = rigidBody2D.linearVelocityX < 0;
            playRenderer.flipX = needFlip;
        }


    }


    public void FixedUpdate()
    {
        rigidBody2D.linearVelocityX = (horizzontalMovement * playerSpeed) + wallJumpXspeed;
        GroundCheck();
        GroundCheckY();
        SetGravity();
        

        if (wallJumpXspeed != 0)
        { wallJumpXspeed *= 0.92f; }
        if(Mathf.Abs(wallJumpXspeed)<0.01f)
        { wallJumpXspeed = 0; }

        if(rigidBody2D.linearVelocityX > 0)
              rigidBody2D.linearVelocityX = Mathf.Min(playerSpeed, rigidBody2D.linearVelocityX);
        if(rigidBody2D.linearVelocityX < 0)
              rigidBody2D.linearVelocityX = Mathf.Min(-playerSpeed, rigidBody2D.linearVelocityX);
    }


    public void PlayerInput_Move(CallbackContext context)
    {

        horizzontalMovement = context.ReadValue<Vector2>().x;

    }
    public void PlayerInput_Jump(CallbackContext context)

    {
        if (context.performed)
        {




            if (isGrounded)

            {

                rigidBody2D.linearVelocityY = jumpForce;


                audioSource.PlayOneShot(jumpSFX);
            }



            else if (isGroundedYright)
            {
                wallJumpXspeed = -wallJumpXforce;
                rigidBody2D.linearVelocityY = wallJumpYforce;
                audioSource.PlayOneShot(jumpSFX);

            }

            else if (isGroundedYleft)
            {
                wallJumpXspeed = wallJumpXforce;
                rigidBody2D.linearVelocityY = wallJumpYforce;
                audioSource.PlayOneShot(jumpSFX);

            }

            else


            {
                if (canPlayerDoubleJump == true && doubleJumpCheck == false)
                {
                    doubleJumpCheck = true;
                    rigidBody2D.linearVelocityY = jumpForce / 2;
                    audioSource.PlayOneShot(jumpSFX);
                }





            }
        }







        if (context.canceled && rigidBody2D.linearVelocityY > 0)
        {
            rigidBody2D.linearVelocityY = rigidBody2D.linearVelocityY / 2;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(groundCheckTranformer.position, groundCheckSize);
        Gizmos.DrawCube(rightgroundCheckYTranformer.position, groundCheckYSize);
        Gizmos.DrawCube(leftgroundCheckYTranformer.position, groundCheckYSize);
    }

    public void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckTranformer.position, groundCheckSize, 0, groundLayer))
        {
            isGrounded = true;
            doubleJumpCheck = false;
        }

        else { isGrounded = false; }
    }


    public void GroundCheckY()
    {
        if (Physics2D.OverlapBox(rightgroundCheckYTranformer.position, groundCheckYSize, 0, groundYLayer))
        {
            isGroundedYright = true;

        }

        else { isGroundedYright = false; }

        if (Physics2D.OverlapBox(leftgroundCheckYTranformer.position, groundCheckYSize, 0, groundYLayer))
        {
            isGroundedYleft = true;

        }

        else { isGroundedYleft = false; }
    }
    public void SetGravity()

    {
        if (rigidBody2D.linearVelocityY < 0)
        {

            rigidBody2D.gravityScale = baseGravity * maxSpeedMultiplier;


            if (isGroundedYright)
            {



                rigidBody2D.linearVelocityY = Mathf.Max(rigidBody2D.linearVelocityY, -maxWalledSpeed);


            }

            else

            {

                rigidBody2D.linearVelocityY = Mathf.Max(rigidBody2D.linearVelocityY, -maxFallSpeed);
            }

            if (isGroundedYleft)
            {



                rigidBody2D.linearVelocityY = Mathf.Max(rigidBody2D.linearVelocityY, -maxWalledSpeed);


            }

            else

            {

                rigidBody2D.linearVelocityY = Mathf.Max(rigidBody2D.linearVelocityY, -maxFallSpeed);
            }

        }

        else
        {

            rigidBody2D.gravityScale = baseGravity;
        }


    }

   
    public void ActivateDoubleJump()

    {
        canPlayerDoubleJump = true;


    }

}




