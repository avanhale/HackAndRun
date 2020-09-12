using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFunction_Aurora : CardFunction
{
    public int strengthToAdd;

	public override void ActivateFunction()
	{
		base.ActivateFunction();
		//GainCredits();
	}


	public override void ActivatePaidAbility(int index)
	{
		base.ActivatePaidAbility(index);
		if (index == 1) BreakSubroutine();
		else if (index == 2) AddStrength();
	}

	void BreakSubroutine()
	{

	}

	void AddStrength()
	{
		Card_Program programCard = card as Card_Program;
		programCard.ModifyStrength(strengthToAdd);
	}

	





}
