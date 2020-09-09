using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerRIG : MonoBehaviour
{
    public static RunnerRIG instance;
    public Transform programsT, hardwaresT, resourcesT;
    public int memoryUsed;

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

	public void InstallCard(Card card)
	{
        
        if (card.GetType() == typeof(Card_Hardware))
        {
            Card_Hardware c = (Card_Hardware) card;
            AddCardToHardwares(c);
        }

    }



    public void AddCardToPrograms(Card_Program programCard)
	{

	}

    public void AddCardToHardwares(Card_Hardware hardwareCard)
    {
        hardwareCard.transform.SetParent(hardwaresT, false);
    }


    public void AddCardToResources(Card_Resource resourceCard)
    {

    }





}
