using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewer : MonoBehaviour
{
	public static CardViewer instance;
    public Card_Program program;
    public Card_Ice ice;

	public Transform cardsT;

	public bool isPinning;
	public GameObject pinGO;
	public int currentViewIndex = -1;

	public struct CardPair
	{
		public Card realCard, viewCard;
	}
	public Dictionary<int, CardPair> cardPairDict = new Dictionary<int, CardPair>();

	private void Awake()
	{
		instance = this;
		PinCard(null);
	}

	private void OnEnable()
	{
		CardChooser.OnHoveredOverCard += CardChooser_OnHoveredOverCard;
		CardChooser.OnCardPinned += PinCard;
	}

	private void OnDisable()
	{
		CardChooser.OnHoveredOverCard -= CardChooser_OnHoveredOverCard;
		CardChooser.OnCardPinned -= PinCard;
	}

	private void CardChooser_OnHoveredOverCard(Card card)
	{
		if (isPinning) return;

		if (card == null)
		{
			HideAllCards();
		}
		else
		{
			ViewCard(card.viewIndex);
		}
	}

	void Start()
    {
		StartCoroutine(CardPopulateDelay());
    }

	void ParentCard(Card card)
	{
		card.FlipCardUp(true);
		SelectorNR selector = card.GetComponentInChildren<SelectorNR>();
		if (selector)
		{
			selector.highlighter.gameObject.SetActive(false);
			selector.gameObject.SetActive(false);
		}
		card.ParentCardTo(cardsT);
		RectTransform rt = card.GetComponent<RectTransform>();
		rt.anchorMin = rt.anchorMax = rt.pivot = Vector2.one;
		card.transform.localScale = Vector3.one * 3;
	}

	void ViewCard(int viewIndex)
	{
		HideAllCards();
		CardPair pair;
		if (cardPairDict.TryGetValue(viewIndex, out pair))
		{
			pair.viewCard.gameObject.SetActive(true);
			currentViewIndex = viewIndex;
		}
		else
		{
			currentViewIndex = -1;
		}
	}

	void HideAllCards()
	{
		foreach (var kvp in cardPairDict)
		{
			kvp.Value.viewCard.gameObject.SetActive(false);
		}
	}

	IEnumerator CardPopulateDelay()
	{
		yield return new WaitForSeconds(1);
		int viewIndex = 0;

		// Deck cards
		List<Card> allDeckCards = new List<Card>();
		for (int i = 0; i < PlayerNR.Runner.deckCards.Length; i++)
		{
			allDeckCards.Add(PlayerNR.Runner.deckCards[i]);
		}
		for (int i = 0; i < PlayerNR.Corporation.deckCards.Length; i++)
		{
			allDeckCards.Add(PlayerNR.Corporation.deckCards[i]);
		}

		for (int i = 0; i < allDeckCards.Count; i++)
		{
			Card deckCard = allDeckCards[i];
			deckCard.viewIndex = viewIndex;

			Card viewCard = Instantiate(deckCard);
			cardPairDict[viewIndex] = new CardPair() { realCard = deckCard, viewCard = viewCard };
			ParentCard(viewCard);
			viewIndex++;
		}

		// Identity cards
		for (int i = 0; i < 2; i++)
		{
			Card identityCard = i == 0 ? PlayerNR.Runner.identity : PlayerNR.Corporation.identity;
			identityCard.viewIndex = viewIndex;

			Card viewCard = Instantiate(identityCard);
			cardPairDict[viewIndex] = new CardPair() { realCard = identityCard, viewCard = viewCard };
			ParentCard(viewCard);
			viewIndex++;
		}

		yield return new WaitForSeconds(0.25f);
		PaidAbility[] allPaidAbilities = cardsT.GetComponentsInChildren<PaidAbility>(true);
		for (int i = 0; i < allPaidAbilities.Length; i++)
		{
			allPaidAbilities[i].SetClickable(true);
			//CardPair cardPair;
			//if (cardPairDict.TryGetValue(allPaidAbilities[i].cardFunction.card.viewIndex, out cardPair))
			//{
			//	allPaidAbilities[i].SetCardFunctions(cardPair.realCard.cardFunction, cardPair.viewCard.cardFunction);
			//}
		}

	}

	public int targetIndex;
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.G))
		{
			CardPair pair;
			if(cardPairDict.TryGetValue(targetIndex, out pair))
			{
				Debug.Log(pair.realCard.name, pair.realCard);
				//ViewCard(targetIndex);
				//PinCard(targetIndex);
			}


		}
    }

	Card GetCard(int viewIndex, bool real)
	{
		CardPair cardPair;
		if (cardPairDict.TryGetValue(viewIndex, out cardPair))
		{
			return real ? cardPair.realCard : cardPair.viewCard;
		}
		return null;
	}


	public void PinCard(Card card)
	{
		if (card)
		{
			isPinning = true;
			pinGO.SetActive(true);
			GetCard(currentViewIndex, true)?.Pinned(false);
			ViewCard(card.viewIndex);
			GetCard(currentViewIndex, true).Pinned(true);
		}
		else
		{
			isPinning = false;
			pinGO.SetActive(false);
			HideAllCards();
			GetCard(currentViewIndex, true)?.Pinned(false);
		}
	}






}
