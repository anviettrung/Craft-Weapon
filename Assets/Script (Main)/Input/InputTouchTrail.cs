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
			isTouching = false;
			return;
		}

		Touch touch = Input.GetTouch(0);

		if (isTouching == false) {
		
			curFingerId = touch.fingerId;
			lastPosition = touch.position;
			deltaPosition = Vector2.zero;

			isTouching = true;

		}


		if (touch.fingerId == curFingerId) {

			deltaPosition = touch.deltaPosition;
			lastPosition = touch.position;

		} else {
		
			isTouching = false;
		}
	}
}
