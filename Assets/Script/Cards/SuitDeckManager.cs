using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitDeckManager : MonoBehaviour {
    public static SuitDeckManager instance;
    public GameObject suitDeckPrefab;
    private Vector3 sPosition;
    private List<SuitDeck> decks;

	void Awake () {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        decks = new List<SuitDeck>();
        sPosition = new Vector3(1.5f, 4.55f, -0.05f);
        Quaternion rot = new Quaternion(0, 0, 0, 1);

        for (int i = 0; i < 4; i++)
        {
            GameObject d = Instantiate(suitDeckPrefab, sPosition, rot);
            decks.Add(d.GetComponent<SuitDeck>());
            sPosition.x += 1.6f;
        }
	}

    public bool Contain(CardDisplay card, out int idx) {
        for (int i = 0; i < decks.Count; i++)
        {
            if (decks[i].Contain(card)) {
                idx = i;
                return true;
            }
        }
        idx = -1;
        return false;
    }

    public void RemoveCard(CardDisplay card) {
        for (int i = 0; i < decks.Count; i++)
        {
            decks[i].RemoveCard(card);
        }
    }

    public void AddCard(CardDisplay card, int index) {
        decks[index].AddCard(card);
    }

    public void PositionateCard() {
        for (int i = 0; i < decks.Count; i++) {
            decks[i].PosisionateCards();
        }
    }

    public void PositionateCard(int index) {
        decks[index].PosisionateCards();
    }
}
