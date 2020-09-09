using UnityEngine;
using TMPro;

public class CreditsUI : MonoBehaviour
{
    public PlayerSide myPlayerSide;
	public TextMeshProUGUI creditsText;
	PlayerNR myPlayer;

	private void Awake()
	{
		myPlayer = myPlayerSide == PlayerSide.Runner ? PlayerNR.Runner : PlayerNR.Corporation;
	}

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
