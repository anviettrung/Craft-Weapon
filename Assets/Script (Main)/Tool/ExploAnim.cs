using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploAnim : MonoBehaviour
{
	ExplodeOnClick exploder;

	private void Awake()
	{
		exploder = GetComponentInChildren<ExplodeOnClick>();
	}

	public void EXPLOSION()
	{
		exploder.EXPLOSION();
	}
}
