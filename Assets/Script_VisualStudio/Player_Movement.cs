using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player_Movement : MonoBehaviour
{

    [Header("Player Settings")]

    public float playerSpeed = 5;
    public float jumpForce = 10;


    [Header("Gravity Settings")]
    public float baseGravity = 2;
    public float maxFallSpeed = 10f;
    public float maxSpeedMultiplier = 2f;


    bool isGrounded;

    [Header("Ground Check")]
    public Transform groundCheckTranformer;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
    public LayerMask groundLayer;

    Rigidbody2D rigidBody2D;
    float horizzontalMovement = 0;

    [Header("Components")]
    public Animator animator;
    public SpriteRenderer playRenderer;


    public void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

   public void Update()
    {

        animator.SetFloat("speedY", rigidBody2D.linearVelocityY);
        animator.SetFloat("speed", Mathf.Abs(rigidBody2D.linearVelocityX));
       
        if(Mathf.Abs(rigidBody2D.linearVelocityX) > 0.01f)
        {
            bool needFlip = rigidBody2D.linearVelocityX < 0;
            playRenderer.flipX = needFlip;
        }
       

    }


    public void FixedUpdate()
    {
        rigidBody2D.linearVelocityX = horizzontalMovement * playerSpeed;
        GroundCheck();
        SetGravity();

    }


    public void PlayerInput_Move(CallbackContext context)
    {

        horizzontalMovement = context.ReadValue<Vector2>().x;

    }
    public void PlayerInput_Jump(CallbackContext context)

    {
        if (isGrounded)
        {
            if (context.performed)

            {
                rigidBody2D.linearVelocityY = jumpForce;
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
    }

    public void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckTranformer.position, groundCheckSize, 0, groundLayer))
        {
            isGrounded = true;
        }

        else { isGrounded = false; }
    }

    public void SetGravity()

    {
        if (rigidBody2D.linearVelocityY < 0)
        {

            rigidBody2D.gravityScale = baseGravity * maxSpeedMultiplier;
            rigidBody2D.linearVelocityY = Mathf.Max(rigidBody2D.linearVelocityY, -maxFallSpeed);


        }
        else
        {

            rigidBody2D.gravityScale = baseGravity;
        }
    }
}

