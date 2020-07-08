using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputGameMatchInputManager : MonoBehaviour
{
    #region GlobalVariables

    private struct MatchVariables
    {
        int localPlayers;
        int matchLength;
        int localBots;

        private Dictionary<int, string> playerTypes;

    }

    //Player Variables
    private int numberOfPlayers;
    PlayerController pController;
    CharacterMovementState pActualMovState;



    #endregion


    #region AwakeFunctions

    void Awake()
    {
        //SetMatchPlayers();
        pController = FindObjectOfType<PlayerController>();
        pActualMovState = pController.GetPlayerMovState();
    }

    #region SetLocalMultiplayer
    public void SetMatchPlayers()
    {

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

    #endregion
    #endregion

    #region UpdateFunctions

    void Update()
    {
        //PlayerHorizontalInputManager();
        PlayerJumpInputManager();
    }


    #region PlayerHorizontalInput

    private bool CheckPlayerHorizontalMove(Vector2 pMovement)
    {
        return pMovement.x != 0 && pMovement.x > 0 || pMovement.x < 0;
    }

    private void PressHorizontalInput(InputAction.CallbackContext context)
    {
        Vector2 pMovement = context.ReadValue<Vector2>();
        if (pMovement != Vector2.zero)
        {
            pActualMovState = CharacterMovementState.Walking;

            pController.ReceiveHorizontalInput(pActualMovState, pMovement);

        }


        if (CheckPlayerHorizontalMove(pMovement))
        {
            


            /*if (PlayerIsRunning())
            {
                pActualMovState = CharacterMovementState.Running;
            }
            else if (PlayerIsRunning() == false)
            {
                pActualMovState = CharacterMovementState.Walking;
            }
            */
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

    private void PressRun(InputAction.CallbackContext context)
    {
        pActualMovState = CharacterMovementState.Running;
        UpdatePlayerState();
    }

    private void UpdatePlayerState()
    {

    }

    private void Onmove(InputAction.CallbackContext context)
    {

    }

    private bool IsPlayerPressingMovement(InputAction.CallbackContext context)
    {
        return true;
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
    #endregion
}
