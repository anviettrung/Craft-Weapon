using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class CraftStateManager : MonoBehaviour
{
	[System.Serializable]
	public class State
	{
		public Material mat;
		public Animator anim;
		public AnimationClip startAnimName;
		public AnimationClip endingAnimName;

		public Tool tool;

		public int totalPix;
		public int initWPix;
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

	public string weaponName;

	// Track component
	[HideInInspector]
	public DrawOnTexture drawer;
	public Animator anim;

	// Init data
	public List<State> states;
	public float targetProcess;

	// Event - Signal
	public UnityEvent OnFinishingLevel = new UnityEvent();

	// In-game process
	[SerializeField]
	protected float process;
	protected int currWpix;
	protected int currentStateIndex = 0;
	public bool isLockInput = false;
	public bool isChangingState = false;
	public bool isWon = false;

	private void Start()
	{
		drawer = GetComponent<DrawOnTexture>();
		for (int i = 0; i < states.Count; i++) {
			states[i].Init();
			states[i].anim = anim;
		}

		StartCoroutine(ChangeState(currentStateIndex, true));
	}

	protected void Update()
	{
		if (isChangingState) {
			if (!isLockInput && Input.GetMouseButtonDown(0))
				isChangingState = false;
		} else if (Input.GetMouseButton(0)) {
			UpdateTool(true);
			drawer.DoAction(states[currentStateIndex].tool.GetAffectPosition(Input.mousePosition));
		} else {
			UpdateTool(false);
		}
	}

	void UpdateTool(bool isOn)
	{
		if (states[currentStateIndex].tool == null)
			return;

		states[currentStateIndex].tool.UpdatePosition(Input.mousePosition);

		if (isOn) {
			states[currentStateIndex].tool.SetEffectActive(true);
		} else {
			states[currentStateIndex].tool.SetEffectActive(false);
		}
	}

	public IEnumerator ChangeState(int nextStateIndex, bool firstChange)
	{
		if (nextStateIndex >= states.Count)
			yield break;

		isLockInput = true;
		isChangingState = true;

		states[currentStateIndex].tool.gameObject.SetActive(false);

		if (!firstChange)
			yield return StartCoroutine(states[currentStateIndex].OnEnd());

		currentStateIndex = nextStateIndex;

		yield return StartCoroutine(states[nextStateIndex].OnStart());

		states[currentStateIndex].tool.gameObject.SetActive(true);

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
		isLockInput = true;

		Color[] texmap = drawer.dynammicMaskTexture.GetPixels();

		currWpix = 0;
		for (int i = 0; i < texmap.Length; i++) {
			if (texmap[i].r > 0)
				currWpix++;
		}

		UpdateStatusText();

		if (process >= targetProcess) {
			StartCoroutine(ChangeState(currentStateIndex+1, false));
		}

		isLockInput = false;
	}

	void UpdateStatusText()
	{
		int x = currentStateIndex;
		process = (float)(currWpix - states[x].initWPix) / (float)(states[x].initBPix);

		UIManager.Instance.SetLabel_Level(currentStateIndex + 1);
		UIManager.Instance.SetLabel_Process(Mathf.RoundToInt(
			Mathf.Clamp(process / targetProcess * 100, 0.0f, 100.0f)));
	}

}
