using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputManager : MonoBehaviour
{
    //CameraDirectorVariables
    CameraDirector cDirector;

    //Player Variables
    PlayerController pCOntroller;
    CharacterMovementState pActualMovState;
    StageDirector sDirector;

    // Start is called before the first frame update
    void Awake()
    {
        pCOntroller = FindObjectOfType<PlayerController>();
        pActualMovState = pCOntroller.GetPlayerMovState();
        cDirector = FindObjectOfType<CameraDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHorizontalInputManager();
    }

    #region PlayerHorizontalInput

    private void PlayerHorizontalInputManager()
    {
        pActualMovState = pCOntroller.GetPlayerMovState();

        if (IsPlayerPressingMovement())
        {
            if (PlayerIsRunning())
            {
                pActualMovState = CharacterMovementState.Running;
            }

            else if(PlayerIsRunning() == false)
            {
                pActualMovState = CharacterMovementState.Walking;
            }
            pCOntroller.ReceiveHorizontalInput(pActualMovState, IsPressingRight());
        }

        else
        {
            if (pCOntroller.GetPlayerMovState() != CharacterMovementState.Idle)
            {
                pActualMovState = CharacterMovementState.Idle;
                pCOntroller.SetIncomingState(pActualMovState);
            }
        }
    }

    private bool PlayerIsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }

    private bool IsPressingRight()
    {
        return Input.GetAxisRaw("Horizontal") >= .1f;

    }

    private bool IsPlayerPressingMovement()
    {
        bool buttonRightPressed = Input.GetAxisRaw("Horizontal") > 0f;
        bool leftButtonPressed = Input.GetAxisRaw("Horizontal") < 0;

        if (buttonRightPressed && leftButtonPressed)
        {
            return false;
        }

        return buttonRightPressed == true || leftButtonPressed == true;
    }

    #endregion

    #region CameraDirectorCheck
    #endregion  
}
