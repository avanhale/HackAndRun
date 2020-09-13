using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaidAbility : MonoBehaviour
{
    public int payAmount;
    public Currency currency;
    Button button;
    public GameObject clickableGO;

	public CardFunction cardFunction;
    [HideInInspector]
    public int myAbilityIndex;

    public CardFunction viewCardFunction;

	//public string AbilityText;
	private void Awake()
	{
        cardFunction = GetComponentInParent<CardFunction>();
        button = GetComponent<Button>();
        SetClickable(false);
    }

    void Start()
    {
        
    }


    public void SetClickable(bool clickable)
	{
        clickableGO.SetActive(clickable);
	}

    public void ActivateOnCredits(int playerCredits)
	{
        button.interactable = currency == Currency.Credits && playerCredits >= payAmount;
    }


    public void Button_Clicked()
	{
        PlayCardManager.instance.TryActivatePaidAbility(this);
    }

    public void ActivateAbility()
	{
        cardFunction.ActivatePaidAbility(myAbilityIndex);
        //if (viewCardFunction) viewCardFunction.ActivatePaidAbility(myAbilityIndex);
    }


    public void SetCardFunctions(CardFunction _cardFunction, CardFunction _viewCardFunction)
	{
        cardFunction = _cardFunction;
        viewCardFunction = _viewCardFunction;
	}



}
