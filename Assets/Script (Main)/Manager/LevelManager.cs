using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


public class LevelManager : Singleton<LevelManager>
{
	public List<LevelData> levelDatas;

	public void ClearLevelData()
	{
		levelDatas = new List<LevelData>();
	}


	protected LevelSaveData CreateSave()
	{
		LevelSaveData save = new LevelSaveData();

		foreach (LevelData data in levelDatas) {

			if (data.weapons != null)
				save.weaponNames.Add(data.weapons.weaponName);
			save.isUnlock.Add(data.isUnlock);
			save.isDone.Add(data.isDone);

		}

		return save;
	}

	public void SaveGame()
	{
		LevelSaveData save = CreateSave();

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/levelsave.save");
		bf.Serialize(file, save);
		file.Close();
	}

	public void LoadGame()
	{
		if (File.Exists(Application.persistentDataPath + "/levelsave.save")) {

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/levelsave.save", FileMode.Open);
			LevelSaveData save = (LevelSaveData)bf.Deserialize(file);
			file.Close();

			ClearLevelData();

			for (int i = 0; i < save.isUnlock.Count; i++) {

				LevelData data = new LevelData();
				data.weapons = WeaponPool.Instance.GetWeapon(save.weaponNames[i]);
				data.isUnlock = save.isUnlock[i];
				data.isDone = save.isDone[i];

				levelDatas.Add(data);

			}

		} else {
			// No Level Save
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
	public List<string> weaponNames = new List<string>();
	public List<bool> isUnlock = new List<bool>();
	public List<bool> isDone = new List<bool>();

}
