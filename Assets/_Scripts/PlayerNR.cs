using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNR : MonoBehaviour
{
    public static PlayerNR Corporation, Runner;
    
    [Header("Initialization")]
    public ScriptableDeck e_PlayerDeck;

    [Header("Player")]
    public PlayerSide playerSide;
    public Card_Identity identity;
    public Card[] deckCards;


    int maximumHandSize = 5;

    [Header("Data")]
    [SerializeField]
    int credits;
    public int Credits
	{
        get { return credits; }
        set { credits = value;
            OnCreditsChanged?.Invoke();
        }
	}
    public delegate void ValueChanged();
    public event ValueChanged OnCreditsChanged;

    [SerializeField]
    int actionPoints;
    public int ActionPoints
	{
        get { return actionPoints; }
        set { actionPoints = value;
            OnActionPointsChanged?.Invoke();
        }
	}
    public event ValueChanged OnActionPointsChanged;


    [SerializeField]
    int memoryUnitsTotal;
    public int MemoryUnitsTotal
	{
        get { return memoryUnitsTotal; }
        set { memoryUnitsTotal = value;
            OnMemoryChanged?.Invoke();
        }
	}

    [SerializeField]
    int memoryUnitsAvailable;
    public int MemoryUnitsAvailable
    {
        get { return memoryUnitsAvailable; }
        set
        {
            memoryUnitsAvailable = value;
            OnMemoryChanged?.Invoke();
        }
    }
    public event ValueChanged OnMemoryChanged;

    [SerializeField]
    List<Card> cardsInHand = new List<Card>();

    [SerializeField]
    int linkStrength;
    public int LinkStrength
	{
        get { return linkStrength; }
        set { linkStrength = value;
            OnLinkStrengthChanged?.Invoke();
        }
	}
    public event ValueChanged OnLinkStrengthChanged;

    [SerializeField]
    int tags;
    public int Tags
    {
        get { return tags; }
        set
        {
            tags = value;
            OnTagsChanged?.Invoke();
        }
    }
    public event ValueChanged OnTagsChanged;


    private void Awake()
	{
        GatherPlayer();
        if (IsRunner()) Runner = this;
        else Corporation = this;
    }


	// Start is called before the first frame update
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GatherPlayer()
	{
        playerSide = e_PlayerDeck.playerSide;
    }

    public void SetPlayerCards(Card_Identity _identity, Card[] _deckCards)
	{
        identity = _identity;
        deckCards = _deckCards;
        LinkStrength = _identity.baseLinkStrength;
    }

    public bool IsRunner()
    {
        return playerSide == PlayerSide.Runner;
    }


    public void AddCredits(int numCredits)
	{
        Credits += numCredits;
	}

    public bool CanAffordCost(int cost)
	{
        return Credits >= cost;
	}


    public bool CanAffordAction(int numActions)
    {
        return actionPoints >= numActions;
    }

    public bool TryUseActionPoints(int numActions)
    {
        if (CanAffordAction(numActions))
        {
            ActionPointsUsed(numActions);
            return true;
        }
        return false;
    }
    public void ActionPointsUsed(int numActions)
    {
        ActionPoints -= numActions;
    }

    public void AddTotalMemory(int amountMemory)
	{
        MemoryUnitsTotal += amountMemory;
        MemoryUnitsAvailable += amountMemory;
	}

    public void AddCardsToHand(params Card[] cards)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cardsInHand.Add(cards[i]);
        }
        PlayArea.instance.AddCardsToHand(this, cards);
    }

    public int NumberOfCardsInHand()
    {
        return cardsInHand.Count;
    }

    public bool HandSizeFull()
    {
        return NumberOfCardsInHand() >= maximumHandSize;
    }

    public bool IsCardInHand(Card card)
    {
        return cardsInHand.Contains(card);
    }


    public void AddTags(int numTags)
	{
        Tags += numTags;
	}

    public void RemoveTags(int numTags)
	{
        Tags -= numTags;
	}



}
