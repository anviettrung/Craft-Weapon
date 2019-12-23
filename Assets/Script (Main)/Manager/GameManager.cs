using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public string curSceneName;
	public string gameplaySceneName;
	public Crafter crafter;

	public void Awake()
	{
		curSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		if (curSceneName == "_preload")
			OnPreload();
	}

	private void Start()
	{
		OnPreload();
	}

	public void OnPreload()
	{
		//ChangeScene(gameplaySceneName);

		// ------------------------------------------------
		// Load Game
		// ------------------------------------------------
		 //LevelManager.Instance.LoadGameData();
		 //UserManager.Instance.LoadGameData();
		// ------------------------------------------------

		LevelManager.Instance.OpenLastestLevel();
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

	private void OnApplicationQuit()
	{
		LevelManager.Instance.SaveGameData();
		UserManager.Instance.SaveGameData();
	}
}
