using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
	public ParticleSystem[] particleSystem;

	public void PlayParticleEffect()
	{
		for (int i=0; i < particleSystem.Length; i++)
			particleSystem[i].Play();
	}

	public void StopParticleEffect()
	{
		for (int i = 0; i < particleSystem.Length; i++)
			particleSystem[i].Stop(false, ParticleSystemStopBehavior.StopEmitting);
	}

}
