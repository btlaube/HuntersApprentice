using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;
    public Animator transition;
    // public CanvasGroupScript canvasGroup;

    [SerializeField] private float transitionTime = 1f;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        
        // DontDestroyOnLoad(gameObject);
    }

    public void ResetScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadSceneAsync(sceneToLoad);

        transition.SetTrigger("End");
    }

    public void LoadNextScene()
    {
        LoadScene(SpawnManager.Instance.SpawnData.sceneName);
    }

    public void LoadScene(int sceneToLoad) {
        StartCoroutine(LoadLevel(sceneToLoad));
    }

    public void LoadScene(string sceneToLoad) {
        StartCoroutine(LoadLevelByString(sceneToLoad));
    }

    IEnumerator LoadLevelByString(string sceneToLoad) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneToLoad);

        transition.SetTrigger("End");
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

        transition.SetTrigger("End");
    }

    public void StartTransition()
    {
        transition.SetTrigger("Start");
    }

    public void EndTransition()
    {
        transition.SetTrigger("End");
    }

    public void ActivateTransition(int transitionIndex)
    {
        StartCoroutine(Transition(transitionIndex));
    }

    IEnumerator Transition(int transitionIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        transition.SetTrigger("End");
    }

    public void Quit() {
        Application.Quit();
    }
}
