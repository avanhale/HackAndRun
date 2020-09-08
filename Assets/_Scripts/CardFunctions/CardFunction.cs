using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardFunction : MonoBehaviour
{



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


}
