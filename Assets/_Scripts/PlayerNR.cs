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


    
    [Header("Data")]
    [SerializeField]
    int credits;
    public int Credits
	{
        get { return credits; }
        set { credits = value;
            OnCreditsChanged?.Invoke(); }
	}
    public delegate void CreditsChanged();
    public event CreditsChanged OnCreditsChanged;


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
	}

    public bool IsRunner()
    {
        return playerSide == PlayerSide.Runner;
    }


    public void AddCredits(int numCredits)
	{
        Credits += numCredits;
	}



}
