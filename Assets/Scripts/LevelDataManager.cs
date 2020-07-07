using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelDataManager
{
    static int numberOfPlayers;
    static int matchDuration;
    static string player1, player2, player3, player4;
    static Dictionary<int, string> playerInputTypes;




    public static void StartSettingControllerType(int playerNumber, string cType, bool defaultSetting)
    {
        if (defaultSetting)
        {
            SetDefaultControllerTypes(playerNumber);
        }
        else
        {
            SetIncomingControllertype(playerNumber, cType);
        }
    }
    private static void SetDefaultControllerTypes(int playerNumber)
    {
        
    }

    private static void SetIncomingControllertype(int playerNumber, string cType)
    {

    }

    public static int GetNumberOfPlayers()
    {
        return numberOfPlayers;
    }
    public static void SetNumberOfPlayers(int pAmount)
    {
        numberOfPlayers = pAmount;
    }

}
