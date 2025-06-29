using System;
using GamerGAMEDEV.SampleGame;
using UnityEngine;

[CreateAssetMenu(fileName = "Level 1", menuName = "Sample Game/New Level")]
public class SampleGame_LevelSO : LevelConfigBaseSO

{
    [Space, Header("Success Reward")]
    public LevelReward successReward;
    [Space, Header("Fail Reward")]
    public LevelReward failReward;
}

namespace GamerGAMEDEV
{
    namespace SampleGame
    {
        [Serializable]
        public class LevelReward
        {
            public int rewardScore;
            public int rewardCoins;
            public int gemAmount;
        }
    }
}