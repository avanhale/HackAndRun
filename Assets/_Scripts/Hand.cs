using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public PlayerSide playerSide;
    PlayerNR myPlayer;
    public Transform cardsParentT;
    int maximumHandSize = 5;
    public float cardScaleInHand;

	private void Awake()
	{
        myPlayer = playerSide == PlayerSide.Runner ? PlayerNR.Runner : PlayerNR.Corporation;
    }

    public void AddCardsToHand(params Card[] cards)
	{
		for (int i = 0; i < cards.Length; i++)
		{
            cards[i].transform.SetParent(cardsParentT, false);
            ScaleCard(cards[i]);
		}
	}

    void ScaleCard(Card card, bool scale = true)
	{
        float scaleFactor = scale ? cardScaleInHand : 1;
        card.transform.localScale = Vector3.one * scaleFactor;
    }

}
