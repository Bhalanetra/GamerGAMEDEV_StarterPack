using UnityEngine;
using GamerGAMEDEV.States;
using GamerGAMEDEV.SceneManagement;
using UnityEngine.SceneManagement;
using System;

public class GameManager : SavableMonoBehaviour<GameState>
{
    #region Variables

    private static GameManager instance;

    public static GameManager Instance => instance;

    [Space, Header("GAME STATE")]
    public GameState gameState = new GameState();

    #endregion Variables

    #region Unity Methods

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SaveManager.Load();
    }

    private void OnEnable()
    {
        Debug.Log(this.gameObject.name);
        SaveManager.Register(this);
    }

    private void OnDisable()
    {
        SaveManager.Unregister(this);
    }

    #endregion Unity Methods

    #region Other
    public void LoadGameplayScene()
    {
        //ScenesManager.LoadScene(Scenes.MapOne);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        Loader.Instance.ShowLoaderWithOperation(operation);
    }
    #endregion Other

    #region Main

    [ContextMenu("Save Game")]
    public void SaveGame()
    {
        SaveManager.SaveModified();
    }

    public void AddScores(LevelScore levelScore, Action OnComplete = null)
    {
        gameState.coin += levelScore.coins;
        gameState.gems += levelScore.gems;

        if(levelScore.score >= gameState.highscore) gameState.highscore = levelScore.score;

        MarkDirty();

        OnComplete?.Invoke();
    }

    #endregion Main

    #region Save/Load

    public override GameState Capture()
    {
        return gameState;
    }

    public override void Restore(GameState state)
    {
        gameState = state;
    }

    #endregion Save/Load
}