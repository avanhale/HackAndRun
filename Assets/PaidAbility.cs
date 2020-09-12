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

	CardFunction cardFunction;
    [HideInInspector]
    public int myAbilityIndex;

	//public string AbilityText;
	private void Awake()
	{
        cardFunction = GetComponentInParent<CardFunction>();
        button = GetComponent<Button>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    }



}
