using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class UserManager : Singleton<UserManager>
{

	public int coins;

	public void SetCoin(int x)
	{
		coins = x;
		UpdateCoinLabel();
	}

	public void IncCoin(int x)
	{
		coins += x;
		UpdateCoinLabel();
	}

	private void Update()
	{
		UpdateCoinLabel();
	}

	void UpdateCoinLabel()
	{
		UIManager.Instance.SetLabel_Coin(coins);
	}

	// ---------------------------------------------------------------------
	// *
	// * SAVE / LOAD GAME DATA
	// *
	// ---------------------------------------------------------------------
	public void ClearData()
	{
		coins = 0;
	}


	protected UserData CreateSave()
	{
		UserData save = new UserData();

		save.coins = coins;

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
			coins = save.coins;

			// ---------------------------------------------------------------------
		}
	}
}

[System.Serializable]
public class UserData
{
	public int coins;
}