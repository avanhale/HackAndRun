using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : PlayArea_Spot
{
    public Transform cardsParentT;
    public List<Card> cardsInDiscard = new List<Card>();

	protected override void Awake()
	{
		base.Awake();
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void AddCardToDiscard(Card card)
	{
        card.MoveCardTo(cardsParentT);
        card.FlipCardDown();
        cardsInDiscard.Add(card);
    }

	public override void RemoveCard(Card card)
	{
		base.RemoveCard(card);
        cardsInDiscard.Remove(card);
	}


}
