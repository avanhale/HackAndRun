using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public ActionsReferenceCard runnerActionsReferenceCard;
    public DiscardPile runnerDiscardPile;


    private void Awake()
    {
        instance = this;
        Canvas.ForceUpdateCanvases();
    }


    // Start is called before the first frame update
    void Start()
    {
        Canvas.ForceUpdateCanvases();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            foreach (var rt in GetComponentsInChildren<RectTransform>())
            {
                rt.ForceUpdateRectTransforms();
            }
        }
    }


    public void SetCardsToSpots(PlayerNR player)
    {
        if (player.IsRunner())
        {
            player.identity.transform.SetParent(runnerIdentityT, false);
            runnerDeck.SetCardsToDeck(player.deckCards);
            //runnerHand.AddCardsToHand(player.)
        }
    }

    public void AddCardsToHand(PlayerNR player, Card[] cards)
    {
        if (player.IsRunner())
        {
            runnerHand.AddCardsToHand(cards);
        }
    }

    public Card DrawCardFromDeck(PlayerNR player)
    {
        if (player.IsRunner())
        {
            Card drawnCard = null;
            if (runnerDeck.DrawNextCard(ref drawnCard))
            {
                return drawnCard;
            }
        }

        return null;
    }

    public int CostOfAction(PlayerNR player, int actionIndex)
    {
        if (player.IsRunner())
        {
            return runnerActionsReferenceCard.CostOfAction(actionIndex);
        }
        return -123;
    }

    public void SendCardToDiscard(PlayerNR player, Card card)
	{
        if (player.IsRunner())
		{
            runnerDiscardPile.AddCardToDiscard(card);
        }

    }

}
