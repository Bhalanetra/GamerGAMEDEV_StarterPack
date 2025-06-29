using System;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    #region Variables
    public static LevelManager instance;

    public LevelConfigBaseSO levelConfig;

    public LevelScore levelScore;

    #endregion Variables

    #region Unity Methods

    public virtual void Awake()
    {
        if (instance == null) instance = this;
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main
    #endregion Main
}

[Serializable]
public class LevelScore
{
    public int score;
    public int coins;
    public int gems;

    public void AddScoreOnLevelEnd(int score, int coins, int gems)
    {
        this.score += score;
        this.coins += coins;
        this.gems += gems;
    }
}