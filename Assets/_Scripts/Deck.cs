﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour, ISelectableNR
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
        }
        ShuffleDeck();
        UpdateCardsToParent();
    }

    void UpdateCardsToParent()
	{
        // Reverse because lowest in hierarchy is visually on top of deck (first card, last child)
        for (int i = cardsInDeck.Count-1; i >= 0; i--)
		{
            cardsInDeck[i].transform.SetParent(cardsParentT, false);
        }
    }

    public void ShuffleDeck()
	{
        cardsInDeck = Shuffle(cardsInDeck);
	}


    public static List<T> Shuffle<T>(List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            T temp = _list[i];
            int randomIndex = Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }

        return _list;
    }

    public bool IsDeckEmpty()
	{
        return cardsInDeck.Count == 0;
	}


    public bool DrawNextCard(ref Card nextCard)
	{
        if (!IsDeckEmpty())
		{
            nextCard = cardsInDeck[0];
            cardsInDeck.RemoveAt(0);
            return true;
		}
        return false;
	}

	public bool CanHighlight()
	{
        return true;
	}

	public bool CanSelect()
	{
        return PlayCardManager.instance.CanDrawAnotherCard();
	}

	public void Highlighted()
	{
	}

	public void Selected()
	{
        PlayCardManager.instance.TryDrawNextCard();
	}
}

