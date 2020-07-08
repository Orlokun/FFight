using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirector : MonoBehaviour
{
    PlayerController pController;
    CameraController cController;

    Transform playerTransform;
    Vector2 playerPosition;

    //Global Camera Values
    float xOffsetOuterBorder;
    float xOffsetInnerBorder;
    float cameraXOffset;
    float cameraYOffset;
    float cameraSmoother;

    //State Values
    bool pMovingRight;
    Vector2 actualTarget;

    #region AwakeFunctions
    private void Awake()
    {
        pController = FindObjectOfType<PlayerController>();
        playerTransform = pController.GetComponent<Transform>();
        cController = FindObjectOfType<CameraController>();

        SetDirectionInitialVariables();
        cController.SetInitialVariables(cameraXOffset, cameraYOffset, cameraSmoother);
        cController.SetTarget(playerTransform);
        cController.SetInitialPosition();

        pMovingRight = true;
        cController.SetCameraState(MainCameraState.RegularFollowPlayer, pMovingRight);
    }

    private void SetDirectionInitialVariables()
    {
        float camHeight = 2f * cController.camera.orthographicSize;
        float camWidth = camHeight * cController.camera.aspect;

        cameraXOffset = camWidth / 6;
        cameraYOffset = camHeight / 4;
        cameraSmoother = 3f;
        xOffsetInnerBorder = camWidth / 12;
        xOffsetOuterBorder = camWidth / 4;
    }
    #endregion
    #region UpdateFunctions
    private void Update()
    {
        if (PlayerInStillZone() == false || PlayerInCenter())
        {
            bool leftRight = WhereIsPlayerLooking();
            cController.SetCameraState(MainCameraState.RegularFollowPlayer, leftRight);
        }
    }

    private bool PlayerInStillZone()
    {
        Debug.DrawLine(new Vector3(transform.position.x - xOffsetOuterBorder, transform.position.y, 0), new Vector3(transform.position.x - xOffsetOuterBorder, transform.position.y - cameraYOffset, 0), Color.red);
        Debug.DrawLine(new Vector3(transform.position.x + xOffsetOuterBorder, transform.position.y, 0), new Vector3(transform.position.x + xOffsetOuterBorder, transform.position.y - cameraYOffset, 0), Color.red);

        playerPosition = playerTransform.position;
        return Mathf.Abs(transform.position.x - playerPosition.x) < xOffsetOuterBorder;
    }

    private bool PlayerInCenter()
    {
        return Mathf.Abs(transform.position.x - playerPosition.x) < xOffsetInnerBorder;
    }

    private bool WhereIsPlayerLooking()
    {
        return pController.GetPlayerDirection();
    }

    #endregion
    #region PublicFunctions
    public void PlayerChangedDirection()
    {
        if (PlayerInStillZone())
        {
            actualTarget = cController.GetActualTargetPosition();
            cController.SetCameraState(MainCameraState.PlayerInsideStillFrame);
        }
    }
    #endregion
}

