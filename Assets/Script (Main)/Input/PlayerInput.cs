using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
	[HideInInspector]
	public Crafter crafter;
	public float sensity = 1;

	protected bool isPauseGame;
	public bool isVibration;

	public void PauseGame()
	{
		isPauseGame = true;
	}

	public void UnpauseGame()
	{
		isPauseGame = false;
	}

	protected void Update()
	{
		if (isPauseGame)
			return;

		if (crafter == null)
			return;

		if (InputTouchTrail.Instance.IsTouching() && ToolManager.Instance.GetActiveTool() != null) {

			crafter.UpdateTouch(
				InputTouchTrail.Instance.GetDeltaPosition() * sensity,
				Input.GetMouseButtonDown(0),
				Input.GetMouseButton(0)
			);
		}
	}

	public void Vibrate()
	{
		if (isVibration)
			Handheld.Vibrate();
	}
}
