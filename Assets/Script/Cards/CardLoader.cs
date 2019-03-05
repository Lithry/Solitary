using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Linq;

public class CardLoader
{
    public static List<Card> LoadCards(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);
        XmlDocument XmlDoc = new XmlDocument();
        XmlDoc.LoadXml(_xml.text);
        Sprite[] sprites = Resources.LoadAll<Sprite>("Cards/PlayingCards");
        List<Card> cards = new List<Card>();
        
        foreach (XmlNode x in XmlDoc)
        {
            if (x.Name == "Cards")
            {
                foreach (XmlNode y in x.ChildNodes)
                {
                    if (y.Name == "Card")
                    {
                        Card card = ScriptableObject.CreateInstance<Card>();
                        foreach (XmlNode z in y.ChildNodes)
                        {
                            if (z.Name == "Type")
                            {
                                switch (z.InnerText)
                                {
                                    case "Club":
                                        card.type = CardType.Club;
                                        card.black = true;
                                        break;
                                    case "Diamond":
                                        card.type = CardType.Diamond;
                                        card.black = false;
                                        break;
                                    case "Heart":
                                        card.type = CardType.Heart;
                                        card.black = false;
                                        break;
                                    case "Spade":
                                        card.type = CardType.Spade;
                                        card.black = true;
                                        break;
                                    default:
                                        card.type = CardType.nullTypeCard;
                                        card.black = true;
                                        break;
                                }
                            }
                            if (z.Name == "Value")
                            {
                                card.value = int.Parse(z.InnerText);
                            }
                        }
                        
                        if (card.type != CardType.nullTypeCard && card.value != 0)
                        {
                            card.img = sprites.Single(s => s.name == card.type.ToString() + "_" + card.value.ToString());
                            card.imgBack = sprites.Single(s => s.name == "Back");
                        }
                        else{
                            card.img = sprites.Single(s => s.name == "ERROR");
                            card.imgBack = sprites.Single(s => s.name == "ERROR");
                        }

                        cards.Add(card);
                    }
                }
            }
        }
        return cards;
    }
}