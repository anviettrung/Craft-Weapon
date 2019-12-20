using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationCooldownUI : MonoBehaviour
{
	public Text coolTime;

	public void ChangeVibrationTime(float x)
	{
		PlayerInput.Instance.vibrationCooldown = x;
		coolTime.text = x.ToString("F2");
	}

}
