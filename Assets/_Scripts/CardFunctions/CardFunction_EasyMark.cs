using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFunction_EasyMark : CardFunction
{

	public int numCredits;

	public override void ActivateFunction()
	{
		base.ActivateFunction();
		GainCredits();
	}

	void GainCredits()
	{
		PlayerNR.Runner.AddCredits(numCredits);
	}


}
