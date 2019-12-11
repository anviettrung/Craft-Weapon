﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class Crafter : MonoBehaviour
{
	// Track component
	[HideInInspector]
	public DrawOnTexture drawer;
	public Animator anim;
	public Weapon weaponController;


	// Init data
	public List<State> states;
	public float targetProcess = 0.95f;

	// Event - Signal
	[HideInInspector]
	public UnityEvent OnFinishingLevel = new UnityEvent();

	// In-game process
	[SerializeField]
	protected float process;
	protected int currWpix;
	protected int curStateID = 0;
	public bool isLockInput = false;
	public bool isChangingState = false;
	public bool isSaving = false;

	private void Start()
	{
		drawer = GetComponent<DrawOnTexture>();
		for (int i = 0; i < states.Count; i++) {
			states[i].Init();
			states[i].anim = anim;
		}

		StartCoroutine(ChangeState(curStateID, true));
	}

	protected void Update()
	{
		if (isSaving)
			return;

		if (isChangingState) {
			if (!isLockInput && Input.GetMouseButtonDown(0))
				isChangingState = false;
		} else if (!isLockInput &&  Input.GetMouseButton(0)) {
			UpdateTool(true);
			drawer.DoAction(ToolManager.Instance.GetActiveTool().GetAffectPosition(Input.mousePosition));
		} else {
			UpdateTool(false);
		}
	}

	void UpdateTool(bool isOn)
	{
		if (ToolManager.Instance.GetActiveTool() == null)
			return;

		ToolManager.Instance.GetActiveTool().UpdatePosition(Input.mousePosition);
		ToolManager.Instance.GetActiveTool().SetEffectActive(isOn);
	}

	public IEnumerator ChangeState(int nextStateIndex, bool firstChange)
	{
		if (nextStateIndex >= states.Count) {
			weaponController.onFinishedWeapon.Invoke();
			weaponController.onFinishedWeapon.RemoveAllListeners();

			isLockInput = true;
			isChangingState = true;

			ToolManager.Instance.GetActiveTool().gameObject.SetActive(false);

			yield break;
		}

		isLockInput = true;
		isChangingState = true;

		if (!firstChange)
			yield return StartCoroutine(states[curStateID].OnEnd());

		curStateID = nextStateIndex;

		yield return StartCoroutine(states[nextStateIndex].OnStart());

		ToolManager.Instance.ChangeTool(states[nextStateIndex].toolName);

		GetComponent<Renderer>().material = states[nextStateIndex].mat;

		drawer.UpdateMask();
		drawer.OnSavingTexture.RemoveListener(OnSavingTexture);
		drawer.OnSavingTexture.AddListener(OnSavingTexture);

		currWpix = states[nextStateIndex].initWPix;

		UpdateStatusText();

		isLockInput = false;
	}

	void OnSavingTexture()
	{
		isSaving = true;

		Color[] texmap = drawer.dynammicMaskTexture.GetPixels();

		currWpix = 0;
		for (int i = 0; i < texmap.Length; i++) {
			if (texmap[i].r > 0)
				currWpix++;
		}

		UpdateStatusText();

		if (process >= targetProcess) {
			StartCoroutine(ChangeState(curStateID+1, false));
		}

		isSaving = false;
	}

	void UpdateStatusText()
	{
		int x = curStateID;
		process = (float)(currWpix - states[x].initWPix) / (float)(states[x].initBPix);

		UIManager.Instance.SetLabel_Level(curStateID + 1);
		UIManager.Instance.SetLabel_Process(Mathf.RoundToInt(
			Mathf.Clamp(process / targetProcess * 100, 0.0f, 100.0f)));
	}

}

[System.Serializable]
public class State
{
	public Material mat;
	public Animator anim;
	public AnimationClip startAnimName;
	public AnimationClip endingAnimName;

	public string toolName;

	[HideInInspector]
	public int totalPix;
	[HideInInspector]
	public int initWPix;
	[HideInInspector]
	public int initBPix;

	public void Init()
	{
		Texture2D tex = mat.GetTexture("_MaskTex") as Texture2D;
		Color[] texmap = tex.GetPixels();
		totalPix = texmap.Length;
		initWPix = 0;
		for (int i = 0; i < totalPix; i++) {
			if (texmap[i].r > 0)
				initWPix++;
		}
		initBPix = totalPix - initWPix;
	}

	public virtual IEnumerator OnStart()
	{
		if (startAnimName != null) {
			anim.enabled = true;
			anim.Play(startAnimName.name);
			yield return new WaitForSeconds(startAnimName.length);
			anim.enabled = false;
		}

		yield break;
	}

	public virtual IEnumerator OnEnd()
	{
		if (endingAnimName != null) {
			anim.enabled = true;
			anim.Play(endingAnimName.name);
			yield return new WaitForSeconds(endingAnimName.length);
			anim.enabled = false;
		}

		yield break;
	}
}