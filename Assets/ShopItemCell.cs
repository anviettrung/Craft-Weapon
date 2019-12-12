using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemCell : MonoBehaviour
{
	public enum CellType
	{
		LOCKED,
		UNFINISH,
		FINISHED
	}

	public CellType cellType;
	public Text nameLabel;

	// Finished Cell elements
	public Button finishedCell;
	public Image weaponImage;

	// Lock Cell elements
	public Button lockCell;
	public Text coinLabel;

	protected LevelData trackData = null;

	public void Unlock()
	{
		if (trackData == null) return;
		trackData.Unlock(true);
		UpdateUI();
	}

	public void PlayThisLevel()
	{
		if (trackData == null) return;
		LevelManager.Instance.OpenLevel(trackData.weapons.weaponName);
	}

	public void UpdateUI()
	{
		if (trackData == null) return;

		UpdateUI(
			GetCellType(trackData.isUnlock, trackData.isFinished),
			trackData.weapons.weaponName,
			trackData.weapons.weaponIcon,
			trackData.weapons.unlockCoins
		);
	}

	public void UpdateUI (LevelData data)
	{
		trackData = data;
		UpdateUI();
	}

	public void UpdateUI(CellType cellType, string weaponName, Sprite weaponIcon, int coins)
	{
		lockCell.gameObject.SetActive(false);
		finishedCell.gameObject.SetActive(false);

		if (cellType == CellType.LOCKED) {

			lockCell.gameObject.SetActive(true);
			coinLabel.text = Mathf.Clamp(coins, 0, 9999).ToString();


		} else if (cellType == CellType.FINISHED || cellType == CellType.UNFINISH) {

			finishedCell.gameObject.SetActive(true);
			weaponImage.sprite = weaponIcon;

		}

		nameLabel.text = weaponName;
	}

	public static CellType GetCellType(bool isUnlock, bool isFinished)
	{
		if (isUnlock == false) return CellType.LOCKED;
		if (isFinished) return CellType.FINISHED;
		else return CellType.UNFINISH;
	}

}
