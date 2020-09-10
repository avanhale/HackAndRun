using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TagsUI : MonoBehaviour, ISelectableNR
{
	public PlayerSide playerSide;
	PlayerNR myPlayer;
	public TextMeshProUGUI tagsText;

	private void Awake()
	{
		myPlayer = playerSide == PlayerSide.Runner ? PlayerNR.Runner : PlayerNR.Corporation;
	}

	private void OnEnable()
	{
		myPlayer.OnTagsChanged += MyPlayer_OnTagsChanged;
	}
	private void OnDisable()
	{
		myPlayer.OnTagsChanged -= MyPlayer_OnTagsChanged;
	}

	private void MyPlayer_OnTagsChanged()
	{
		UpdateTagsText();
	}
	void UpdateTagsText()
	{
		tagsText.text = myPlayer.Tags.ToString();
	}


	public bool CanHighlight()
	{
		return true;
	}

	public bool CanSelect()
	{
		return PlayCardManager.instance.CanRemoveTag();
	}

	public void Highlighted()
	{
	}

	public void Selected()
	{
		PlayCardManager.instance.TryRemoveTag();
	}


	

}
