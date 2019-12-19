using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableDestroyer : MonoBehaviour
{
	public Explodable explodable;
	public float delayDestroyTime;
	public List<GameObject> frags;

	public Material cloneMat;

	private void Awake()
	{
		cloneMat = new Material(explodable.GetComponent<Renderer>().material);
		explodable.GetComponent<Renderer>().material = cloneMat;
		explodable.fragmentInEditor();
	}

	private void Start()
	{
		frags = new List<GameObject>(explodable.fragments.Count);
		foreach (GameObject frag in explodable.fragments) {
			frags.Add(frag);
		}
	}

	protected IEnumerator DestroyFragmentAfterTime(float t)
	{
		float elapsed = 0;
		Renderer rend = frags[0].GetComponent<Renderer>();
		while (elapsed < t) {
			elapsed += Time.deltaTime;
			rend.sharedMaterial.SetFloat("_Transparency", (t-elapsed) / t);
			yield return new WaitForEndOfFrame();
		}

		gameObject.SetActive(false);
	}

	public void Explosion()
	{
		explodable.explode();
		ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
		ef.doExplosion(transform.position);

		foreach (GameObject frag in frags) {
			frag.transform.SetParent(transform);
		}

		StartCoroutine(DestroyFragmentAfterTime(delayDestroyTime));
	}

}
