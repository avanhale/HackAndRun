﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    public static PlayArea instance;

    [Header("Corporation Spots")]
    public IdentitySpot corpIdentity;
    public Deck corpDeck;
    public Hand corpHand;

    public ActionTracker corpActionTracker;
    public ActionsReferenceCard corpActionsReferenceCard;
    public DiscardPile corpDiscardPile;

    [Header("Runner Spots")]
    public IdentitySpot runnerIdentity;
    public Deck runnerDeck;
    public Hand runnerHand;
    public ActionTracker runnerActionTracker;
    public ActionsReferenceCard runnerActionsReferenceCard;
    public DiscardPile runnerDiscardPile;


    private void Awake()
    {
        instance = this;
        GetAllReferences();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void SetCardsToSpots(PlayerNR player)
    {
        IdentitySpot identity = IdentityNR(player);
        identity.SetIdentityCard(player.identity);
        DeckNR(player).SetCardsToDeck(player.deckCards);
    }

    public void AddCardsToHand(PlayerNR player, Card[] cards)
    {
        Hand hand = HandNR(player);
        hand.AddCardsToHand(cards);
    }

    public Card DrawCardFromDeck(PlayerNR player)
    {
        Deck deck = DeckNR(player);
        Card drawnCard = null;
        if (deck.DrawNextCard(ref drawnCard))
        {
            return drawnCard;
        }
        return null;
    }

    public int CostOfAction(PlayerNR player, int actionIndex)
    {
        if (player.IsRunner())
        {
            return runnerActionsReferenceCard.CostOfAction(actionIndex);
        }
        return -123;
    }

    public void SendCardToDiscard(PlayerNR player, Card card)
	{
        if (player.IsRunner())
		{
            runnerDiscardPile.AddCardToDiscard(card);
        }

    }


	#region

    public IdentitySpot IdentityNR(PlayerNR player)
	{
        return player.IsRunner() ? runnerIdentity : corpIdentity;
    }

    public Deck DeckNR(PlayerNR player)
	{
        return player.IsRunner() ? runnerDeck : corpDeck;
    }

    public Hand HandNR(PlayerNR player)
	{
        return player.IsRunner() ? runnerHand : corpHand;
    }


    #endregion



    void GetAllReferences()
    {
		foreach (var spot in GetComponentsInChildren<PlayArea_Spot>())
		{
            if (spot is IdentitySpot)
            {
                IdentitySpot identity = spot as IdentitySpot;
                if (spot.myPlayer.IsRunner()) runnerIdentity = identity;
                else corpIdentity = identity;
            }
            else if (spot is Deck)
			{
                Deck deck = spot as Deck;
                if (spot.myPlayer.IsRunner()) runnerDeck = deck;
                else corpDeck = deck;
			}
            else if (spot is Hand)
			{
                Hand hand = spot as Hand;
                if (spot.myPlayer.IsRunner()) runnerHand = hand;
                else corpHand = hand;
            }
            else if (spot is ActionTracker)
            {
                ActionTracker tracker = spot as ActionTracker;
                if (spot.myPlayer.IsRunner()) runnerActionTracker = tracker;
                else corpActionTracker = tracker;
            }
            else if (spot is ActionsReferenceCard)
            {
                ActionsReferenceCard referenceCard = spot as ActionsReferenceCard;
                if (spot.myPlayer.IsRunner()) runnerActionsReferenceCard = referenceCard;
                else corpActionsReferenceCard = referenceCard;
            }
            else if (spot is DiscardPile)
            {
                DiscardPile discardPile = spot as DiscardPile;
                if (spot.myPlayer.IsRunner()) runnerDiscardPile = discardPile;
                else corpDiscardPile = discardPile;
            }
        }
    }



}
