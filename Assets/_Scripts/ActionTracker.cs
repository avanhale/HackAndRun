using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionTracker : MonoBehaviour
{
    Button[] actionPointButtons;
    public int numActionPoints;


	private void Awake()
	{
        GetActionPointButtons();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetActionPoints()
	{
        numActionPoints = actionPointButtons.Length;
        UpdateActionPointButtons();
    }

    public void ActionPointUsed()
	{
        numActionPoints--;
        UpdateActionPointButtons();
	}

    void UpdateActionPointButtons()
	{
		for (int i = 0; i < actionPointButtons.Length; i++)
		{
            actionPointButtons[i].interactable = false;
		}
		for (int i = 0; i < numActionPoints; i++)
		{
            int targetIndex = actionPointButtons.Length - (i + 1);
            if (targetIndex >= 0 && targetIndex < actionPointButtons.Length)
                actionPointButtons[targetIndex].interactable = true;
		}
	}



    void GetActionPointButtons()
	{
        actionPointButtons = GetComponentsInChildren<Button>();
    }


}
