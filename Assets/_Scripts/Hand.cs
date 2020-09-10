using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hand : MonoBehaviour
{
    public PlayerSide playerSide;
    PlayerNR myPlayer;
    public Transform cardsParentT;
    int maximumHandSize = 5;
    public float cardScaleInHand;

    [Header("Show Toggler")]
    bool isShowing = true;
    RectTransform rt;
    public int showYValue, hideYValue;
    public float transitionTime;

	private void Awake()
	{
        myPlayer = playerSide == PlayerSide.Runner ? PlayerNR.Runner : PlayerNR.Corporation;
        rt = GetComponent<RectTransform>();
    }

    public void AddCardsToHand(params Card[] cards)
	{
		for (int i = 0; i < cards.Length; i++)
		{
            cards[i].ParentCardTo(cardsParentT);
            ScaleCard(cards[i]);
		}
	}

    void ScaleCard(Card card, bool scale = true)
	{
        float scaleFactor = scale ? cardScaleInHand : 1;
        card.transform.localScale = Vector3.one * scaleFactor;
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Space)) ToggleShowing();
	}


	void ToggleShowing()
	{
        isShowing = !isShowing;
        rt.DOAnchorPosY(isShowing ? showYValue : hideYValue, transitionTime);

    }

}
