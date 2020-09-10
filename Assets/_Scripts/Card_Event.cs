using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Event : Card, IActivateable
{


	public override bool CanSelect()
	{
        return PlayCardManager.instance.CanActivateEvent(this);
	}

    public bool CanActivate()
    {
        return cardCost.CanAffordCard(PlayerNR.Runner.Credits);
    }

    public virtual void ActivateCard()
	{
		cardFunction.ActivateFunction();
	}

	
}
