using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleController))]
public class Tool : MonoBehaviour
{
	public string toolName;

	public Transform initTransform;
	public Vector3 affectAreaToToolOffset;

	public bool hasRestrictiveBound;

	public ParticleController particleController;
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


	protected Vector3 ClampPosition(Vector3 thisPosition)
	{

		Vector3 viewPos = Camera.main.WorldToViewportPoint(thisPosition);

		viewPos.x = Mathf.Clamp01(viewPos.x);
		viewPos.y = Mathf.Clamp01(viewPos.y);

		return Camera.main.ViewportToWorldPoint(viewPos);
	}

	public bool UpdatePosition(Vector3 deltaPosition)
	{
		if (!isAnimating) {
			//Vector3 newPos = Camera.main.ScreenToWorldPoint(deltaPosition + toolToTouchOffset);
			//transform.position = newPos;
			Vector3 newPos = transform.position + deltaPosition;
			transform.position = newPos;

			if (hasRestrictiveBound)
				transform.position = ClampPosition(transform.position);
		}

		return !isAnimating;
	}


	public Vector3 GetAffectPosition()
	{
		Vector3 affectPosition = Camera.main.WorldToScreenPoint(transform.position);
		return affectPosition + affectAreaToToolOffset;
	}

	public virtual void SetEffectActive(bool isEnable)
	{
		if (!isAnimating) {

			if (isEnable) {
				particleController.PlayParticleEffect();
				PlayerInput.Instance.Vibrate();
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
