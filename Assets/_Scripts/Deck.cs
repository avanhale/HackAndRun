using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public Transform cardsParentT;
    public List<Card> cardsInDeck;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetCardsToDeck(Card[] cards)
	{
        cardsInDeck = new List<Card>();
		for (int i = 0; i < cards.Length; i++)
		{
            cardsInDeck.Add(cards[i]);
            cards[i].transform.SetParent(cardsParentT, false);
        }
	}




}
