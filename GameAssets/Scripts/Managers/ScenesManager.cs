using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamerGAMEDEV.SceneManagement
{
    public static class ScenesManager
    {
        public static void LoadScene(Scenes scenes)
        {
            SceneManager.LoadSceneAsync(scenes.ToString());
        }

        public static AsyncOperation LoadSceneAyncOperation(Scenes scenes)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(scenes.ToString());
            return operation;
        }
    }

    public enum Scenes
    {
        MenuScene,
        GameplayScene,
        MapTwo
    }
}
