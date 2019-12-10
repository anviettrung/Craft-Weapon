using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public string curSceneName;
	public CraftStateManager crafter;

	public void Awake()
	{
		curSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		if (curSceneName == "_preload")
			OnPreload();
	}

	public void OnPreload()
	{

	}

	public void ChangeScene(int x)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(x);
	}

	public void ChangeScene(string sceneName)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
	}
}
