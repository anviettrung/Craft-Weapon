using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

	public void OnTableIn()
	{
		PlayerInput.Instance.PauseGame();
	}

	public void OnTableFinishedIn()
	{
		PlayerInput.Instance.UnpauseGame();
	}

}
