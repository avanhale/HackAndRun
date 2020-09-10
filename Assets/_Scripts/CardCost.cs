using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCost : MonoBehaviour
{

    public int costOfCard = 0;
    public int memorySpaceCost = 0;

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


    public bool CanUseMemorySpace(int spaceAvailable)
	{
        return spaceAvailable >= memorySpaceCost;
	}

    public bool TryUseMemorySpace(ref int memorySpace)
	{
        if (CanUseMemorySpace(memorySpace))
        {
            memorySpace -= memorySpaceCost;
            return true;
        }
        return false;
    }


}
