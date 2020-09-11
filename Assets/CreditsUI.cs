using UnityEngine;
using TMPro;

public class CreditsUI : PlayArea_Spot
{
	public TextMeshProUGUI creditsText;

	private void OnEnable()
	{
		myPlayer.OnCreditsChanged += MyPlayer_OnCreditsChanged;
	}

	private void OnDisable()
	{
		myPlayer.OnCreditsChanged -= MyPlayer_OnCreditsChanged;
	}

	private void MyPlayer_OnCreditsChanged()
	{
		creditsText.text = myPlayer.Credits.ToString();
	}
}
