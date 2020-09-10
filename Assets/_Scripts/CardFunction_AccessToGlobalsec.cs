using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFunction_AccessToGlobalsec : CardFunction
{
	public int amountLinks;
	protected override void SubscribeToConditions()
	{
		base.SubscribeToConditions();
		PlayCardManager.OnCardInstalled += PlayCardManager_OnCardInstalled;
	}

	protected override void UnSubscribeToConditions()
	{
		base.UnSubscribeToConditions();
		PlayCardManager.OnCardInstalled -= PlayCardManager_OnCardInstalled;
	}

	private void PlayCardManager_OnCardInstalled(Card card, bool installed)
	{
		if (card == this.card)
		{
			AddLink();
		}
	}

	void AddLink()
	{
		PlayerNR.Runner.LinkStrength += amountLinks;
	}

}
