using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCost : MonoBehaviour
{

    public int costOfCard;

    public bool CanAffordCard(int balance)
	{
        return balance >= costOfCard;
	}

    public bool TryBuyCard(ref int newBalance)
	{
        if (CanAffordCard(newBalance))
		{
            newBalance -= costOfCard;
            return true;
		}
        return false;
	}
}
