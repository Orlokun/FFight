using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region PlayerActions
public enum CharacterMovementState
{
    Idle,
    Walking, 
    Running
}
public enum CharacterJumpState
{
    Jumping,
    Grounded,
    Falling
}
#endregion

public class StageDirector : MonoBehaviour
{
    [SerializeField]
    private Vector2[] startPositions;

    // Start is called before the first frame update
    void Awake()
    {

    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check If needs to be triggered
        if (collision.GetComponent<PlayerController>())
        {
            PlayerActivatedEvent();
        }
    }
    
    private void PlayerActivatedEvent()
    {

    }

    private void StartEvent(int eventNumber)
    {
        //First Base on levels
        switch (eventNumber)
        {
            case 1:
                Stage01Event01();
                break;
            default:
                break;
        }
    }

    private void Stage01Event01()
    {

    }
}
