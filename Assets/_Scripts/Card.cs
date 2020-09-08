using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Card : MonoBehaviour
{
    CardReferences cardRefs;
    CardFunction cardFunction;
    CardCost cardCost;
    [Header("Card Data")]
    public PlayerSide playerSide;
    public CardType cardType;
    public CardSubType cardSubType;
    public string cardTitle;



	private void Awake()
	{
        cardRefs = GetComponent<CardReferences>();
        cardFunction = GetComponent<CardFunction>();
        cardCost = GetComponent<CardCost>();
    }

	// Start is called before the first frame update
	void Start()
    {
        print(cardType.TypeToString());
        print(cardSubType.TypeToString());
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


	//private void OnValidate()
	//{
 //       if (titleText) titleText.text = cardTitle;
	//}


}
