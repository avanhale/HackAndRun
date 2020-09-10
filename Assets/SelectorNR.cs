using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectorNR : MonoBehaviour
{
    public Image highlighter;
    public UnityEvent onHighlighted, onClicked;
    public ISelectableNR selectable;
    Color ogColor;

	private void Awake()
	{
        selectable = GetComponentInParent<ISelectableNR>();
        ogColor = highlighter.color;
    }

	void Start()
    {
        highlighter.enabled = false;
        Highlight(false);
    }

    public void Highlight(bool highlight = true)
	{
        if (selectable != null && selectable.CanHighlight())
		{
            highlighter.enabled = highlight;
            selectable.Highlighted();
            onHighlighted?.Invoke();
            if (selectable.CanSelect())
			{
                highlighter.color = ogColor;
            }
            else
			{
                highlighter.color = Color.black;
			}
		}
    }


    public void Click()
	{
        if (selectable != null && selectable.CanSelect())
		{
            selectable.Selected();
            onClicked?.Invoke();
            Highlight(true);
        }
	}


}
