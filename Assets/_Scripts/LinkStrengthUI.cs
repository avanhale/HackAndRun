using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LinkStrengthUI : MonoBehaviour
{
	public PlayerSide playerSide;
	PlayerNR myPlayer;
	public TextMeshProUGUI linkStrengthText;

	private void Awake()
	{
        myPlayer = playerSide == PlayerSide.Runner ? PlayerNR.Runner : PlayerNR.Corporation;
	}
	protected void OnEnable()
	{
		myPlayer.OnLinkStrengthChanged += MyPlayer_OnLinkStrengthChanged;
	}
	protected void OnDisable()
	{
		myPlayer.OnLinkStrengthChanged -= MyPlayer_OnLinkStrengthChanged;
	}

	private void MyPlayer_OnLinkStrengthChanged()
	{
		linkStrengthText.text = myPlayer.LinkStrength.ToString();
	}
}
