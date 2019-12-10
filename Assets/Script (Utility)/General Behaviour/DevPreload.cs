using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this should run absolutely first; use script-execution-order to do so.
// (of course, normally never use the script-execution-order feature,
// this is an unusual case, just for development.)
public class DevPreload : MonoBehaviour
{
	void Awake()
	{
		GameObject check = GameObject.Find("__app");
		Debug.Log(check);
		if (check == null) { 
			UnityEngine.SceneManagement.SceneManager.LoadScene("_preload"); 
		}
	}
}
