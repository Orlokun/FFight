using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainSceneManager : MonoBehaviour
{
    //Initial Info
    [SerializeField]
    private GameObject[] buttons;
    [SerializeField]
    private Vector2[] mainbuttonPositions;
    

    private int activeButton;


    //DataForMatch
    private int localPlayers;
    private int totalPlayers;

    private void Awake()
    {
        CheckBasicParameters();
        SetPositionOfButtons();   
    }
    
    private void SetPositionOfButtons()
    {
        for (int i = 0; i)
    }


    #region MenuNavigation
    public void ChangeButtonUp(InputAction.CallbackContext context)
    {
        activeButton = GetSafeActiveButtonNumber(1);
        ChangeActiveButton(activeButton);
    }
    public void ChangeButtondown(InputAction.CallbackContext context)
    {
        activeButton = GetSafeActiveButtonNumber(-1);
        ChangeActiveButton(activeButton);
    }

    private int GetSafeActiveButtonNumber(int _newbuttonNumber)
    {
        activeButton += _newbuttonNumber;
        // THIS HARDWIRED NUMBERS MUST BE CORRECTLY SET
        if (activeButton >= 3)
        {
            activeButton = 0;
        }
        else if (activeButton <= -1)
        {
            activeButton = 2;
        }
        return activeButton;
    }

    private void ChangeActiveButton(int activeButton)
    {
        ChangeParticlePosition(activeButton);
    }

    #endregion
    private void OnEnable()
    {
        
    }
}
