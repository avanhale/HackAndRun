using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOperator : MonoBehaviour
{
    public static RunOperator instance;
    public Card_Ice currentIceEncountered;
    public bool isRunning;

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

    public void MakeRun(ServerColumn serverColumn)
	{
        isRunning = true;
        if (serverColumn.GetNextIce(ref currentIceEncountered))
		{
            int iceStrength = currentIceEncountered.strength;
            //int 








		}
	}


}
