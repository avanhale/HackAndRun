using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Hardware : Card, IInstallable
{

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
	}



	// Update is called once per frame
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
