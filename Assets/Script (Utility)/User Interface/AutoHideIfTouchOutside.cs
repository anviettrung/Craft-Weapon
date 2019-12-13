using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class AutoHideIfTouchOutside : MonoBehaviour
{
	public UnityEvent onHide = new UnityEvent();
	// If user touch on these UI, script will act like user touch on this UI
	public List<GameObject> dontIgnoreUIs;

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

			for (int i = 0; i < dontIgnoreUIs.Count; i++) {
				if (res.gameObject == dontIgnoreUIs[i])
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
