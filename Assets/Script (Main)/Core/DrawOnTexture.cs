using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrawOnTexture : MonoBehaviour
{
	// Brush Info
	public Texture2D brushTexture;
	public float brushScale = 1;

	protected int brushWidth;  // Get from brush texture
	protected int brushHeight; // Get from brush texture

	protected Color[] brushPixels;

	// Affected Texture
	public Texture2D dynammicMaskTexture;

	public string affectedTextureName = "_MainTex";
	protected Texture2D affectedTexture;

	protected Color[] colors;

	// Event - Signal
	public UnityEvent OnSavingTexture = new UnityEvent();


	// Control script flow
	public Vector2Int drawBoxUpperLeft;
	public Vector2Int drawBoxResize;
	public bool isSaving = false;

	// Ref
	protected Renderer renderer;

	private void Awake()
	{
		renderer = GetComponent<Renderer>();
		Init();
	}

	public void Init()
	{
		UpdateMask();

		brushWidth = (int)(brushTexture.width * brushScale);
		brushHeight = (int)(brushTexture.height * brushScale);

		colors = new Color[brushHeight * brushWidth];
		brushPixels = new Color[brushHeight * brushWidth];
		for (int y = 0; y < brushHeight; y++) {
			for (int x = 0; x < brushWidth; x++) {
				brushPixels[y * brushWidth + x] = brushTexture.GetPixel(x, y);
			}
		}
	}

	public void UpdateMask()
	{
		affectedTexture = (Texture2D)renderer.sharedMaterial.GetTexture(affectedTextureName);
		dynammicMaskTexture = Instantiate(affectedTexture) as Texture2D;

		renderer.material.SetTexture(affectedTextureName, dynammicMaskTexture);
	}

	public void DoAction(Vector2 targetPosition)
	{
		if (isSaving)
			return;


		RaycastHit hit;
		if (!Physics.Raycast(Camera.main.ScreenPointToRay(targetPosition), out hit))
			return;

		Renderer rend = hit.collider.GetComponent<Renderer>();
		MeshCollider meshCollider = hit.collider as MeshCollider;
		if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
			return;

		Vector2 pixelUV = hit.textureCoord;
		int startX = (int)(pixelUV.x * dynammicMaskTexture.width - brushWidth * 0.5f);
		int startY = (int)(pixelUV.y * dynammicMaskTexture.height - brushHeight * 0.5f);
		int brWidthResize = brushWidth;
		int brHeightResize = brushHeight;

		if (startX < 0) {
			brWidthResize += startX;
			startX = 0; 
		}

		if (startY < 0) {
			brHeightResize += startY;
			startY = 0;
		}

		if (startX + brWidthResize > dynammicMaskTexture.width) {
			brWidthResize = dynammicMaskTexture.width - startX;
		}

		if (startY + brHeightResize > dynammicMaskTexture.height) {
			brHeightResize = dynammicMaskTexture.height - startY;
		}

		drawBoxUpperLeft = new Vector2Int(
			startX,
			startY
		);

		drawBoxResize = new Vector2Int(
			brWidthResize,
			brHeightResize
		);

		

		Color[] curTexViewport = dynammicMaskTexture.GetPixels(
			drawBoxUpperLeft.x, drawBoxUpperLeft.y, 
			drawBoxResize.x, drawBoxResize.y
		);

		for (int i = 0; i < curTexViewport.Length; i++) {
			colors[i] = brushPixels[i] + curTexViewport[i];
		}



		isSaving = true;
		Invoke("SaveTexture", 0.02f);
	}

	//Sets the base material with a our canvas texture, then removes all our brushes
	void SaveTexture()
	{
		dynammicMaskTexture.SetPixels(
			drawBoxUpperLeft.x, drawBoxUpperLeft.y,
			drawBoxResize.x, drawBoxResize.y, 
			colors
		);

		dynammicMaskTexture.Apply();
		OnSavingTexture.Invoke();
		isSaving = false;
	}
}
