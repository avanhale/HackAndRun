using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{

    //public enum CardType { Identity, Operation, Agenda, Ice, }


    public string cardTitle;

    [Header("Card References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI cardTypeText, cardSubTypeText;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	//private void OnValidate()
	//{
 //       if (titleText) titleText.text = cardTitle;
	//}


}
