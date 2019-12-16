using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
	public GameObject target;
	

	private void Update()
	{
		// Only follow x axis
		if (target != null)
			transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
	}
}
