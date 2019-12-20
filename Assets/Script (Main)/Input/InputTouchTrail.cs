using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouchTrail : Singleton<InputTouchTrail>
{

	protected int curFingerId;
	protected bool isTouching;
	protected Vector2 lastPosition;
	protected Vector2 deltaPosition;

	public bool IsTouching()
	{
		return isTouching;
	}

	public Vector2 GetDeltaPosition()
	{
		if (isTouching)
			return deltaPosition;

		return Vector2.zero;
	}

	public Vector2 GetTouchPosition()
	{
		return lastPosition;
	}

	public void Update()
	{
		if (!(Input.touchCount > 0)) {
			Debug.Log("No touch found");
			isTouching = false;
			return;
		}

		Touch touch = Input.GetTouch(0);

		if (isTouching == false) {

			Debug.Log("Setting new finger id: " + touch.fingerId);
			curFingerId = touch.fingerId;
			lastPosition = touch.position;
			deltaPosition = Vector2.zero;

			isTouching = true;

		}


		if (touch.fingerId == curFingerId) {

			Debug.Log("Cal delta position");
			deltaPosition = touch.deltaPosition;
			lastPosition = touch.position;

		} else {

			Debug.Log("Lost that touch");
			isTouching = false;
		}
	}
}
