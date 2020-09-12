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


    public void DrawCards(PlayerNR player, int numberOfCards)
	{
        Card[] drawnCards = new Card[numberOfCards];
		for (int i = 0; i < numberOfCards; i++)
		{
            Card drawnCard = PlayArea.instance.DrawCardFromDeck(player);
            drawnCards[i] = drawnCard;
            drawnCard.FlipCard();
		}

        Hand hand = PlayArea.instance.HandNR(player);
        hand.AddCardsToHand(drawnCards);
	}

    public bool TryDrawNextCard()
	{
        if (CanDrawAnotherCard())
		{
            Action_DrawNextCard(GameManager.CurrentTurnPlayer);
            return true;
		}
        return false;
	}

    public bool CanAffordAction(PlayerNR player, int actionIndex)
	{
        int costOfAction = PlayArea.instance.CostOfAction(player, actionIndex);
        return player.CanAffordAction(costOfAction);
	}

    public bool CanAffordCost(int cost)
	{
        return PlayerNR.Runner.CanAffordCost(cost);
	}

    public bool CanDrawAnotherCard()
	{
        //!PlayArea.instance.IsHandSizeMaxed(GameManager.instance.runner);
        return CanAffordAction(GameManager.CurrentTurnPlayer, RUNNER_DRAW_CARD);
    }


    public bool CanGainCredit()
	{
        return CanAffordAction(GameManager.CurrentTurnPlayer, RUNNER_GAIN_CREDIT);
	}

    public void TryGainCredit()
	{
        if (CanGainCredit())
		{
            Action_GainCredit(GameManager.CurrentTurnPlayer);
        }
	}

    public bool CanInstallCard(IInstallable installableCard)
	{
        int actionIndex = GameManager.CurrentTurnPlayer.IsRunner() ? RUNNER_INSTALL : CORP_INSTALL;
        return CanAffordAction(GameManager.CurrentTurnPlayer, actionIndex) && installableCard.CanInstall();
	}

    public bool TryInstallCard(IInstallable installableCard)
	{
        if (CanInstallCard(installableCard))
		{
            Card card = (Card)installableCard;
            if (GameManager.CurrentTurnPlayer.IsRunner())
            {
                Action_InstallCard_Runner(installableCard);
                PayCostOfCard(GameManager.CurrentTurnPlayer, card);
                UseMemorySpaceOfCard(card);
            }
            else
			{
                Action_InstallCard_Corp(installableCard);
            }
                return true;
		}
        return false;
	}

    public bool CanActivateEvent(IActivateable activateableCard)
	{
        return CanAffordAction(GameManager.CurrentTurnPlayer, RUNNER_EVENT) && activateableCard.CanActivate();
	}

    public bool TryActivateEvent(IActivateable activateableCard)
	{
        if (CanActivateEvent(activateableCard))
		{
            Action_ActivateEvent(GameManager.CurrentTurnPlayer, activateableCard);
            Card card = (Card)activateableCard;
            PayCostOfCard(GameManager.CurrentTurnPlayer, card);
            SendCardToDiscard(card);
        }
        return true;
	}

    public bool CanRemoveTag()
	{
        return CanAffordAction(PlayerNR.Runner, RUNNER_REMOVE_TAG)
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

    public void TryActivatePaidAbility(PaidAbility paidAbility)
	{
        if (paidAbility.currency == Currency.Credits)
		{
            if (CanAffordCost(paidAbility.payAmount))
			{
                paidAbility.ActivateAbility();
                PayCost(paidAbility.payAmount);
			}
		}
        else if (paidAbility.currency == Currency.Clicks)
		{
            if (GameManager.CurrentTurnPlayer.CanAffordAction(paidAbility.payAmount))
			{
                // Activate card on clicks
			}
		}
	}


    #region Actions
    void Action_DrawNextCard(PlayerNR player)
	{
        int costOfAction = PlayArea.instance.CostOfAction(PlayerNR.Runner, player.IsRunner() ? RUNNER_DRAW_CARD : CORP_DRAW_CARD);
        player.ActionPointsUsed(costOfAction);
        DrawNextCard(player);
    }

    void Action_GainCredit(PlayerNR player)
	{
        int costOfAction = PlayArea.instance.CostOfAction(player, player.IsRunner() ? RUNNER_GAIN_CREDIT : CORP_GAIN_CREDIT);
        player.ActionPointsUsed(costOfAction);
        GainCredit(GameManager.CurrentTurnPlayer);
    }

    void Action_InstallCard_Runner(IInstallable installableCard)
	{
        int costOfAction = PlayArea.instance.CostOfAction(PlayerNR.Runner, RUNNER_INSTALL);
        PlayerNR.Runner.ActionPointsUsed(costOfAction);
        InstallCard_Runner(installableCard);
    }

    void Action_InstallCard_Corp(IInstallable installableCard)
    {
        int costOfAction = PlayArea.instance.CostOfAction(PlayerNR.Corporation, CORP_INSTALL);
        PlayerNR.Corporation.ActionPointsUsed(costOfAction);
        InstallCard_Corporation(installableCard);
    }

    void Action_ActivateEvent(PlayerNR player, IActivateable activateableCard)
	{
        int costOfAction = PlayArea.instance.CostOfAction(player, player.IsRunner() ? RUNNER_EVENT : CORP_OPERATION);
        player.ActionPointsUsed(costOfAction);
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

    void PayCostOfCard(PlayerNR player, Card card)
	{
        int balance = player.Credits;
        if (card.cardCost.TryBuyCard(ref balance))
        {
            player.Credits = balance;
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

    void DrawNextCard(PlayerNR player)
	{
        DrawCards(player, 1);
    }

    void GainCredit(PlayerNR player)
	{
        player.AddCredits(1);
	}

    void InstallCard_Runner(IInstallable installableCard)
	{
        RunnerRIG.instance.InstallCard(installableCard);
        OnCardInstalled?.Invoke((Card)installableCard, true);
    }

    void InstallCard_Corporation(IInstallable installableCard)
	{
        ServerSpace.instance.InstallCard(installableCard);
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
        PlayArea.instance.DiscardNR(GameManager.CurrentTurnPlayer).AddCardToDiscard(card);
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
            playerTurn.ActionPoints = numActionPointsStart;
		}
	}





}
