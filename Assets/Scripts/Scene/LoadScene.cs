using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void SceneSwitchGame(string OGScene)
    {
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(OGScene);
    }
    public void SceneSwitchMenu(string OGScene)
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(OGScene);
    }
    public void SceneSwitchTrophies1(string OGScene)
    {
        SceneManager.LoadScene("Trophycase1", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(OGScene);
    }
    public void SceneSwitchTrophies2(string OGScene)
    {
        SceneManager.LoadScene("Trophycase2", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(OGScene);
    }
    public void SceneSwitchExtras(string OGScene)
    {
        SceneManager.LoadScene("ExtrasMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(OGScene);
    }
    public void SceneSwitchDeath(string OGScene)
    {
        SceneManager.LoadScene("DeathScreen", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(OGScene);
    }
    public void SceneSwitchCutscene(string OGScene)
    {
        SceneManager.LoadScene("Cutscene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(OGScene);
    }
}
