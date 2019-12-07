using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBottle : Tool
{
	public GameObject lavaStream;

	public override void SetEffectActive(bool isEnable)
	{
		base.SetEffectActive(isEnable);

		if (isEnable) {
			lavaStream.SetActive(true);
		} else {
			lavaStream.SetActive(false);
		}
	}
}
