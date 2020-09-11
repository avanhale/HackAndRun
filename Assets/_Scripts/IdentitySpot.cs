using UnityEngine;

public class IdentitySpot : PlayArea_Spot
{
    public Transform cardIdentityT;

    public void SetIdentityCard(Card card)
	{
		card.ParentCardTo(cardIdentityT);
	}
}
