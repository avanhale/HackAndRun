using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card_Ice : Card, IInstallable
{
    public TextMeshProUGUI strengthText;
    public int strength;

    protected override void Start()
    {
        base.Start();
        strengthText.text = strength.ToString();
    }

    public override bool CanSelect()
    {
        return PlayCardManager.instance.CanInstallCard(this);
    }


    public bool CanInstall()
    {
        return true;
        //cardCost.CanAffordCard(PlayerNR.Runner.Credits)
            //&& cardCost.CanUseMemorySpace(PlayerNR.Runner.MemoryUnitsAvailable);
    }
}
