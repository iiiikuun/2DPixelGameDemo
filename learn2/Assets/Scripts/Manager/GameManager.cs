using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private UI_BlackBackground blackBackground;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void LoadScene(string sceneName)
    {
        blackBackground.FadeOut();
        StartCoroutine(LoadSceneFor(sceneName, 1.5f));
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public IEnumerator LoadSceneFor(string sceneName,float second)
    {
        yield return new WaitForSeconds(second);
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
