using UnityEngine;
using UnityEngine.UI;

public class ActionTracker : MonoBehaviour
{
    public PlayerSide playerSide;
    PlayerNR myPlayer;
    Button[] actionPointButtons;


	private void Awake()
	{
        myPlayer = playerSide == PlayerSide.Runner ? PlayerNR.Runner : PlayerNR.Corporation;
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
