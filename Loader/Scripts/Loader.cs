using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Loader : MonoBehaviour
{
    #region Variables
    public static Loader Instance;
    [Header("AWAKE SETTING")]
    public bool loadOnAwake = false;
    public Sprite _bg;

    [Space, Header("UI")]
    [SerializeField] TextMeshProUGUI tipText;
    [SerializeField] Image bg;
    [SerializeField] TextMeshProUGUI tapText;
    [SerializeField] Button tapButton;
    [SerializeField] Slider loadingBar;

    [Space, Header("CONTENT")]
    [SerializeField] GameObject screen;
    public LoaderContentSO[] content;

    public Action OnShowLoader;

    public float lerpSpeed = 0.5f; // Adjust for how fast the slider fills
    private AsyncOperation asyncOperation;
    private bool allowSceneActivation = false;
    private bool loadingComplete = false;

    #endregion Variables

    #region Unity Methods

    private void Awake()
    {
        if(Instance == null) Instance = this;

        if (loadOnAwake)
        {
            bg.sprite = _bg;
            tipText.text = "Random Tip";
        }
    }

    private void Start()
    {
        ResetScreen();
    }

    private void OnEnable()
    {
        OnShowLoader += ShowLoader;
    }

    private void OnDisable()
    {
        OnShowLoader -= ShowLoader;
    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    public void ShowLoader()
    {
        SelectRandomContent();
        screen.SetActive(true);
    }

    public void ShowLoaderWithOperation(AsyncOperation operation)
    {
        ShowLoader();
        StartCoroutine(LoadSceneAsyncSmooth(operation));
    }

    void OnLoaderCompleted()
    {
        loadingBar.gameObject.SetActive(false);
        tapText.gameObject.SetActive(true);
        tapText.text = "TAP TO CONTINUE";
        tapButton.interactable = true;

        tapButton.onClick.AddListener(HideLoader);
    }

    public void HideLoader()
    {
        screen.SetActive(false);
        ResetScreen();

        if (asyncOperation != null)
        {
            asyncOperation.allowSceneActivation = true;
            Debug.Log("Scene activation allowed");
        }
        else
        {
            Debug.LogWarning("asyncOperation is null!");
        }
    }

    void ResetScreen()
    {
        tapText.gameObject.SetActive(false);
        tapButton.interactable = false;
        loadingBar.gameObject.SetActive(true);
    }

    void SelectRandomContent()
    {
        int index = UnityEngine.Random.Range(0, content.Length);

        tipText.text = content[index].tip;
        bg.sprite = content[index].BG;
    }

    IEnumerator LoadSceneAsyncSmooth(AsyncOperation operation)
    {
        asyncOperation = operation;
        asyncOperation.allowSceneActivation = false;

        float displayedProgress = 0f;

        while (!asyncOperation.isDone)
        {
            // asyncOperation.progress ranges from 0 to 0.9. It reaches 0.9 when the scene is ready.
            float targetProgress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            // Smoothly move displayed value towards target value
            displayedProgress = Mathf.MoveTowards(displayedProgress, targetProgress, Time.deltaTime * lerpSpeed);
            loadingBar.value = displayedProgress;

            // When the scene is fully loaded and slider is also full
            if (displayedProgress >= 1f && asyncOperation.progress >= 0.9f && !loadingComplete)
            {
                loadingComplete = true;
                OnLoaderCompleted();
            }

            yield return null;
        }
    }

    #endregion Main
}