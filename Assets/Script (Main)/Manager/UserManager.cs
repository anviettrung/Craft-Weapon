using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class UserManager : Singleton<UserManager>
{

	protected int coins = 0;
	public int Coins {
		get {
			return coins;
		}

		set {
			coins = value;
			UpdateCoinLabel();
		}
	}

	// ---------------------------------------------------------------------
	// *
	// * General functions
	// *
	// ---------------------------------------------------------------------
	private void Start()
	{
		UpdateCoinLabel();
	}

	// ---------------------------------------------------------------------
	// *
	// * Script main function
	// *
	// ---------------------------------------------------------------------

	public void Buy(Weapon buyingWeapon)
	{
		if (buyingWeapon.unlockCoins > Coins) {
			Debug.Log("Don't have enough coin. Open purchase or watch Ads panel");
		} else {
			Debug.Log("Pop Up \"Are you sure?\" panel");
			Coins -= buyingWeapon.unlockCoins;
			LevelManager.Instance.UnlockLevel(buyingWeapon.weaponName);
		}
	}

	public void SetCoin(int x)
	{
		Coins = x;
		UpdateCoinLabel();
	}

	public void IncreaseCoin(int x)
	{
		Coins += x;
		UpdateCoinLabel();
	}

	void UpdateCoinLabel()
	{
		UIManager.Instance.SetLabel_Coin(Coins);
	}

	// ---------------------------------------------------------------------
	// *
	// * SAVE / LOAD GAME DATA
	// *
	// ---------------------------------------------------------------------
	public void ClearData()
	{
		Coins = 0;
	}


	protected UserData CreateSave()
	{
		UserData save = new UserData();

		save.coins = Coins;

		return save;
	}

	public void SaveGameData()
	{
		UserData save = CreateSave();

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/userdata.save");
		bf.Serialize(file, save);
		file.Close();
	}

	public void LoadGameData()
	{
		if (File.Exists(Application.persistentDataPath + "/userdata.save")) {

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/userdata.save", FileMode.Open);
			UserData save = (UserData)bf.Deserialize(file);
			file.Close();


			// Clear
			ClearData();

			// ---------------------------------------------------------------------
			// Load
			Coins = save.coins;

			// ---------------------------------------------------------------------
		}
	}
}

[System.Serializable]
public class UserData
{
	public int coins;
}