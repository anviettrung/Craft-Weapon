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
		Destroy(t.gameObject.GetComponent<FollowObject>());
		PlayerInput.Instance.UnpauseGame();

	}

	public void ToolFollowTable(Tool target)
	{
		t = target;
		t.gameObject.AddComponent<FollowObject>().target = toolGoInPosition;
	}

}
