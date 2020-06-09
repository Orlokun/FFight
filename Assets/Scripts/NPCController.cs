using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class NPCController : MonoBehaviour
{
    private CharacterMovementState npcStateAction;
    Rigidbody2D rBody;
    Animator npcAnim;
    SpriteRenderer spriteRender;

    //MovementVariables
    protected int directionX = 1;
    protected bool canAccelerate;
    protected float actualSpeed;
    protected float speedX;
    protected float speedY;
    protected float acceleration;
    protected string jumpTag = "Ground";

    [SerializeField]
    protected float maxXSpeed = 1.3f;
    protected float maxRunX = 2;
    protected float initialMaxSpeed;

    protected float maxYSpeed = 7f;
    protected float maxAcceleration = 1;

    //JumpVariables
    protected float npcSizeX;
    protected float npcSizeY;
    protected float groundDistance;
    protected int maxJumpAmount;
    protected bool isGrounded;

    #region StartFunctions
    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetInitialVariables();
    }

    protected virtual void SetInitialVariables()
    {
        npcStateAction = CharacterMovementState.Idle;
        rBody = GetComponent<Rigidbody2D>();
        npcAnim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        npcSizeX = SetPlayersizeX();
        npcSizeY = SetPlayerSizeY();
        groundDistance = npcSizeX / 2 + .03f;
        maxJumpAmount = 1;
        initialMaxSpeed = maxXSpeed;
    }

    private float SetPlayersizeX()
    {
        Collider2D myCol = GetComponent<Collider2D>();
        return myCol.bounds.size.x;
    }
    private float SetPlayerSizeY()
    {
        Collider2D myCol = GetComponent<Collider2D>();
        return myCol.bounds.size.y;
    }

    #endregion

    private void ResetVariables()
    {
        canAccelerate = true;
        isGrounded = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        IsItGrounded();
        npcAnim.SetBool("grounded", isGrounded);
        switch (npcStateAction)
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
            default:
                break;
        }

        speedX = GetActualSpeed();
        SetPlayerLocalScaleX();
        rBody.velocity = new Vector2(speedX, rBody.velocity.y);
    }

    void SetPlayerLocalScaleX()
    {
        transform.localScale = new Vector3(directionX, transform.localScale.y, transform.localScale.z);
    }

    protected void Walk()
    {
        if (canAccelerate)
        {
            Accelerate();
        }
    }

    protected void Run()
    {
        if (canAccelerate)
        {
            Accelerate();
        }
    }

    protected void Idle()
    {
        Deaccelerate();
    }

    #region Movement

    private bool IsItGrounded()
    {
        Vector2 myPosition = new Vector2(transform.position.x, transform.position.y);

        Vector2 leftRayPosition = new Vector2(myPosition.x - npcSizeX / 2, myPosition.y);
        Vector2 rightRayPosition = new Vector2(myPosition.x + npcSizeX / 2, myPosition.y);

        LayerMask floorMask = LayerMask.GetMask("Floor");
        RaycastHit2D leftRay = Physics2D.Raycast(leftRayPosition, Vector2.down, groundDistance, floorMask);
        RaycastHit2D rightRay = Physics2D.Raycast(rightRayPosition, Vector2.down, groundDistance, floorMask);

        Debug.DrawLine(leftRayPosition, new Vector2(leftRayPosition.x, leftRayPosition.y - groundDistance), Color.red);
        Debug.DrawLine(rightRayPosition, new Vector2(rightRayPosition.x, rightRayPosition.y - groundDistance), Color.red);


        return leftRay.collider != null && leftRay.collider.tag == jumpTag || rightRay.collider != null && rightRay.collider.tag == jumpTag;
    }

    private void Accelerate()
    {
        if (canAccelerate)
        {
            acceleration += .1f;
            if (acceleration >= maxAcceleration)
            {
                canAccelerate = false;
            }
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

    private void CheckSpeedIsRight()
    {
        if (directionX > 0 && speedX < 0 || directionX < 0 && speedX > 0)
        {
            speedX *= -1;
        }
    }

    private float GetActualSpeed()
    {
        actualSpeed = maxXSpeed * acceleration * directionX;
        return actualSpeed;
    }

    #endregion


    #region ReceiveDirections

    public void ReceiveHorizontalMovement(CharacterMovementState incomingAction, bool rightDirection)
    {
        CheckMovementStateReceived(incomingAction);
        CheckDirectionMovement(rightDirection);
    }

    private void CheckMovementStateReceived(CharacterMovementState _action)
    {
        switch (_action)
        {
            case CharacterMovementState.Running:
                SetRunning(_action);
                break;
            case CharacterMovementState.Walking:
                SetWalking(_action);
                break;
            case CharacterMovementState.Idle:
                SetIdle(_action);
                break;
        }
    }

    private void SetRunning(CharacterMovementState _action)
    {
        if (npcStateAction != CharacterMovementState.Running || maxXSpeed < 1.1f)
        {
            maxXSpeed = maxRunX;
        }
        SetAnimState("running", true);
        npcStateAction = _action;
    }

    private void SetWalking(CharacterMovementState _action)
    {
        if (npcStateAction != _action || maxXSpeed != initialMaxSpeed)
        {
            maxXSpeed = initialMaxSpeed;
        }

        SetAnimState("walking", true);
        npcStateAction = _action;
    }

    private void SetIdle(CharacterMovementState _action)
    {
        npcStateAction = _action;
        SetAnimState("idle", true);

    }

    private void CheckDirectionMovement(bool rightDirection)
    {
        if (rightDirection && directionX == -1 || rightDirection == false && directionX == 1)
        {
            ResetDirectionX();          //Check whatItDoes{
            ResetAcceleration();
        }
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

    #region Getters & Setters

    #endregion

    private void SetAnimState(string _var, bool _value)     //Review System
    {
        switch (_var)
        {
            case "walking":
                npcAnim.SetBool(_var, _value);
                npcAnim.SetBool("running", !_value);
                npcAnim.SetBool("idle", !_value);
                break;
            case "running":
                npcAnim.SetBool(_var, _value);
                npcAnim.SetBool("walking", !_value);
                npcAnim.SetBool("idle", !_value);
                break;
            case "idle":
                npcAnim.SetBool(_var, _value);
                npcAnim.SetBool("running", !_value);
                npcAnim.SetBool("walking", !_value);
                break;
        }
    }
}
