using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFunction_Desperado : CardFunction
{
	public int amountMemory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	protected override void SubscribeToConditions()
	{
		base.SubscribeToConditions();
		PlayCardManager.OnCardInstalled += PlayCardManager_OnCardInstalled;
		// Make Successful Run
		//GainCredits();
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
			AddMemory();
		}
	}


	void AddMemory()
	{
		PlayerNR.Runner.AddTotalMemory(amountMemory);
	}

	void GainCredits()
	{

	}


}
