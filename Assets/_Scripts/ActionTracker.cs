using UnityEngine;
using UnityEngine.UI;

public class ActionTracker : PlayArea_Spot
{
    Button[] actionPointButtons;

	protected override void Awake()
	{
		base.Awake();
        GetActionPointButtons();
	}

	private void OnEnable()
	{
		myPlayer.OnActionPointsChanged += MyPlayer_OnActionPointsChanged;
	}
	private void OnDisable()
	{
		myPlayer.OnActionPointsChanged -= MyPlayer_OnActionPointsChanged;
	}

	private void MyPlayer_OnActionPointsChanged()
	{
		UpdateActionPointButtons();
	}

    void UpdateActionPointButtons()
	{
		for (int i = 0; i < actionPointButtons.Length; i++)
		{
            actionPointButtons[i].interactable = false;
		}
		for (int i = 0; i < myPlayer.ActionPoints; i++)
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
