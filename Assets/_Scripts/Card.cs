using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public abstract class Card : MonoBehaviour, ISelectableNR
{
    CardReferences cardRefs;
    CardFunction cardFunction;
    CardCost cardCost;

    [Header("Card Data")]
    public PlayerSide playerSide;
    public CardType cardType;
    public CardSubType cardSubType;
    public string cardTitle;

    [Header("Play Data")]
    public bool isFaceUp = true;
    float cardFlipTransitionTime = 1f;



    protected virtual void Awake()
	{
        cardRefs = GetComponent<CardReferences>();
        cardFunction = GetComponent<CardFunction>();
        cardCost = GetComponent<CardCost>();
    }

	// Start is called before the first frame update
	protected virtual void Start()
    {
        isFaceUp = true;
        UpdateCardTitle();
        UpdateCardTypes();
        if (cardCost) UpdateCardCost();
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


    public void FlipCard()
	{
        isFaceUp = !isFaceUp;
        UpdateCardFlipDisplay();
	}

    void UpdateCardFlipDisplay()
	{
        if (isFaceUp)
		{
            transform.DOLocalRotate(Vector3.zero, cardFlipTransitionTime);
		}
        else
		{
            transform.DOLocalRotate(Vector3.up * 180, cardFlipTransitionTime);
        }
    }


	public bool CanHighlight()
	{
        return true;
	}

	public bool CanSelect()
	{
        return true;
	}

	public void Highlighted()
	{
	}

	public void Selected()
	{
        FlipCard();
	}



	//private void OnValidate()
	//{
	//       if (titleText) titleText.text = cardTitle;
	//}


}
