using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableDestroyer : MonoBehaviour
{
	public Explodable explodable;
	public float delayDestroyTime;
	public List<GameObject> frags;

	private void Awake()
	{
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
		yield return new WaitForSeconds(t);
 		Destroy(gameObject);
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
