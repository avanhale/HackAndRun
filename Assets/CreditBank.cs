using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditBank : MonoBehaviour, ISelectableNR
{


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public bool CanHighlight()
	{
		return true;
	}

	public bool CanSelect()
	{
		return PlayCardManager.instance.CanGainCredit();
	}

	public void Highlighted()
	{
	}

	public void Selected()
	{
		PlayCardManager.instance.TryGainCredit();
	}

}
