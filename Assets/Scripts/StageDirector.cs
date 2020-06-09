using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region GameActions

public enum CharacterMovementState
{
    Idle,
    Walking, 
    Running
}
#endregion


public class StageDirector : MonoBehaviour
{
    int eventNumber = 0;

    private List<NPCController> npcs;
    private Dictionary<int, bool> eventsPlayed;
     
    // Start is called before the first frame update
    void Awake()
    {
        //Do Player Management
        //Do NPC Management
        npcs = new List<NPCController>();
        NPCController[] npcControllers = FindObjectsOfType<NPCController>();
        foreach (NPCController npcController in npcControllers)
        {
            npcs.Add(npcController);
        }
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
        eventNumber++;
        StartEvent(eventNumber);
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
