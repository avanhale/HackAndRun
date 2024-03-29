﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public abstract class Card : MonoBehaviour, ISelectableNR
{
    CardReferences cardRefs;
    [HideInInspector]
    public CardFunction cardFunction;
    [HideInInspector]
    public CardCost cardCost;

    [Header("Card Data")]
    public PlayerSide playerSide;
    [HideInInspector]
    public PlayerNR myPlayer;
    public CardType cardType;
    public CardSubType cardSubType;
    public string cardTitle;

    [Header("Play Data")]
    [HideInInspector]
    public bool isFaceUp = true;
    float cardFlipTransitionTime = 1f;


    public int viewIndex;



    protected virtual void Awake()
	{
        isFaceUp = true;
        cardRefs = GetComponent<CardReferences>();
        cardFunction = GetComponent<CardFunction>();
        cardCost = GetComponent<CardCost>();
        myPlayer = playerSide == PlayerSide.Runner ? PlayerNR.Runner : PlayerNR.Corporation;
        Pinned(false);
    }

	protected virtual void OnEnable()
	{
		PlayCardManager.OnCardInstalled += OnCardInstalled;
		myPlayer.OnCreditsChanged += MyPlayer_OnCreditsChanged;
	}

	protected virtual void OnDisable()
    {
        PlayCardManager.OnCardInstalled -= OnCardInstalled;
		myPlayer.OnCreditsChanged -= MyPlayer_OnCreditsChanged;
    }
    protected virtual void OnCardInstalled(Card card, bool installed)
	{

	}

    private void MyPlayer_OnCreditsChanged()
    {
        cardFunction?.UpdatePaidAbilitesActive_Credits(myPlayer.Credits);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        UpdateCardTitle();
        UpdateCardTypes();
        UpdateCardCost();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsCardType(CardType _cardType)
	{
        return cardType == _cardType;
	}

    public bool IsOwnedByPlayerSide(PlayerSide _playerSide)
	{
        return playerSide == _playerSide;
	}

    void UpdateCardCost()
	{
        cardRefs.UpdateCardCost(cardCost.costOfCard);
	}

    void UpdateCardTitle()
    {
        cardRefs.UpdateCardTitle(cardTitle);
    }

    void UpdateCardTypes()
	{
        cardRefs.UpdateCardTypes(cardType.TypeToString(), cardSubType.TypeToString());
    }



    // Installation
    protected virtual void CardInstalled()
	{

	}

    protected virtual void CardUnInstalled()
    {

    }

    [ContextMenu("FlipCard")]
    public void FlipCard()
	{
        isFaceUp = !isFaceUp;
        UpdateCardFlipDisplay();
	}

    public void FlipCardUp(bool immediate = false)
	{
        isFaceUp = true;
        UpdateCardFlipDisplay(immediate);
	}
    public void FlipCardDown(bool immediate = false)
    {
        isFaceUp = false;
        UpdateCardFlipDisplay(immediate);
    }

    void UpdateCardFlipDisplay(bool immediate = false)
	{
        float time = immediate ? 0 : cardFlipTransitionTime;
        if (isFaceUp)
		{
            transform.DOLocalRotate(Vector3.zero, time);
		}
        else
		{
            transform.DOLocalRotate(Vector3.up * 180, time);
        }
    }

    public bool IsCardInHand()
	{
        return PlayArea.instance.HandNR(myPlayer).IsCardInHand(this);
	}


	public bool CanHighlight()
	{
        return true;
	}

	public virtual bool CanSelect()
	{
        return false;
	}

	public void Highlighted()
	{
	}

	public void Selected()
	{
        if (IsCardInHand())
		{
            ActivateCardFromHand();
		}
        //FlipCard();
        print(IsCardInHand());
	}

    public void Pinned(bool pinned = true)
    {
        cardRefs.cardPin.enabled = pinned;
    }

    public void ActivateCardFromHand()
	{
        if (this is IInstallable)
		{
            IInstallable installable = this as IInstallable;
            PlayCardManager.instance.TryInstallCard(installable);
		}
		else if (this is IActivateable)
		{
            IActivateable activateable = this as IActivateable;
            PlayCardManager.instance.TryActivateEvent(activateable);
        }
    }

	


	//private void OnValidate()
	//{
	//       if (titleText) titleText.text = cardTitle;
	//}


}



static class CardExtensions
{
    public static void MoveCardTo(this Card card, Transform parent)
	{
        // Remove from list
        PlayArea_Spot currentSpot = card.GetComponentInParent<PlayArea_Spot>();
        PlayArea_Spot newSpot = parent.GetComponentInParent<PlayArea_Spot>();
        if (currentSpot && newSpot != currentSpot)
        {
            currentSpot.RemoveCard(card);
        }

        card.ParentCardTo(parent);
    }

    public static void ParentCardTo(this Card card, Transform parent)
	{
        

        // Parent card
        RectTransform rt = card.GetComponent<RectTransform>();
        card.transform.localScale = Vector3.one;
        card.transform.SetParent(parent, false);
        rt.anchorMin = rt.anchorMax = Vector2.one * 0.5f;
        rt.anchoredPosition = Vector3.zero;
        
    }
}
