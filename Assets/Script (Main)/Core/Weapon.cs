using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public string weaponName;
	public Crafter crafter;

	private void Awake()
	{
		crafter = GetComponentInChildren<Crafter>();
	}
}
