using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	public ShopItemCell cellPrefab;
	List<ShopItemCell> items = new List<ShopItemCell>();

	public void UpdateShop(List<LevelData> datas)
	{
		if (datas.Count < items.Count) {
			int delta = items.Count - datas.Count;
			for (int i = 0; i < delta; i++)
				Destroy(items[i].gameObject);

			items.RemoveRange(0, items.Count - datas.Count);
		}

		if (datas.Count > items.Count) {
			int delta = datas.Count - items.Count;
			for (int i = 0; i < delta; i++) {
				ShopItemCell clone = Instantiate(cellPrefab.gameObject).GetComponent<ShopItemCell>();
				clone.transform.SetParent(this.transform);
				items.Add(clone);
			}
		}

		for (int i = 0; i < items.Count; i ++) {
			items[i].UpdateUI(datas[i]);
		}
	}
}
