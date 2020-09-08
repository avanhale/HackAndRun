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
        if (player.identity.IsOwnedByPlayerSide(PlayerSide.Runner))
		{
            player.identity.transform.SetParent(runnerIdentityT, false);
            runnerDeck.SetCardsToDeck(player.deckCards);
		}
	}





}
