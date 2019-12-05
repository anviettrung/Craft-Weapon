using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour
{
	private void Start()
	{
		SetToPixelPerfect();
	}

	public void SetToPixelPerfect()
	{
		Texture texture = GetComponent<Renderer>().material.mainTexture;
		float scale = ((float)Screen.height / 2.0f) / Camera.main.orthographicSize;
		transform.localScale = new Vector3(texture.width / scale, texture.height / scale, 1);
	}
}
