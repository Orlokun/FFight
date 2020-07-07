using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region GlobalVariables

    //Player Variables
    private int numberOfPlayers;
    PlayerController pController;
    CharacterMovementState pActualMovState;
    
    
    
    #endregion

    private struct MatchPlayerType
    {
        Dictionary<int, string> playerTypes;
    }
    // Start is called before the first frame update
    void Awake()
    {
        SetMatchPlayers();
        pController = FindObjectOfType<PlayerController>();
        pActualMovState = pController.GetPlayerMovState();
    }

    public void SetMatchPlayers()
    {
        numberOfPlayers = LevelDataManager.GetNumberOfPlayers();
        if (numberOfPlayers != 0)
        {
            CheckNumberOfPlayers();
        }
    }

    void CheckNumberOfPlayers()
    {
        switch (numberOfPlayers)
        {
            case 1:
                Set1Player();
                break;
            case 2:
                Set2Players();
                break;
            case 3:
                Set3Players();
                break;
            case 4:
                Set4Players();
                break;
            default:
                break;
        }
    }

    private void Set1Player()
    {

    }
    private void Set2Players()
    {

    }
    private void Set3Players()
    {
        //Not ready yet
    }
    private void Set4Players()
    {
        //Not ready yet
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHorizontalInputManager();
        PlayerJumpInputManager();
    }

    #region PlayerHorizontalInput
    private void PlayerHorizontalInputManager()
    {
        pActualMovState = pController.GetPlayerMovState();
        if (IsPlayerPressingMovement())
        {
            if (PlayerIsRunning())
            {
                pActualMovState = CharacterMovementState.Running;
            }
            else if (PlayerIsRunning() == false)
            {
                pActualMovState = CharacterMovementState.Walking;
            }
            pController.ReceiveHorizontalInput(pActualMovState, IsPressingRight());
        }
        else
        {
            if (pController.GetPlayerMovState() != CharacterMovementState.Idle)
            {
                pActualMovState = CharacterMovementState.Idle;
                pController.SetIncomingState(pActualMovState);
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

    #region PlayerJumpInput

    void PlayerJumpInputManager()
    {
        if (CheckJumpPressed())
        {
            pController.ReceiveJumpButton();
        }
    }

    private bool CheckJumpPressed()
    {
        return Input.GetKey(KeyCode.Space);
    }

    #endregion

}
