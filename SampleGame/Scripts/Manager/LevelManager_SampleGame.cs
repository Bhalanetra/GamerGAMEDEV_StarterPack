
using UnityEngine;


public class LevelManager_SampleGame : LevelManager
{
    #region Variables

    SampleGame_LevelSO levelSO;

    #endregion Variables

    #region Unity Methods

    public override void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        levelSO = LevelsManager.instance.selectedMap.levelConfig as SampleGame_LevelSO;
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public void AddScore(int score)
    {
        levelScore.score += score;
    }

    public void LevelSuccess()
    {
        levelScore.AddScoreOnLevelEnd(levelSO.successReward.rewardScore, levelSO.successReward.rewardCoins, levelSO.successReward.gemAmount);
    }

    public void LevelFail()
    {
        levelScore.AddScoreOnLevelEnd(levelSO.failReward.rewardScore, levelSO.failReward.rewardCoins, levelSO.failReward.gemAmount);
    }

    public void SaveScore()
    {
        GameManager.Instance.AddScores(levelScore, () =>
        {
            SaveManager.SaveModified();
        });
    }

    #endregion Main
}