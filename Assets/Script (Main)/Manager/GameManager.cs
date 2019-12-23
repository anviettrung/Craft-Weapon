using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public string curSceneName;
	public string gameplaySceneName;
	public GameObject splashScreen;
	public float splashScreenTime;

	public void Awake()
	{
		curSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		if (curSceneName == "_preload")
			OnPreload();
	}

	private void Start()
	{
		StartCoroutine(OnPreload());
	}

	public IEnumerator OnPreload()
	{
		//ChangeScene(gameplaySceneName);

		// ------------------------------------------------
		// Load Game
		// ------------------------------------------------
		//LevelManager.Instance.LoadGameData();
		//UserManager.Instance.LoadGameData();
		// ------------------------------------------------

		UIManager.Instance.mainCanvas.gameObject.SetActive(false);

		yield return StartCoroutine(SplashScreen(splashScreenTime));

		UIManager.Instance.mainCanvas.gameObject.SetActive(true);

		LevelManager.Instance.OpenLastestLevel();
	}

	public IEnumerator SplashScreen(float t)
	{
		splashScreen.SetActive(true);
		yield return new WaitForSecondsRealtime(t);
		splashScreen.GetComponent<Animator>().SetTrigger("fade out");
		yield return new WaitForSecondsRealtime(1.0f);
		splashScreen.SetActive(false);
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
