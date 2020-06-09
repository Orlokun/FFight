using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //CameraDirectorVariables
    CameraDirector cDirector;

    //Player Variables
    PlayerController pCOntroller;
    bool theresMovement;

    // Start is called before the first frame update
    void Start()
    {
        pCOntroller = FindObjectOfType<PlayerController>();
        cDirector = FindObjectOfType<CameraDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region PlayerHorizontalInput
   
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
