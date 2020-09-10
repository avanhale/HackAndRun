using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public abstract class Card : MonoBehaviour, ISelectableNR
{
    CardReferences cardRefs;
    protected CardFunction cardFunction;
    [HideInInspector]
    public CardCost cardCost;

    [Header("Card Data")]
    public PlayerSide playerSide;
    protected PlayerNR myPlayer;
    public CardType cardType;
    public CardSubType cardSubType;
    public string cardTitle;

    [Header("Play Data")]
    [HideInInspector]
    public bool isFaceUp = true;
    float cardFlipTransitionTime = 1f;



    protected virtual void Awake()
	{
        isFaceUp = true;
        cardRefs = GetComponent<CardReferences>();
        cardFunction = GetComponent<CardFunction>();
        cardCost = GetComponent<CardCost>();
        myPlayer = playerSide == PlayerSide.Runner ? PlayerNR.Runner : PlayerNR.Corporation;
    }

	protected virtual void OnEnable()
	{
		PlayCardManager.OnCardInstalled += OnCardInstalled;
	}
    protected virtual void OnDisable()
    {
        PlayCardManager.OnCardInstalled -= OnCardInstalled;
    }
    protected virtual void OnCardInstalled(Card card, bool installed)
	{

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


    public void FlipCard()
	{
        isFaceUp = !isFaceUp;
        UpdateCardFlipDisplay();
	}

    public void FlipCardUp()
	{
        isFaceUp = true;
        UpdateCardFlipDisplay();
	}
    public void FlipCardDown()
    {
        isFaceUp = false;
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

    public bool IsCardInHand()
	{
        return PlayerNR.Runner.IsCardInHand(this);
	}


	public bool CanHighlight()
	{
        return true;
	}

	public virtual bool CanSelect()
	{
        return true;
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
    public static void ParentCardTo(this Card card, Transform parent)
	{
        card.transform.localScale = Vector3.one;
        card.transform.SetParent(parent, false);
        card.transform.localPosition = Vector3.zero;
    }
}
