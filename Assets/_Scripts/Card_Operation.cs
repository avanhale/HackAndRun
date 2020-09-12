using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Operation : Card, IActivateable
{
    public override bool CanSelect()
    {
        return PlayCardManager.instance.CanActivateEvent(this);
    }

    public bool CanActivate()
    {
        return cardCost.CanAffordCard(myPlayer.Credits);
    }

    public virtual void ActivateCard()
    {
        cardFunction.ActivateFunction();
    }

}
