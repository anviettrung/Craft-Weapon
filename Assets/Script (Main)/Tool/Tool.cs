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

	public GameObject toolDragger;

	public FollowObject followScript;
	public Animator anim;

	private void Awake()
	{
		particleController = GetComponent<ParticleController>();
	}

	protected void OnEnable()
	{
		transform.position = initTransform.position;
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

	public virtual void SetEffectActive(bool isEnable)
	{
		if (isEnable) {
			particleController.PlayParticleEffect();
		} else {
			particleController.StopParticleEffect();
		}
	}

	//public void Follow()
	//{
	//	followScript.target = toolDragger;
	//}

	//public void ReleaseFollowing()
	//{
	//	followScript.target = null;
	//}

	public void DeactiveTool()
	{
		gameObject.SetActive(false);
	}

	public void DeactiveAnim()
	{
	}

	public void TriggerGoInAnimation()
	{
		anim.SetTrigger("Go In");
	}

	public void TriggerGoOutAnimation()
	{

		anim.SetTrigger("Go Out");

	}
}
