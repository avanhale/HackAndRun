using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionsReferenceCard : MonoBehaviour
{
    public bool isShowing;
    RectTransform rectTransform;
    public int startingX, showingX, transitionTime;

    [Header("Action Point Costs")]
    public int[] actionPointCosts;

	private void Awake()
	{
        rectTransform = GetComponent<RectTransform>();
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // actionIndex: non-zero-based 1,2,3,4,5, etc.
    public int CostOfAction(int actionIndex)
	{
        if (actionIndex <= actionPointCosts.Length)
		{
            return actionPointCosts[actionIndex - 1];
		}
        return -123;
	}

    public void SwitchShowing()
	{
        isShowing = !isShowing;
        UpdateDisplay();

    }

    public void UpdateDisplay()
	{

        if (isShowing)
		{
            rectTransform.DOAnchorPosX(showingX, transitionTime);
		}
        else
		{
            rectTransform.DOAnchorPosX(startingX, transitionTime);
        }
    }


    public void CardButton_Clicked()
	{
        SwitchShowing();
	}




}
