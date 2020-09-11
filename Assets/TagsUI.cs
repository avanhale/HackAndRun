using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TagsUI : PlayArea_Spot, ISelectableNR
{
	public TextMeshProUGUI tagsText;

	protected override void Awake()
	{
		base.Awake();
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
