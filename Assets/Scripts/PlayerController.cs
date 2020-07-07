using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{

    #region Global Variables

    private CameraDirector cDirector;

    Rigidbody2D rBody;
    SpriteRenderer spriteRender;
    Animator playerAnim;
    BoxCollider2D myCol;

    //Movement variables
    CharacterMovementState actualMovState;
    bool mustMove;
    Vector2 playerInput;
    Vector2 lastPosition;
    float speedX;
    float speedY;
    int directionX = 1;                 //Si es positivo estamos mirando a la derecha, si es negativo a la izquierda. 
    int directionY = 1;
    float actualSpeed;
    float acceleration;

    // static variables
    protected static float maxAcceleration = 1f;
    protected static float takeDamageRate = 1f;
    protected static float attackRate = .25f;
    protected static float jumpRate = .15f;
    protected static float poweredRare = .25f;
    protected static float maxXSpeed;
    protected static float initialMaxSpeed = 1.1f;
    protected static float runningMaxXSpeed = 1.8f;
    protected static float maxYSpeed = 7f;
    bool canAccelerate;

    // jumpVariables
    CharacterJumpState pJumpState;
    string jumpTag = "Floor";
    bool isGrounded;
    bool justJumped;
    float playerSizeX;
    float playerSizeY;
    float playerJumpSpeed = 3f;
    float groundDistance;
    float jumpAcceleration = 1;

    #endregion

    #region Awake Functions

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        SetInitialVariables();
        ResetVariables();
    }

    private void SetInitialVariables()
    {
        actualMovState = CharacterMovementState.Idle;
        pJumpState = CharacterJumpState.Grounded;
        cDirector = FindObjectOfType<CameraDirector>();
        playerSizeX = SetPlayersizeX();
        playerSizeY = SetPlayerSizeY();
        groundDistance = playerSizeY / 2 + .02f;
        maxXSpeed = initialMaxSpeed;
    }

    private float SetPlayersizeX()
    {
        myCol = GetComponent<BoxCollider2D>();
        return myCol.bounds.size.x;
    }
    private float SetPlayerSizeY()
    {
        Collider2D myCol = GetComponent<Collider2D>();
        return myCol.bounds.size.y;
    }
    private void ResetVariables()
    {
        canAccelerate = true;
        isGrounded = false;
    }
    #endregion

    #region Update Functions
    void Update()
    {
        HorizontalMove();
        JumpManaging();
        rBody.velocity = new Vector2(speedX, speedY);
        CheckGroundedconditions();
        lastPosition = transform.position;
    }

    #region HorizontalMovement
    private void HorizontalMove()
    {
        switch (actualMovState)
        {
            case CharacterMovementState.Idle:
                Idle();
                break;
            case CharacterMovementState.Walking:
                Walk();
                break;
            case CharacterMovementState.Running:
                Run();
                break;
        }
        CheckSpeedIsRight();
        speedX = GetActualSpeed() * directionX;
    }
    void SetPlayerLocalScaleX()
    {
        transform.localScale = new Vector3(directionX, transform.localScale.y, transform.localScale.z);
    }
    private void Walk()
    {
        if (canAccelerate)
        {
            Accelerate();
        }
    }
    private void Run()
    {
        if (canAccelerate)
        {
            Accelerate();
        }
    }
    private void Idle()
    {
        Deaccelerate();
    }
    private void CheckSpeedIsRight()
    {
        if (directionX > 0 && speedX < 0 || directionX < 0 && speedX > 0)
        {
            speedX *= -1;
        }
    }
    private void Accelerate()
    {
        acceleration += .1f;
        if (acceleration >= maxAcceleration)
        {
            canAccelerate = false;
        }
    }
    private void Deaccelerate()
    {
        acceleration -= .1f;
        canAccelerate = true;
        if (acceleration <= 0f)
        {
            acceleration = 0;
        }
    }
    private float GetActualSpeed()
    {
        actualSpeed = maxXSpeed * acceleration;
        return actualSpeed;
    }
    private void ResetDirectionX()
    {
        directionX *= -1;
        speedX *= -1;
        SetPlayerLocalScaleX();
    }
    private void ResetAcceleration()
    {
        acceleration = 0.1f;
        canAccelerate = true;
    }
    #endregion

    #region JumpManager
    private void JumpManaging()
    {
        switch (pJumpState)
        {
            case CharacterJumpState.Jumping:
                Jump();
                break;
            case CharacterJumpState.Falling:
                JustFall();
                break;
            case CharacterJumpState.Grounded:
                TouchGround();
                break;
            default:
                break;
        }
    }
    private void Jump()
    {
        speedY = playerJumpSpeed * directionY;
        justJumped = true;
        StartCoroutine(WaitJumpingTime());
    }
    private void JustFall()
    {
        speedY = rBody.velocity.y;
    }
    private void TouchGround()
    {
        speedY = rBody.velocity.y;
    }
    private void SetJumpState(CharacterJumpState incomingJumpState)
    {
        pJumpState = incomingJumpState;
    }
    private bool IsItGrounded()
    {
        Vector2 myPosition = new Vector2(transform.position.x, transform.position.y);

        Vector2 leftRayPosition = new Vector2(myPosition.x - playerSizeX / 2, myPosition.y);
        Vector2 rightRayPosition = new Vector2(myPosition.x + playerSizeX / 2, myPosition.y);

        LayerMask floorMask = LayerMask.GetMask("Floor");
        RaycastHit2D leftRay = Physics2D.Raycast(leftRayPosition, Vector2.down, groundDistance, floorMask);
        RaycastHit2D rightRay = Physics2D.Raycast(rightRayPosition, Vector2.down, groundDistance, floorMask);

        Debug.DrawLine(leftRayPosition, new Vector2(leftRayPosition.x, leftRayPosition.y - groundDistance), Color.red);
        Debug.DrawLine(rightRayPosition, new Vector2(rightRayPosition.x, rightRayPosition.y - groundDistance), Color.red);

        return leftRay.collider != null && leftRay.collider.tag == jumpTag || rightRay.collider != null && rightRay.collider.tag == jumpTag;
    }
    private void CheckGroundedconditions()
    {
        isGrounded = IsItGrounded();
        if (!isGrounded)
        {
            SetJumpState(CharacterJumpState.Falling);
            SetAnimState("grounded", false);
        }
        else
        {
            SetJumpState(CharacterJumpState.Grounded);
            SetAnimState("grounded", true);
        }
    }

    private IEnumerator WaitJumpingTime()
    {
        yield return new WaitForSeconds(.3f);
        justJumped = false;
    }
    #endregion

    #endregion

    #region Getters & Setters

    public int GetPlayerDirection()
    {
        return directionX;
    }

    public CharacterMovementState GetPlayerMovState()
    {
        return actualMovState;
    }

    public void SetIncomingState(CharacterMovementState _movState)
    {
        switch (_movState)
        {
            case CharacterMovementState.Walking:
                SetWalking(_movState);
                break;
            case CharacterMovementState.Running:
                SetRunning(_movState);
                break;
            case CharacterMovementState.Idle:
                SetIdle(_movState);
                break;
        }
    }

    #endregion

    #region InputReceive

    public void ReceiveHorizontalInput(CharacterMovementState _movState, bool pressedRight)
    {
        if (pressedRight && directionX == -1 || !pressedRight && directionX == 1)
        {
            cDirector.PlayerChangedDirection();
            ResetDirectionX();          //Check whatItDoes{
            ResetAcceleration();
        }
        if (_movState != actualMovState)
        {
            SetIncomingState(_movState);
        }
    }
    private void SetIdle(CharacterMovementState _movState)
    {
        actualMovState = _movState;
        SetAnimState("idle", true);
    }
    private void SetWalking(CharacterMovementState _movState)
    {
        maxXSpeed = initialMaxSpeed;
        actualMovState = _movState;
        SetAnimState("walking", true);
    }
    private void SetRunning(CharacterMovementState _movState)
    {
        maxXSpeed = runningMaxXSpeed;
        actualMovState = _movState;
        SetAnimState("running", true);
    }
    public void ReceiveJumpButton()
    {
        if (isGrounded && !justJumped)
        {
            SetJumpState(CharacterJumpState.Jumping);
        }
    }
    #endregion

    #region AnimatorController

    private void SetAnimState(string _var, bool _value)     //Review System
    {
        switch (_var)
        {
            case "walking":
                playerAnim.SetBool(_var, _value);
                playerAnim.SetBool("running", !_value);
                playerAnim.SetBool("idle", !_value);
                break;
            case "running":
                playerAnim.SetBool(_var, _value);
                playerAnim.SetBool("walking", !_value);
                playerAnim.SetBool("idle", !_value);
                break;
            case "idle":
                playerAnim.SetBool(_var, _value);
                playerAnim.SetBool("running", !_value);
                playerAnim.SetBool("walking", !_value);
                break;
            case "grounded":
                playerAnim.SetBool(_var, _value);
                break;
        }
    }

    #endregion

}

