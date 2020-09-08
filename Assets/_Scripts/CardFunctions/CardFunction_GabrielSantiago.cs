using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFunction_GabrielSantiago : CardFunction
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
		GameManager.OnRunFinished += GameManager_OnRunFinished;
	}

	protected override void UnSubscribeToConditions()
	{
		base.UnSubscribeToConditions();
		GameManager.OnRunFinished -= GameManager_OnRunFinished;
	}

	private void GameManager_OnRunFinished(bool success, int serverType)
	{
		if (serverType == 1)
		{
			// & This turn
			// Gain 2 cc
		}
	}
}
