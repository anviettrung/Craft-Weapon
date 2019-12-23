using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBottle : Tool
{
	public GameObject lavaStream;
	public Animator bottleAnim;

	public void OnDisable()
	{
		DisableLavaStream();
	}

	public void DisableLavaStream()
	{
		//lavaStream.SetActive(false);
		bottleAnim.SetBool("IsPouring", false);
	}


	public override void SetEffectActive(bool isEnable)
	{
		base.SetEffectActive(isEnable);

		if (isEnable) {
			//lavaStream.SetActive(true);
			bottleAnim.SetBool("IsPouring", true);
		} else {
			//lavaStream.SetActive(false);
			bottleAnim.SetBool("IsPouring", false);
		}
	}
}
