using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerRIG : PlayArea_Spot
{
    public static RunnerRIG instance;
    public Transform programsT, hardwaresT, resourcesT;

    public List<Card_Program> programCards;
    public List<Card_Hardware> hardwareCards;
    public List<Card_Resource> resourceCards;

	protected override void Awake()
	{
		base.Awake();
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
        programCard.MoveCardTo(programsT);
        programCards.Add(programCard);
    }

    public void AddCardToHardwares(Card_Hardware hardwareCard)
    {
        hardwareCard.MoveCardTo(hardwaresT);
        hardwareCards.Add(hardwareCard);
    }


    public void AddCardToResources(Card_Resource resourceCard)
    {
        resourceCard.MoveCardTo(resourcesT);
        resourceCards.Add(resourceCard);
    }


    public bool IsCardInRIG(Card card)
	{
        return programCards.Contains(card as Card_Program)
            || hardwareCards.Contains(card as Card_Hardware);
	}


	public override void RemoveCard(Card card)
	{
		base.RemoveCard(card);

        if (card.IsCardType(CardType.Program))
		{
            Card_Program program = card as Card_Program;
            if (programCards.Contains(program)) programCards.Remove(program);
		}
        else if (card.IsCardType(CardType.Hardware))
        {
            Card_Hardware hardware = card as Card_Hardware;
            if (hardwareCards.Contains(hardware)) hardwareCards.Remove(hardware);
        }
        else if (card.IsCardType(CardType.Resource))
        {
            Card_Resource resource = card as Card_Resource;
            if (resourceCards.Contains(resource)) resourceCards.Remove(resource);
        }
    }





}
