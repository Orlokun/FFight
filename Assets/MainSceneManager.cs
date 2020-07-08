using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttons;
    private int activeButton;


    private void Awake()
    {
        SetDefaultParameters();   
    }

    private void SetDefaultParameters()
    {
        activeButton = 2;
    }

    public void ChangeActiveButton(InputAction.CallbackContext context)
    {
        Vector2 incomingJoystick = context.ReadValue<Vector2>();
        {
            if (incomingJoystick.y != 0 && incomingJoystick.y >0)
            {
                ChangeActiveButton(1);
            }
            else if (incomingJoystick.y !=0 && incomingJoystick.y < 0)
            {
                ChangeActiveButton(-1);
            }
        }
    }

    private void ChangeActiveButton(int incomingNewButton)
    {

    }

    private void OnEnable()
    {
        
    }

}
