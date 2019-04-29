using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitDeck : MonoBehaviour {
	private List<CardDisplay> deck;
    private BoxCollider coll;

	void Start () {
		deck = new List<CardDisplay>();
        coll = GetComponent<BoxCollider>();
	}

    public bool Check(CardDisplay card) {
        if (deck.Count > 0) {
            if ((card.card.value -1 == deck[deck.Count - 1].card.value) && (card.card.type == deck[deck.Count - 1].card.type)) {
                return true;
            }
            else
                return false;
        }
        else if (card.card.value == 1) {
            return true;
        }
        else
            return false;
    }

    public bool Contain(CardDisplay card) {
        if (deck.Contains(card))
            return true;

        return false;
    }

    public void AddCard(CardDisplay card) {
        deck.Add(card);
        PosisionateCards();

        if (deck.Count > 0)
            coll.enabled = false;
    }

    public void RemoveCard(CardDisplay card) {
        if (deck.Contains(card)) {
            deck.Remove(card);
        }
        if (deck.Count == 0)
            coll.enabled = true;
    }

    public void PosisionateCards() {
        for (int i = 0; i < deck.Count; i++) {
            deck[i].transform.parent = null;

            if (i != 0)
            {
                deck[i].MoveCardToPosition(new Vector3(transform.position.x - (float)i / 500f, transform.position.y + (float)i / 500f, (transform.position.z - 0.05f) - (float)i / 100f));
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
