using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFunction_Desperado : CardFunction
{
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
		GameManager.OnCardInstallation += GameManager_OnCardInstallation;
	}

	

	protected override void UnSubscribeToConditions()
	{
		base.UnSubscribeToConditions();
		GameManager.OnCardInstallation -= GameManager_OnCardInstallation;
	}

	private void GameManager_OnCardInstallation(Card card, bool installed)
	{
		if (card == this.card)
		{
			if (installed)
			{
				AddMemory();
			}
		}
	}


	void AddMemory()
	{
		// Add 1 memory
	}


}
