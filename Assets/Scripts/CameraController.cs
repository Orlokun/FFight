using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainCameraState
{
    RegularFollowPlayer,
    PlayerInsideStillFrame,
    FollowMultipleTargets,
}

public class CameraController : MonoBehaviour
{
    public Camera camera;
    public MainCameraState actualCameraState;

    Transform currentTarget;
    Vector3 actualTargetPosition;

    List<Transform> targets;

    float zoom;
    float xOffset;
    float yOffset;
    float yPosition;
    float cameraSmoother;

    private bool onPlayersRight = true;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        yPosition = currentTarget.position.y + yOffset;
    }
    // Update is called once per frame
    void Update()
    {
        switch (actualCameraState)
        {
            case MainCameraState.RegularFollowPlayer:
                RegularFollowPlayer();
                break;
            case MainCameraState.PlayerInsideStillFrame:
                StillCamera();
                break;
            case MainCameraState.FollowMultipleTargets:
                break;
                FollowPlayerMultipleTargets();
            default:
                break;
        }
    }

    private void RegularFollowPlayer()
    {
        if (onPlayersRight)
        {
            actualTargetPosition = new Vector3(currentTarget.position.x + xOffset, yPosition, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, actualTargetPosition, cameraSmoother * Time.deltaTime);
        }
        else
        {
            actualTargetPosition = new Vector3(currentTarget.position.x - xOffset, yPosition, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, actualTargetPosition, cameraSmoother * Time.deltaTime);
        }
    }

    private void StillCamera()
    {
        if (transform.position.x == actualTargetPosition.x && actualTargetPosition.y == transform.position.y)
        {
            return;                
        }
        transform.position = Vector3.Lerp(transform.position, actualTargetPosition, cameraSmoother * Time.deltaTime);
        // Do nothing, for now. 
    }

    private void FollowPlayerMultipleTargets()
    {

    }

    #region Getters&Setters

    public float GetXOffset()
    {
        return xOffset;
    }

    public Vector2 GetActualTargetPosition()
    {
        return actualTargetPosition;
    }

    public void SetInitialVariables(float _xOffset, float _yOffset, float _camSMoother)
    {
        xOffset = _xOffset;
        yOffset = _yOffset;
        cameraSmoother = _camSMoother;
    }


    public void SetTarget(Transform incomingTarget)
    {
        currentTarget = incomingTarget;
    }

    public void SetInitialPosition()
    {
        transform.position = new Vector3(currentTarget.position.x + xOffset, currentTarget.position.y + yOffset, transform.position.z);
    }

    public void SetCameraState(MainCameraState directorsOrder)
    {
        actualCameraState = directorsOrder;
    }

    public void SetMultipleTargets(MainCameraState directorsOrder, Transform[] targets)
    {
        actualCameraState = directorsOrder;
        
    }

    public void SetCameraState(MainCameraState directorsOrder, bool movingRight)
    {
        actualCameraState = directorsOrder;
        onPlayersRight = movingRight;
    }

    public MainCameraState GetCameraState()
    {
        return actualCameraState;
    }
    #endregion
}