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

	// Setting UI
	public RectTransform settingPanel;
	protected bool isOpenSettingPanel = false;

	// Shop UI
	public RectTransform shopUI;
	public Shop shop;
	public Button completedCellPrefab;
	public Button lockCellPrefab;

	// Bot UI
	public Text rewardCoinLabel;

	//----------------------------------------------------------
	// General Settings
	//----------------------------------------------------------
	private void Awake()
	{
		isOpenSettingPanel = settingPanel.gameObject.active;
	}

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

	public void SwitchSettingUIPanelActive()
	{
		isOpenSettingPanel = !isOpenSettingPanel;
		if (isOpenSettingPanel)
			OpenSettingUI();
		else
			CloseSettingUI();
	}

	public void OpenSettingUI()
	{
		isOpenSettingPanel = true;
		settingPanel.gameObject.SetActive(true);
		PlayerInput.Instance.PauseGame();
	}

	public void CloseSettingUI()
	{
		isOpenSettingPanel = false;
		settingPanel.gameObject.SetActive(false);
		PlayerInput.Instance.UnpauseGame();
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

		shop.UpdateShop(LevelManager.Instance.levelDatas);

		shopUI.gameObject.SetActive(true);
	}

	public void CloseShop()
	{
		shopUI.gameObject.SetActive(false);

		LevelManager.Instance.currentWeapon.gameObject.SetActive(true);

		botUI.gameObject.SetActive(true);
		midUI.gameObject.SetActive(true);
	}


	//----------------------------------------------------------
	// Bot UI functions
	//----------------------------------------------------------
	public void SetLabel_RewardCoin(int x)
	{
		rewardCoinLabel.text = x.ToString();
	}

	public void WatchADs()
	{
		Debug.Log("Watch Ads");
	}


	//----------------------------------------------------------
	// Setting UI functions
	//----------------------------------------------------------
	public void RemoveADs()
	{
		Debug.Log("Remove ADs function");
	}

	public void RestorePurchase()
	{
		Debug.Log("Restore Purchase function");
	}

	public void SwitchVibrationActive()
	{
		PlayerInput.Instance.isVibration = !PlayerInput.Instance.isVibration;

		if (PlayerInput.Instance.isVibration)
			TurnOnVibration();
		else
			TurnOffVibration();
	}

	public void TurnOnVibration()
	{
		PlayerInput.Instance.isVibration = true;

		Debug.Log("Vibration On");
	}

	public void TurnOffVibration()
	{
		PlayerInput.Instance.isVibration = false;

		Debug.Log("Vibration Off");
	}

	public void Share()
	{
		Debug.Log("Share function");
	}

	public void Rate()
	{
		Debug.Log("Rate function");
	}
}
