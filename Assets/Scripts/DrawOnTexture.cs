using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrawOnTexture : MonoBehaviour
{
	public GameObject toolObject;
	public Texture2D brushTexture;
	public float brushScale = 1;
	protected int brushWidth;
	protected int brushHeight;
	public string affectedTextureName = "_MainTex";

	public Vector3 toolAffectOffset;
	public Vector3 toolFirstTouchOffset;
	public Vector3 toolCurrentOffset;
	public Vector2Int drawPosition;
	public UnityEvent OnSavingTexture = new UnityEvent();

	public Renderer renderer;
	protected Texture2D affectedTexture;
	public Texture2D dynammicMaskTexture;
	protected Color[] colors;
	protected Color[] brushPixels;
	protected bool isSaving = false;
	public bool canReceiveInput = true;

	private void Awake()
	{
		renderer = GetComponent<Renderer>();
		Init();
	}

	public void UpdateMask()
	{
		affectedTexture = (Texture2D)renderer.sharedMaterial.GetTexture(affectedTextureName);
		dynammicMaskTexture = Instantiate(affectedTexture) as Texture2D;

		renderer.material.SetTexture(affectedTextureName, dynammicMaskTexture);
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

		drawPosition = new Vector2Int(
			(int)(pixelUV.x * dynammicMaskTexture.width - brushWidth * 0.5f),
			(int)(pixelUV.y * dynammicMaskTexture.height - brushHeight * 0.5f)
		);

		Color[] curTexViewport = dynammicMaskTexture.GetPixels(drawPosition.x, drawPosition.y, brushWidth, brushHeight);
		for (int i = 0; i < brushPixels.Length; i++) {
			colors[i] = brushPixels[i] + curTexViewport[i];
		}

		isSaving = true;
		Invoke("SaveTexture", 0.05f);
	}

	//Sets the base material with a our canvas texture, then removes all our brushes
	void SaveTexture()
	{
		dynammicMaskTexture.SetPixels(drawPosition.x, drawPosition.y, brushWidth, brushHeight, colors);
		dynammicMaskTexture.Apply();
		OnSavingTexture.Invoke();
		isSaving = false;
	}
}
