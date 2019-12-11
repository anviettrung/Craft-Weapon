using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
	public string weaponName;
	public Crafter crafter;

	public UnityEvent onFinishedWeapon = new UnityEvent();

	private void Awake()
	{
		crafter = GetComponentInChildren<Crafter>();
		crafter.weaponController = this;
		onFinishedWeapon.AddListener(OnFinishedWeapon);
	}

	void OnFinishedWeapon()
	{
		UIManager.Instance.botUI.gameObject.SetActive(true);
		UIManager.Instance.midUI.gameObject.SetActive(true);

		Debug.Log("WON");
	}
}
