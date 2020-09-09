using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChooser : MonoBehaviour
{
    public SelectorNR hoveredSelector;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
            if (hoveredSelector)
			{
                //hoveredSelector.FlipCard();
                hoveredSelector.Click();
			}
		}
    }


	private void FixedUpdate()
	{
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

        if (hit)
		{
            //Card card = hit.collider.GetComponent<Card>();
            SelectorNR selector = hit.collider.GetComponent<SelectorNR>();
            if (selector)
            {
                if (selector != hoveredSelector)
                {
                    Debug.Log("selector - " + hit.collider.name, hit.collider);
                    if (hoveredSelector) hoveredSelector.Highlight(false);
                    selector.Highlight();
                    hoveredSelector = selector;
                }
            }
		}
        else
		{
            if (hoveredSelector) hoveredSelector.Highlight(false);
            hoveredSelector = null;
		}

	}













}
