using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


public class LevelManager : Singleton<LevelManager>
{
	public List<LevelData> levelDatas;
	public int curLevelID;

	public GameObject tablePrefab;

	// ---------------------------------------------------------------------
	public void OpenNextLevel()
	{
		OpenLevel(curLevelID + 1);
	}

	public void OpenLevel(int x)
	{
		curLevelID = x;


	}

	public void UnlockLevel(bool status)
	{
		UnlockLevel(curLevelID, status);
	}

	public void UnlockLevel(int x, bool status)
	{

	}

	public void FinishLevel(bool status)
	{
		FinishLevel(curLevelID, status);
	}

	public void FinishLevel(int x, bool status)
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
			save.isDone.Add(data.isDone);

		}

		save.curLevelID = curLevelID;

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
				data.isDone = save.isDone[i];

				levelDatas.Add(data);

			}

			curLevelID = save.curLevelID;
			// ---------------------------------------------------------------------
		}
	}

}

[System.Serializable]
public class LevelData
{
	public Weapon weapons;
	public bool isUnlock;
	public bool isDone;
}

[System.Serializable]
public class LevelSaveData
{
	public int curLevelID;

	public List<string> weaponNames = new List<string>();
	public List<bool> isUnlock = new List<bool>();
	public List<bool> isDone = new List<bool>();

}
