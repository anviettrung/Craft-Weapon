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

	// Bot UI
	public Text rewardCoinLabel;


	//----------------------------------------------------------
	// 
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
	// 
	//----------------------------------------------------------


	//----------------------------------------------------------
	// 
	//----------------------------------------------------------
	public void SetLabel_RewardCoin(int x)
	{
		rewardCoinLabel.text = x.ToString();
	}

}
