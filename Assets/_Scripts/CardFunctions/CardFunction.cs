using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardFunction : MonoBehaviour
{
	protected Card card;

	protected virtual void Awake()
	{
		card = GetComponent<Card>();
	}


	protected virtual void OnEnable()
	{
		SubscribeToConditions();
	}

	protected virtual void OnDisable()
	{
		UnSubscribeToConditions();
	}



	protected virtual void SubscribeToConditions()
	{

	}

	protected virtual void UnSubscribeToConditions()
	{

	}

	public virtual void ActivateFunction()
	{

	}


}
