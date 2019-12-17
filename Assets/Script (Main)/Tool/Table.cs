using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
	public GameObject toolGoInPosition;
	protected Tool t;

	public void OnTableIn()
	{
		PlayerInput.Instance.PauseGame();
	}

	public void OnTableFinishedIn()
	{
		//t.followScript.target = null;
		PlayerInput.Instance.UnpauseGame();

	}

	public void ToolFollowTable(Tool target)
	{
		t = target;
		//t.followScript.target = toolGoInPosition;
	}

}
