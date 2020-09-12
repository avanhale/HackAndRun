using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChooser : MonoBehaviour
{
    public SelectorNR hoveredSelector;
    public delegate void HoveredOverCard(Card card);
    public static event HoveredOverCard OnHoveredOverCard;

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
                    if (hoveredSelector) hoveredSelector.Hover(false);
                    selector.Hover();

                    Card card = selector.GetComponentInParent<Card>();
                    if (card) OnHoveredOverCard?.Invoke(card);

                    hoveredSelector = selector;
                }
            }
		}
        else
		{
            if (hoveredSelector) hoveredSelector.Hover(false);
            hoveredSelector = null;
		}

	}













}
