using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public string curSceneName;
	public string gameplaySceneName;
	public CraftStateManager crafter;

	public void Awake()
	{
		curSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		if (curSceneName == "_preload")
			OnPreload();
	}

	private void OnLevelWasLoaded(int level)
	{
		curSceneName = UnityEngine.SceneManagement.SceneManager.GetSceneAt(level).name;
	}

	public void OnPreload()
	{
		ChangeScene(gameplaySceneName);
	}

	public void ChangeScene(int x)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(x);
		curSceneName = UnityEngine.SceneManagement.SceneManager.GetSceneAt(x).name;
	}

	public void ChangeScene(string sceneName)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
		curSceneName = sceneName;
	}
}
