using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerRIG : MonoBehaviour
{
    public static RunnerRIG instance;
    public Transform programsT, hardwaresT, resourcesT;

    public List<Card_Program> programCards;
    public List<Card_Hardware> hardwareCards;
    public List<Card_Resource> resourceCards;

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

	public void InstallCard(IInstallable installableCard)
	{
        Card card = (Card)installableCard;
        if (card.cardType == CardType.Program)
		{
            Card_Program programCard = (Card_Program)installableCard;
            AddCardToPrograms(programCard);
            //memoryUsed++;
        }
        else if (card.cardType == CardType.Hardware)
        {
            Card_Hardware hardwareCard = (Card_Hardware)installableCard;
            AddCardToHardwares(hardwareCard);
        }
        else if (card.cardType == CardType.Resource)
        {
            Card_Resource resourceCard = (Card_Resource)installableCard;
            AddCardToResources(resourceCard);
        }
    }



    public void AddCardToPrograms(Card_Program programCard)
	{
        programCard.transform.SetParent(programsT, false);
    }

    public void AddCardToHardwares(Card_Hardware hardwareCard)
    {
        hardwareCard.transform.SetParent(hardwaresT, false);
    }


    public void AddCardToResources(Card_Resource resourceCard)
    {
        resourceCard.transform.SetParent(resourcesT, false);
    }





}
