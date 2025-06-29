using GamerGAMEDEV.SceneManagement;
using TMPro;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [Header("ON AWAKE")]
    public bool loadOnAwake = false;

    [Space]
    public Scenes scene;
    public TextMeshProUGUI sceneNameText;

    private void Start()
    {
        if(sceneNameText != null) sceneNameText.text = scene.ToString();

        if(loadOnAwake) LoadScene();
    }

    public void LoadScene()
    {
        Loader loader = Loader.Instance;
        if(loader != null)
        {
            loader.ShowLoaderWithOperation(ScenesManager.LoadSceneAyncOperation(scene));
        }
        else
        {
            ScenesManager.LoadScene(scene);
        }
    }
}
