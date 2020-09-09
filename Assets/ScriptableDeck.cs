using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableDeck", menuName = "ScriptableObjects/ScriptableDeck", order = 1)]
public class ScriptableDeck : ScriptableObject
{
    public PlayerSide playerSide;
    public Card_Identity identityCard;
    [Space]
    public Card[] deckCards;
}
