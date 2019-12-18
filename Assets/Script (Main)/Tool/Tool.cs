using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleController))]
public class Tool : MonoBehaviour
{
	public string toolName;

	public ParticleController particleController;
	public Transform initTransform;
	public Vector3 firstTouchOffset;
	public Vector3 toolToTouchOffset;
	public Vector3 affectAreaToToolOffset;

	public Animator anim;

	protected bool isAnimating = false;

	private void Awake()
	{
		particleController = GetComponent<ParticleController>();
	}

	protected virtual void OnEnable()
	{
		transform.position = initTransform.position;
	}

	public bool UpdatePosition(Vector3 touchInputPosition)
	{
		if (!isAnimating) {
			Vector3 newPos = Camera.main.ScreenToWorldPoint(touchInputPosition + toolToTouchOffset);
			transform.position = newPos;
		}

		return !isAnimating;
	}

	public Vector3 GetAffectPosition(Vector3 touchInputPosition)
	{
		//if (!isAnimating)
			return touchInputPosition + toolToTouchOffset + affectAreaToToolOffset;

	}

	public virtual void SetEffectActive(bool isEnable)
	{
		if (!isAnimating) {
			if (isEnable) {
				particleController.PlayParticleEffect();
			} else {
				particleController.StopParticleEffect();
			}
		}
	}

	public virtual void DeactiveTool()
	{
		gameObject.SetActive(false);
	}

	public void DeactiveAnim()
	{
	}

	public void TriggerGoInAnimation()
	{
		StartCoroutine(OnStart());
	}

	public void TriggerGoOutAnimation()
	{
		StartCoroutine(OnEnd());
	}

	public AnimationClip startAnimName;
	public AnimationClip endingAnimName;

	public virtual IEnumerator OnStart()
	{
		if (startAnimName != null) {

			isAnimating = true;

			anim.Play(startAnimName.name);
			yield return new WaitForSeconds(startAnimName.length);

			isAnimating = false;

		}

		yield break;
	}

	public virtual IEnumerator OnEnd()
	{
		if (endingAnimName != null) {

			isAnimating = true;

			anim.Play(endingAnimName.name);
			yield return new WaitForSeconds(endingAnimName.length);

			isAnimating = false;
		}

		yield break;
	}
}
