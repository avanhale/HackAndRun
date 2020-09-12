using UnityEngine;

public abstract class PlayArea_Spot : MonoBehaviour
{
    public PlayerSide playerSide;
	[HideInInspector]
    public PlayerNR myPlayer;

	protected virtual void Awake()
	{
		SetMyPlayer(playerSide == PlayerSide.Runner ? PlayerNR.Runner : PlayerNR.Corporation);
	}

	public void SetMyPlayer(PlayerNR player)
	{
        myPlayer = player;
	}

	public virtual void RemoveCard(Card card)
	{

	}
}
