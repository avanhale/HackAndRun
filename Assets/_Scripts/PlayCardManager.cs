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


    #endregion

    void DrawNextCard()
	{
        DrawCards(1);
    }

    void GainCredit()
	{
        PlayerNR.Runner.AddCredits(1);
	}


    public void StartTurn(PlayerNR playerTurn)
	{
        if (playerTurn.IsRunner())
		{
            playerTurn.ActionPoints = 4;
            playerTurn.MemoryUnitsTotal = 4;
        }
        else
		{
            playerTurn.ActionPoints = 3;
		}
	}





}
