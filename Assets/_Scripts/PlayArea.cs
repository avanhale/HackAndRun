using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    public static PlayArea instance;

    [Header("Corporation Spots")]
    public RectTransform corpIdentityT;


    [Header("Runner Spots")]
    public RectTransform runnerIdentityT;
    public Deck runnerDeck;
    public Hand runnerHand;
    public ActionTracker runnerActionTracker;


	private void Awake()
	{
        instance = this;
    }


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetCardsToSpots(GameManager.Player player)
	{
        if (player.IsRunner())
		{
            player.identity.transform.SetParent(runnerIdentityT, false);
            runnerDeck.SetCardsToDeck(player.deckCards);
            //runnerHand.AddCardsToHand(player.)
		}
	}

    public void AddCardsToHand(GameManager.Player player, Card[] cards)
	{
        if (player.IsRunner())
		{
            runnerHand.AddCardsToHand(cards);
		}
	}

    public Card DrawCardFromDeck(GameManager.Player player)
	{
        if (player.IsRunner())
		{
            Card drawnCard = null;
            if(runnerDeck.DrawNextCard(ref drawnCard))
			{
                return drawnCard;
			}
		}

        return null;
	}

    public void ResetActionTracker(GameManager.Player player)
	{
        if (player.IsRunner())
		{
            runnerActionTracker.ResetActionPoints();
        }
	}



}
