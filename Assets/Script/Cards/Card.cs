using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

public enum CardType{
    nullTypeCard = 0,
	Spade,
	Heart,
	Club,
	Diamond
}

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
public class Card : ScriptableObject {
	public CardType type;
    public int value;
    public Sprite img;
    public Sprite imgBack;
    public bool black;
}
