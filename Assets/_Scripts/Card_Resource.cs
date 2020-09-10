using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Resource : Card, IInstallable
{
	

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
	}


	void Update()
    {
        
    }


	public override bool CanSelect()
	{
		return PlayCardManager.instance.CanInstallCard(this);
	}


	public bool CanInstall()
	{
		return cardCost.CanAffordCard(PlayerNR.Runner.Credits);
	}

}
