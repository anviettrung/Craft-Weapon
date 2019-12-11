using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


public class LevelManager : Singleton<LevelManager>
{
	public List<LevelData> levelDatas;
	public int curLevelID;
	public int lastestLevelID;

	public Weapon currentWeapon;
	public GameObject tablePrefab;

	// ---------------------------------------------------------------------
	public void OpenLastestLevel()
	{
		OpenLevel(lastestLevelID);
	}

	public void OpenNextLevel()
	{
		OpenLevel(curLevelID + 1);
	}

	public void OpenLevel(int x)
	{
		if (x < 0 || x >= levelDatas.Count) {
			Debug.Log("No level left");
			return;
		}

		curLevelID = x;

		if (currentWeapon != null) {
			currentWeapon.onFinishedWeapon.RemoveListener(FinishLevel);
			Destroy(currentWeapon.gameObject);
		}

		UIManager.Instance.botUI.gameObject.SetActive(false);
		UIManager.Instance.midUI.gameObject.SetActive(false);

		currentWeapon = Instantiate(levelDatas[x].weapons.gameObject).GetComponent<Weapon>();

		currentWeapon.onFinishedWeapon.AddListener(FinishLevel);
	}

	public void UnlockLevel(bool status)
	{
		UnlockLevel(curLevelID, status);
	}

	public void UnlockLevel(int x, bool status)
	{

	}

	public void FinishLevel()
	{
		FinishLevel(curLevelID);
	}

	public void FinishLevel(int x)
	{
		levelDatas[x].isFinished = true;
		lastestLevelID = Mathf.Clamp(lastestLevelID + 1, 0, levelDatas.Count-1);
		SaveGameData();
	}

	public void UpdateShop()
	{

	}


	// ---------------------------------------------------------------------
	// *
	// * SAVE / LOAD GAME DATA
	// *
	// ---------------------------------------------------------------------
	public void ClearLevelData()
	{
		levelDatas = new List<LevelData>();
	}


	protected LevelSaveData CreateSave()
	{
		LevelSaveData save = new LevelSaveData();

		foreach (LevelData data in levelDatas) {
		
			save.weaponNames.Add(data.weapons.weaponName);
			save.isUnlock.Add(data.isUnlock);
			save.isFinished.Add(data.isFinished);

		}

		save.lastestLevelID = lastestLevelID;

		return save;
	}

	public void SaveGameData()
	{
		LevelSaveData save = CreateSave();

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/levelsave.save");
		bf.Serialize(file, save);
		file.Close();
	}

	public void LoadGameData()
	{
		if (File.Exists(Application.persistentDataPath + "/levelsave.save")) {

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/levelsave.save", FileMode.Open);
			LevelSaveData save = (LevelSaveData)bf.Deserialize(file);
			file.Close();


			// Clear
			ClearLevelData();

			// ---------------------------------------------------------------------
			// Load
			for (int i = 0; i < save.isUnlock.Count; i++) {

				LevelData data = new LevelData();
				data.weapons = WeaponPool.Instance.GetWeapon(save.weaponNames[i]);
				data.isUnlock = save.isUnlock[i];
				data.isFinished = save.isFinished[i];

				levelDatas.Add(data);

			}

			curLevelID     = save.lastestLevelID;
			lastestLevelID = save.lastestLevelID;

			// ---------------------------------------------------------------------
		}
	}

}

[System.Serializable]
public class LevelData
{
	public Weapon weapons;
	public bool isUnlock;
	public bool isFinished;
}

[System.Serializable]
public class LevelSaveData
{
	public int lastestLevelID;

	public List<string> weaponNames = new List<string>();
	public List<bool> isUnlock = new List<bool>();
	public List<bool> isFinished = new List<bool>();

}
