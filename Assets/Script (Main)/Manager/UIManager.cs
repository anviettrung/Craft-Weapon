using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
	public Canvas mainCanvas;
	public RectTransform topUI;
	public RectTransform midUI;
	public RectTransform botUI;

	// Top UI
	public Text levelLabel;
	public Text processLabel;
	public Text coinLabel;
	public Button settingButton;

	// Mid UI
	public RectTransform shopUI;
	public RectTransform shopItemHolder;
	public Button completedCellPrefab;
	public Button lockCellPrefab;

	// Bot UI
	public Text rewardCoinLabel;

	//----------------------------------------------------------
	// Top UI function
	//----------------------------------------------------------
	public void SetLabel_Level(int x)
	{
		levelLabel.text = "Level " + x.ToString();
	}

	public void SetLabel_Process(int x)
	{
		processLabel.text = x.ToString() + "%";
	}

	public void SetLabel_Coin(int x)
	{
		coinLabel.text = x.ToString();
	}

	//----------------------------------------------------------
	// Mid UI function
	//----------------------------------------------------------
	public void OpenShop()
	{
		LevelManager.Instance.currentWeapon.gameObject.SetActive(false);
		ToolManager.Instance.GetActiveTool().gameObject.SetActive(false);

		botUI.gameObject.SetActive(false);
		midUI.gameObject.SetActive(false);

		shopUI.gameObject.SetActive(true);
	}

	public void CloseShop()
	{
		shopUI.gameObject.SetActive(false);

		LevelManager.Instance.currentWeapon.gameObject.SetActive(true);

		botUI.gameObject.SetActive(true);
		midUI.gameObject.SetActive(true);
	}


	public Button CreateLockItemInShop(string itemName, int price)
	{
		return null;

	}

	public Button CreateUnfinishedItemInShop(string itemName, Sprite itemIcon)
	{
		return null;
	}

	public Button CreateFinishedItemInShop(string itemName, Sprite itemIcon)
	{
		return null;
	}

	//----------------------------------------------------------
	// 
	//----------------------------------------------------------
	public void SetLabel_RewardCoin(int x)
	{
		rewardCoinLabel.text = x.ToString();
	}

}
