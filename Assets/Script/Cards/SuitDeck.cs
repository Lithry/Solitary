using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitDeck : MonoBehaviour {
	private List<CardDisplay> deck;

	// Use this for initialization
	void Start () {
		deck = new List<CardDisplay>();
	}

    public bool Check(CardDisplay card) {
        if (deck.Count > 0) {
            if ((card.card.value -1 == deck[deck.Count - 1].card.value) && (card.card.type == deck[deck.Count - 1].card.type)) {
                card.RemoveFromContainer();
                deck.Add(card);
                card.transform.position = transform.position;
                PosisionateCards();
                return true;
            }
            else
                return false;
        }
        else if (card.card.value == 1) {
            card.RemoveFromContainer();
            deck.Add(card);
            card.transform.position = transform.position;
            PosisionateCards();
            return true;
        }
        else
            return false;
    }

    private void PosisionateCards() {
        for (int i = 0; i < deck.Count; i++)
        {
            if (i != 0)
            {
                deck[i].MoveCardToPosition(new Vector3(transform.position.x - (float)i / 500f, transform.position.y + (float)i / 500f, transform.position.z - (float)i / 100f));
            }
            else
            {
                deck[i].MoveCardToPosition(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.05f));
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
