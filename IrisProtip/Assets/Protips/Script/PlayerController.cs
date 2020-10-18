using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variavel de entrada horizontal e tempo de Coiote
    private float movementInputDirection;
    private float jumpTime;
    private float turnTime;
    private float wallJumpTime;

    //Variaveis de controles de mecanica
    private int amountOfJumpsLeft;
    private int facingDirection = 1;
    private int lastWallJumpDirection;

    //Variaveis Logicas
    private bool isGrounded;
    private bool canNormalJump;
    private bool canWallJump;
    private bool isWallSliding;
    private bool isTouchingWall;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    private bool canMove;
    private bool canFlip;
    private bool hasWallJumped;

    //Componentes do personagem
    private Rigidbody2D rb;
    public Transform groundCheck;
    public Transform wallCheck;

    [Header("Horzontal var")]
    public float moveSpeed = 10f;
    public float groundCheckRadius;

    [Space(3)]

    [Header("Vertical var")]
    public float jumpForce = 16f;
    public int amountOfJumps = 1;
    public float variableJumpHeightMultiplier = 0.5f;
    public float airDragMultiplier = 0.95f;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float wallJumpForce;
    public float jumpTimeSet = 0.15f;
    public float wallJumpTimeSet = 0.5f;   

    [Space(3)]

    [Header("Outros")]
    public LayerMask whatisGround;
    public bool isFacingRight = true;
    public Vector2 wallJumpDirection;
    public float turnTimeSet = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatisGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatisGround);
    }

    private void CheckIfCanJump()
    {
        if ((isGrounded && rb.velocity.y <= 0.01f))
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (isTouchingWall)
        {
            canWallJump = true;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }

    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            if (isGrounded || (amountOfJumpsLeft > 0 && !isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                jumpTime = jumpTimeSet;
                isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if (!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTime = turnTimeSet;
            }
        }

        if (turnTime >= 0)
        {
            turnTime -= Time.deltaTime;

            if (turnTime <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

    }

    private void CheckJump()
    {
       if(jumpTime > 0)
        {
            //Wall Jump
            if(!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
            {
                WallJump();
            }
            else if(isGrounded)
            {
                NormalJump();
            }
        }
        
        if(isAttemptingToJump)
        {
            jumpTime -= Time.deltaTime;
        }

        if (wallJumpTime > 0)
        {
            if (hasWallJumped && movementInputDirection == -lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                hasWallJumped = false;
            }
            else if (wallJumpTime <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTime -= Time.deltaTime;
            }
        }
    }

    private void NormalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTime = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }
    }

    private void WallJump()
    {
        if (canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTime = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            canMove = true;
            canFlip = true;
            turnTime = 0;
            hasWallJumped = true;
            wallJumpTime = wallJumpTimeSet;
            lastWallJumpDirection = -facingDirection;
        }
    }

    private void ApplyMovement()
    {

        if (!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if(canMove)
        {
            rb.velocity = new Vector2(moveSpeed * movementInputDirection, rb.velocity.y);
        }

        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }
    
    private void Flip()
    {
        if (!isWallSliding && canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
