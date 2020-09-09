using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardManager : MonoBehaviour
{
    public static PlayCardManager instance;
    public int numCardsToDrawFirstHand;

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


    public void DrawFirstHands()
	{
        Card[] runnerCards = new Card[numCardsToDrawFirstHand];
		for (int i = 0; i < numCardsToDrawFirstHand; i++)
		{
            Card drawnCard = PlayArea.instance.DrawCardFromDeck(GameManager.instance.runner);
            runnerCards[i] = drawnCard;
		}

        PlayArea.instance.AddCardsToHand(GameManager.instance.runner, runnerCards);


	}

    public bool TryDrawNextCard()
	{
        if (CanAffordAction(1) && CanDrawAnotherCard())
		{
            Action_DrawNextCard();
            return true;
		}
        return false;
	}

    public bool CanAffordAction(int actionIndex)
	{
        int costOfAction = PlayArea.instance.CostOfAction(GameManager.instance.runner, actionIndex);
        return PlayArea.instance.CanAffordAction(GameManager.instance.runner, costOfAction);
	}

    public bool CanDrawAnotherCard()
	{
        return !PlayArea.instance.IsHandSizeMaxed(GameManager.instance.runner);
    }

	#region Actions
	public void Action_DrawNextCard()
	{
        int actionIndex = 1;
        int costOfAction = PlayArea.instance.CostOfAction(GameManager.instance.runner, actionIndex);
        PlayArea.instance.SpendActionPoints(GameManager.instance.runner, costOfAction);
        DrawNextCard();
    }


	#endregion

	public void DrawNextCard()
	{
        Card drawnCard = PlayArea.instance.DrawCardFromDeck(GameManager.instance.runner);
        PlayArea.instance.AddCardsToHand(GameManager.instance.runner, new Card[1] { drawnCard });
    }


    public void StartTurn(GameManager.Player playerTurn)
	{
        PlayArea.instance.ResetActionTracker(playerTurn);
	}





}
