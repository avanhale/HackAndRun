using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerColumn : PlayArea_Spot
{
    public PlayArea_Spot serverProtected;
    public Transform iceColumnT;
    public Card_Ice currentIceEncountered;
    public List<Card_Ice> iceInColumn = new List<Card_Ice>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeRun()
	{

        if (GetNextIce(ref currentIceEncountered))
		{
            print("Got next Ice...");
		}

    }

    int currentIceIndex = -1;
    public bool GetNextIce(ref Card_Ice card)
	{
        currentIceIndex++;
        if (currentIceIndex >= iceInColumn.Count) return false;
        else
		{
            card = iceInColumn[currentIceIndex];
            return true;
		}

    }


    public void InstallIce(Card_Ice card)
	{
        card.MoveCardTo(iceColumnT);
        card.transform.localEulerAngles = Vector3.forward * 180;
        iceInColumn.Add(card);

    }


	public override void RemoveCard(Card card)
	{
		base.RemoveCard(card);
        iceInColumn.Remove(card as Card_Ice);
	}



}
