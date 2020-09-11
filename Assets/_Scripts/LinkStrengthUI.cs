using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LinkStrengthUI : PlayArea_Spot
{
	public TextMeshProUGUI linkStrengthText;

	protected override void Awake()
	{
		base.Awake();
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
