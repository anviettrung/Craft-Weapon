using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : Singleton<WeaponPool>
{
	public Weapon[] weaponPool;

	public Weapon GetWeapon(string name)
	{
		foreach (Weapon wp in weaponPool) {
			if (wp.weaponName == name)
				return wp;
		}

		return null;
	}
}
