using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardManager : MonoBehaviour
{
    public static PlayCardManager instance;

    // Action Point Indexes
    private const int
        RUNNER_DRAW_CARD = 1,
        RUNNER_GAIN_CREDIT = 2,
        RUNNER_INSTALL = 3,
        RUNNER_EVENT = 4,
        RUNNER_REMOVE_TAG = 5,
        RUNNER_MAKE_RUN = 6;
    private const int
        CORP_DRAW_CARD = 1,
        CORP_GAIN_CREDIT = 2,
        CORP_INSTALL = 3,
        CORP_OPERATION = 4,
        CORP_ADVANCE = 5,
        CORP_PURGE_VIRUSES = 6,
        CORP_TRASH_RESOURCE_TAGGED = 7;

    public delegate void CardInstalled(Card card, bool installed);
    public static event CardInstalled OnCardInstalled;

    public delegate void CardActivated(Card card);
    public static event CardActivated OnCardActivated;

    [Header("Starting Points")]
    public int numActionPointsStart;
    public int numMemoryUnitsStart;
    public int numTagsStart;



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


    public void DrawCards(int numberOfCards)
	{
        Card[] drawnCards = new Card[numberOfCards];
		for (int i = 0; i < numberOfCards; i++)
		{
            Card drawnCard = PlayArea.instance.DrawCardFromDeck(PlayerNR.Runner);
            drawnCards[i] = drawnCard;
            drawnCard.FlipCard();
		}

        PlayerNR.Runner.AddCardsToHand(drawnCards);

	}

    public bool TryDrawNextCard()
	{
        if (CanDrawAnotherCard())
		{
            Action_DrawNextCard();
            return true;
		}
        return false;
	}

    public bool CanAffordAction(int actionIndex)
	{
        int costOfAction = PlayArea.instance.CostOfAction(PlayerNR.Runner, actionIndex);
        return PlayerNR.Runner.CanAffordAction(costOfAction);
	}

    public bool CanAffordCost(int cost)
	{
        return PlayerNR.Runner.CanAffordCost(cost);
	}

    public bool CanDrawAnotherCard()
	{
        //!PlayArea.instance.IsHandSizeMaxed(GameManager.instance.runner);
        return CanAffordAction(RUNNER_DRAW_CARD);
    }


    public bool CanGainCredit()
	{
        return CanAffordAction(RUNNER_GAIN_CREDIT);
	}

    public void TryGainCredit()
	{
        if (CanGainCredit())
		{
            Action_GainCredit();
        }
	}

    public bool CanInstallCard(IInstallable installableCard)
	{
        return CanAffordAction(RUNNER_INSTALL) && installableCard.CanInstall();
	}

    public bool TryInstallCard(IInstallable installableCard)
	{
        if (CanInstallCard(installableCard))
		{
            Action_InstallCard(installableCard);
            Card card = (Card)installableCard;
            PayCostOfCard(card);
            UseMemorySpaceOfCard(card);
            return true;
		}
        return false;
	}

    public bool CanActivateEvent(IActivateable activateableCard)
	{
        return CanAffordAction(RUNNER_EVENT) && activateableCard.CanActivate();
	}

    public bool TryActivateEvent(IActivateable activateableCard)
	{
        if (CanActivateEvent(activateableCard))
		{
            Action_ActivateEvent(activateableCard);
            Card card = (Card)activateableCard;
            PayCostOfCard(card);
            SendCardToDiscard(card);
        }
        return true;
	}

    public bool CanRemoveTag()
	{
        return CanAffordAction(RUNNER_REMOVE_TAG)
            && CanAffordCost(2)
            && PlayerNR.Runner.Tags > 0;
    }

    public bool TryRemoveTag()
	{
        if (CanRemoveTag())
		{
            Action_RemoveTag();
            PayCost(2);

            return true;
		}
        return false;
	}


    #region Actions
    void Action_DrawNextCard()
	{
        int costOfAction = PlayArea.instance.CostOfAction(PlayerNR.Runner, RUNNER_DRAW_CARD);
        PlayerNR.Runner.ActionPointsUsed(costOfAction);
        DrawNextCard();
    }

    void Action_GainCredit()
	{
        int costOfAction = PlayArea.instance.CostOfAction(PlayerNR.Runner, RUNNER_GAIN_CREDIT);
        PlayerNR.Runner.ActionPointsUsed(costOfAction);
        GainCredit();
    }

    void Action_InstallCard(IInstallable installableCard)
	{
        int costOfAction = PlayArea.instance.CostOfAction(PlayerNR.Runner, RUNNER_INSTALL);
        PlayerNR.Runner.ActionPointsUsed(costOfAction);
        InstallCard(installableCard);
    }

    void Action_ActivateEvent(IActivateable activateableCard)
	{
        int costOfAction = PlayArea.instance.CostOfAction(PlayerNR.Runner, RUNNER_EVENT);
        PlayerNR.Runner.ActionPointsUsed(costOfAction);
        ActivateCard(activateableCard);
    }

    void Action_RemoveTag()
	{
        int costOfAction = PlayArea.instance.CostOfAction(PlayerNR.Runner, RUNNER_REMOVE_TAG);
        PlayerNR.Runner.ActionPointsUsed(costOfAction);
        RemoveTag();
    }

    void PayCost(int cost)
	{
        PlayerNR.Runner.Credits -= cost;
	}

    void PayCostOfCard(Card card)
	{
        int balance = PlayerNR.Runner.Credits;
        if (card.cardCost.TryBuyCard(ref balance))
        {
            PlayerNR.Runner.Credits = balance;
        }
        else print("Did not pay for card!!!");
	}

	void UseMemorySpaceOfCard(Card card)
	{
		int spaceAvailable = PlayerNR.Runner.MemoryUnitsAvailable;
		if (card.cardCost.TryUseMemorySpace(ref spaceAvailable))
		{
            PlayerNR.Runner.MemoryUnitsAvailable = spaceAvailable;
        }
        else print("Did not use Memory Space for card!!!");
    }

    #endregion

    void DrawNextCard()
	{
        DrawCards(1);
    }

    void GainCredit()
	{
        PlayerNR.Runner.AddCredits(1);
	}

    void InstallCard(IInstallable installableCard)
	{
        RunnerRIG.instance.InstallCard(installableCard);
        OnCardInstalled?.Invoke((Card)installableCard, true);
    }

    void ActivateCard(IActivateable activateableCard)
	{
        activateableCard.ActivateCard();
        OnCardActivated?.Invoke((Card)activateableCard);
    }

    void RemoveTag()
	{
        PlayerNR.Runner.Tags--;
	}


    void SendCardToDiscard(Card card)
	{
        PlayArea.instance.SendCardToDiscard(PlayerNR.Runner, card);
	}


    public void StartTurn(PlayerNR playerTurn)
	{
        if (playerTurn.IsRunner())
		{
            playerTurn.ActionPoints = numActionPointsStart;
            playerTurn.MemoryUnitsTotal = numMemoryUnitsStart;
            playerTurn.MemoryUnitsAvailable = numMemoryUnitsStart;
            playerTurn.Tags = numTagsStart;
        }
        else
		{
            playerTurn.ActionPoints = 3;
		}
	}





}
