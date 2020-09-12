using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card_Program : Card, IInstallable
{
	public TextMeshProUGUI memorySpaceText, strengthText;
	public int strength;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
		memorySpaceText.text = cardCost.memorySpaceCost.ToString();
		UpdateStrengthText();
	}


	public override bool CanSelect()
	{
		if (PlayArea.instance.HandNR(myPlayer).IsCardInHand(this))
		{
			if (PlayCardManager.instance.CanInstallCard(this))
			{
				return true;
			}
		}
		else if (RunnerRIG.instance.IsCardInRIG(this))
		{
			if (RunOperator.instance.isRunning)
			{
				return true;
			}
		}

		return false;
	}


	public bool CanInstall()
	{
		return cardCost.CanAffordCard(myPlayer.Credits)
			&& cardCost.CanUseMemorySpace(PlayerNR.Runner.MemoryUnitsAvailable);
	}


	public void ModifyStrength(int modification)
	{
		strength += modification;
		UpdateStrengthText();
	}

	void UpdateStrengthText()
	{
		strengthText.text = strength.ToString();
	}




}
