using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card_Program : Card, IInstallable
{
	public TextMeshProUGUI memorySpaceText;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
		memorySpaceText.text = cardCost.memorySpaceCost.ToString();
	}


	public override bool CanSelect()
	{
		return PlayCardManager.instance.CanInstallCard(this);
	}


	public bool CanInstall()
	{
		return cardCost.CanAffordCard(PlayerNR.Runner.Credits)
			&& cardCost.CanUseMemorySpace(PlayerNR.Runner.MemoryUnitsAvailable);
	}

}
