using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	public ShopItemCell cellPrefab;
	List<ShopItemCell> items = new List<ShopItemCell>();

	private void Awake()
	{
		ShopItemCell[] cells = GetComponentsInChildren<ShopItemCell>();
		for (int i = 0; i < cells.Length; i++)
			items.Add(cells[i]);
		
	}

	public void UpdateShop(List<LevelData> datas)
	{
		//if (datas.Count < items.Count) {
		//	int delta = items.Count - datas.Count;
		//	for (int i = 0; i < delta; i++)
		//		Destroy(items[i].gameObject);

		//	items.RemoveRange(0, items.Count - datas.Count);
		//}

		//if (datas.Count > items.Count) {
		//	int delta = datas.Count - items.Count;
		//	for (int i = 0; i < delta; i++) {
		//		ShopItemCell clone = Instantiate(cellPrefab.gameObject).GetComponent<ShopItemCell>();
		//		clone.transform.SetParent(this.transform);
		//		items.Add(clone);
		//	}
		//}

		for (int i = 0; i < items.Count; i ++) {
			if (i < datas.Count) {
				items[i].UpdateUI(datas[i]);
				items[i].gameObject.SetActive(true);
			} else
				items[i].gameObject.SetActive(false);
		}
	}
}
