using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class AutoHideIfTouchOutside : MonoBehaviour
{
	public UnityEvent onHide = new UnityEvent();

	private bool IsPointerOverUIObject()
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

		foreach (RaycastResult res in results) {
			if (res.gameObject == gameObject) {
				return true;
			}
		}

		return false;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
			if (!IsPointerOverUIObject()) {
				onHide.Invoke();
				gameObject.SetActive(false);
			}
	}
}
