using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelDataManager
{
    #region GlobalData&Structs

    private struct LocalPlayerData
    {
        int localId;
        string playerType;
        int healthAmount;
        int powerAmount;

        public LocalPlayerData(int _localId, string _playerType, int _healthAmount, int _powerAmount)
        {
            localId = _localId;
            playerType = _playerType;
            healthAmount = _healthAmount;
            powerAmount = _powerAmount;
        }
    }


    private struct MatchType
    {
        int lPlayersAmount;
        int lCpuAmount;
        int matchLength;

        public MatchType(int _lPlayersAmount, int _lCpuAmount, int _matchLength)
        {
            lPlayersAmount = _lPlayersAmount;
            lCpuAmount = _lCpuAmount;
            matchLength = _matchLength;
        }
    }

    private static MatchType actualMatchType;

    #region Getters&Setters

    public static void SetDefaultMatchType()
    {
        int defaultPlayers = 1;
        int defaultCpuAmount = 1;
        int defaultMatchLength = 45;

        actualMatchType = new MatchType(defaultPlayers, defaultCpuAmount, defaultMatchLength);
    }

    #endregion
    #endregion
}

