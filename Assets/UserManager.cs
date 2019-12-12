using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : Singleton<UserManager>
{
	public int coin;

	// ---------------------------------------------------------------------
	// *
	// * SAVE / LOAD GAME DATA
	// *
	// ---------------------------------------------------------------------
	public void ClearData()
	{
		coin = 0;
	}


	//protected LevelSaveData CreateSave()
	//{
	//	LevelSaveData save = new LevelSaveData();

	//	foreach (LevelData data in levelDatas) {

	//		save.weaponNames.Add(data.weapons.weaponName);
	//		save.isUnlock.Add(data.isUnlock);
	//		save.isFinished.Add(data.isFinished);

	//	}

	//	save.lastestLevelID = lastestLevelID;

	//	return save;
	//}

	//public void SaveGameData()
	//{
	//	LevelSaveData save = CreateSave();

	//	BinaryFormatter bf = new BinaryFormatter();
	//	FileStream file = File.Create(Application.persistentDataPath + "/levelsave.save");
	//	bf.Serialize(file, save);
	//	file.Close();
	//}

	//public void LoadGameData()
	//{
	//	if (File.Exists(Application.persistentDataPath + "/levelsave.save")) {

	//		BinaryFormatter bf = new BinaryFormatter();
	//		FileStream file = File.Open(Application.persistentDataPath + "/levelsave.save", FileMode.Open);
	//		LevelSaveData save = (LevelSaveData)bf.Deserialize(file);
	//		file.Close();


	//		// Clear
	//		ClearLevelData();

	//		// ---------------------------------------------------------------------
	//		// Load
	//		for (int i = 0; i < save.isUnlock.Count; i++) {

	//			LevelData data = new LevelData();
	//			data.weapons = WeaponPool.Instance.GetWeapon(save.weaponNames[i]);
	//			data.isUnlock = save.isUnlock[i];
	//			data.isFinished = save.isFinished[i];

	//			levelDatas.Add(data);

	//		}

	//		curLevelID = save.lastestLevelID;
	//		lastestLevelID = save.lastestLevelID;

	//		// ---------------------------------------------------------------------
	//	}
	//}
}
