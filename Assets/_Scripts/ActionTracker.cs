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
        numActionPoints = actionPointButtons.Length;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool HasEnoughActionPoints(int numActions)
	{
        return numActionPoints >= numActions;
	}

    public bool TryUseActionPoints(int numActions)
	{
        if (HasEnoughActionPoints(numActions))
		{
            ActionPointsUsed(numActions);
            return true;
        }
        return false;
	}

    public void ResetActionPoints()
	{
        numActionPoints = actionPointButtons.Length;
        UpdateActionPointButtons();
    }

    public void ActionPointsUsed(int numActions)
	{
        numActionPoints -= numActions;
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
