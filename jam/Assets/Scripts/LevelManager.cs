using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private string[] levels;

    [SerializeField]
    private string mainSceneName;
    private Scene loadedScene;
    public LevelStateController currentLevel;

    private IEnumerator restart;

    private bool quitting = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        Events.Instance.levelLoaded.AddListener(SetCurrentLevel);
    }

    private void OnDisable()
    {
        if (!quitting)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
            Events.Instance.levelLoaded.RemoveListener(SetCurrentLevel);
        }
    }

    private void OnApplicationQuit()
    {
        quitting = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentLevel)
        {
            if (restart != null) return;
            restart = RestartCoroutine();
            StartCoroutine(restart);
        }
    }

    private IEnumerator RestartCoroutine()
    {
            Events.Instance.playerDied.Invoke(DeathType.Explode);
        yield return new WaitForSeconds(2);
            ReloadLevel(() => UIManager.Instance.FadeIn(null), null);
        restart = null;
    }

    private void SetCurrentLevel(LevelStateController level)
    {
        currentLevel = level;
    }

    public void LoadNext()
    {
        LoadLevel(currentLevel.nextLevelName,
            () => UIManager.Instance.FadeIn(null), null);
    }

    public void LoadMainScene(Action unloadCallback)
    {
        StartCoroutine(UnloadCoroutine(unloadCallback));
    }

    public void LoadLevel(int index, Action loadCallback, Action unloadCallback)
    {
        if (levels.Length <= index)
        {
            Debug.LogError(string.Format("Trying to load {0}. level! There are {1} levels in total!", index, (levels.Length - 1)));
            return;
        }

        LoadLevel(levels[index], loadCallback, unloadCallback);
    }

    public void LoadLevel(string levelName, Action loadCallback, Action unloadCallback)
    {
        if(levelName == mainSceneName)
        {
            LoadMainScene(() => UIManager.Instance.SetPanel(Panel.Credits));
            return;
        }
        StartCoroutine(LoadLevelCoroutine(levelName, loadCallback, unloadCallback));
    }

    public void ReloadLevel(Action loadCallback, Action unloadCallback)
    {
        StartCoroutine(LoadLevelCoroutine(loadedScene.name, loadCallback, unloadCallback));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == mainSceneName) return;
        loadedScene = scene;
        SceneManager.SetActiveScene(loadedScene);
        Debug.Log(string.Format("New scene loaded: {0}", loadedScene.name));
    }

    private void OnSceneUnloaded(Scene scene)
    {
        loadedScene = default;
        Debug.Log(string.Format("Unloaded scene {0}", scene.name));

    }

    private IEnumerator LoadLevelCoroutine(string levelName, Action loadCallback, Action unloadCallback)
    {
        yield return StartCoroutine(UnloadCoroutine(unloadCallback));
        yield return StartCoroutine(LoadCoroutine(levelName, loadCallback));
    }

    private IEnumerator LoadCoroutine(string levelName, Action callback)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        load.allowSceneActivation = true;
        while (!load.isDone)
            yield return null;

        if (callback != null)
            callback.Invoke();
    }

    private IEnumerator UnloadCoroutine(Action callback)
    {
        if (loadedScene != default)
        {
            AsyncOperation unload = SceneManager.UnloadSceneAsync(loadedScene);
            while (!unload.isDone)
                yield return null;
        }

        if (callback != null)
            callback.Invoke();
    }
}
