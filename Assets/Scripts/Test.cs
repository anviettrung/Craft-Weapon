using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public Texture2D brushCircle;
	public Texture2D tex;
	public Vector2Int pos;
	public int radius;
	public Color color;

	public Texture2D texture;
	protected bool isSaving;

	void Start()
	{
		texture = new Texture2D(256, 256, TextureFormat.ARGB32, false);
		GetComponent<Renderer>().material.mainTexture = texture;

		//for (int y = 0; y < 256; y++) {
		//	for (int x = 0; x < 256; x++) {
		//		Color color = ((x & y) != 0 ? Color.white : Color.gray);
		//		texture.SetPixel(x, y, color);
		//	}
		//}

		Color fillColor = Color.black;
		Color[] fillPixels = new Color[texture.width * texture.height];

		for (int i = 0; i < fillPixels.Length; i++) {
			fillPixels[i] = fillColor;
		}

		texture.SetPixels(fillPixels);

		//int r = radius;
		//float rSquared = r * r;
		//for (int u = 0; u < texture.width; u++) {
		//	for (int v = 0; v < texture.height; v++) {
		//		Color color = Color.clear;
		//		if ((pos.x - u) * (pos.x - u) + (pos.y - v) * (pos.y - v) < rSquared) texture.SetPixel(u, v, color);
		//	}
		//}

		for (int u = 0; u < texture.width; u++) {
			for (int v = 0; v < texture.height; v++) {
				Color c = tex.GetPixel(u, v);
				float alpha = c.r;
				c = new Color(0, 0, 0, alpha);
				texture.SetPixel(u, v, c);
			}
		}

		texture.Apply();
	}

	public void Circle(int x, int y, int r, Color color)
	{
		float rSquared = brushCircle.width * brushCircle.height;

		x -= (int)(brushCircle.width / 2);
		y -= (int)(brushCircle.height / 2);

		Debug.Log("x: " + x + " y: " + y);
		for (int u = 0; u < brushCircle.width; u++) {
			for (int v = 0; v < brushCircle.height; v++) {
				Debug.Log(u + " " + v);
				texture.SetPixel(x+u, y+v, GetAlphaBlack(brushCircle.GetPixel(u, v) * texture.GetPixel(x+u, y+v)));
			}
		}

		texture.Apply();
	}

	public Color GetAlphaBlack(Color inp)
	{
		Color c = inp;
		float alpha = c.r;
		return new Color(1, 1, 1, alpha);
	}

	void Update()
	{
		if (!Input.GetMouseButton(0))
			return;

		RaycastHit hit;
		if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			return;

		Renderer renderer = hit.collider.GetComponent<Renderer>();
		MeshCollider meshCollider = hit.collider as MeshCollider;
		if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null || meshCollider == null)
			return;

		Texture2D tex = (Texture2D)renderer.material.mainTexture;
		Vector2 pixelUV = hit.textureCoord;
		Circle((int)(pixelUV.x * texture.width), (int)(pixelUV.y * texture.height), radius, Color.black);
		print((int)(pixelUV.x * texture.width) + "--" + (int)(pixelUV.y * renderer.material.mainTexture.height));
	}
}
