using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform cardsParentT;
    List<Card> cardsInHand = new List<Card>();
    int maximumHandSize = 5;
    public float cardScaleInHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCardsToHand(params Card[] cards)
	{
		for (int i = 0; i < cards.Length; i++)
		{
            cardsInHand.Add(cards[i]);
            cards[i].transform.SetParent(cardsParentT, false);
            ScaleCard(cards[i]);
		}
	}




    void ScaleCard(Card card, bool scale = true)
	{
        float scaleFactor = scale ? cardScaleInHand : 1;
        card.transform.localScale = Vector3.one * scaleFactor;
    }

    public int NumberOfCardsInHand()
	{
        return cardsInHand.Count;
	}

    public bool HandSizeFull()
	{
        return NumberOfCardsInHand() >= maximumHandSize;
	}




}
