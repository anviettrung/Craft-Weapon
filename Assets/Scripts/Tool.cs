using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleController))]
public class Tool : MonoBehaviour
{
	public ParticleController particleController;
	public Vector3 initPosition;
	public Vector3 firstTouchOffset;
	public Vector3 toolToTouchOffset;
	public Vector3 affectAreaToToolOffset;

	private void Awake()
	{
		particleController = GetComponent<ParticleController>();
	}

	public void UpdatePosition(Vector3 touchInputPosition)
	{
		Vector3 newPos = Camera.main.ScreenToWorldPoint(touchInputPosition + toolToTouchOffset);
		transform.position = newPos;
	}

	public Vector3 GetAffectPosition(Vector3 touchInputPosition)
	{
		return touchInputPosition + toolToTouchOffset + affectAreaToToolOffset;
	}

	public void SetEffectActive(bool isEnable)
	{
		if (isEnable) {
			particleController.PlayParticleEffect();
		} else {
			particleController.StopParticleEffect();
		}
	}
}
