using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardReferences : MonoBehaviour
{
    [Header("Card References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI cardTypeText, cardSubTypeText;
    public TextMeshProUGUI costText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCardTitle(string cardTitle)
	{
        titleText.text = cardTitle;
	}

    public void UpdateCardTypes(string cardType, string cardSubType)
	{
        cardTypeText.text = cardType;
        cardSubTypeText.text = cardSubType;
    }

    public void UpdateCardCost(int cardCost)
	{
        costText.text = cardCost.ToString();
    }


}
