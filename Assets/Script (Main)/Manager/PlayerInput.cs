using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
	public Crafter crafter;

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

		crafter.UpdateTouch(
			Input.mousePosition,
			Input.GetMouseButtonDown(0),
			Input.GetMouseButton(0)
		);
	}

	public void Vibrate()
	{
		if (isVibration)
			Handheld.Vibrate();
	}
}
