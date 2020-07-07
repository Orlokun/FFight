using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelDataManager
{
#region GlobalData&Structs

    public struct LocalPlayerData
    {
        int localId;
        string playerType;
        PlayerController pController;
        CharacterMovementState pActualMovState;
        CharacterJumpState characterJumpState;
    }

    public struct MatchType
    {
        int lPlayersAmount;
        int lCpuAmount;

        int matchLength;
    }

#endregion

}
