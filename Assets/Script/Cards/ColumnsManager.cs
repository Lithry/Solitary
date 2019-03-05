using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnsManager : MonoBehaviour {
    public GameObject[] col;
    public DeckManager deck;
    private List<Column> columns;
	
	void Start () {
        columns = new List<Column>();
        foreach (GameObject column in col)
        {
            columns.Add(column.GetComponent<Column>());
        }
        PrepareCards();
    }

    #region Check Card & Add
    public bool ContainCardAsLast(CardDisplay card, out int idx) {
        for (int i = 0; i < columns.Count; i++)
        {
            if (columns[i].ContainCardAsLast(card))
            {
                idx = i;
                return true;
            }
        }
        idx = -1;
        return false;
    }

    public void AddCardByIndex(CardDisplay card, int idx) {
        if (idx > -1 && idx < columns.Count) {
            columns[idx].Add(card);
            //card.SetCurrentPosition();
        }
    }

    public void RemoveCard(CardDisplay toRemove) {
        for (int i = 0; i < columns.Count; i++) {
            if (columns[i].Contains(toRemove)) {
                columns[i].Remove(toRemove);

                if (columns[i].Count() > 0)
                    columns[i].GetCard(columns[i].Count() - 1).FlipCardUp();

                return;
            }
        }

    }
    #endregion

    private void PrepareCards() {
        int k = 1;
        for (int i = 0; i < columns.Count; i++) {
            for (int j = 0; j < k; j++) {
                CardDisplay card = deck.GetLastCardOfDeck();
                card.RemoveFromContainer();
                columns[i].Add(card);
            }
            k++;
        }

        for (int i = 0; i < columns.Count; i++) {
            columns[i].GetCard(columns[i].Count() - 1).FlipCardUp();
        }

        PosisionateCards();
    }

    public void PosisionateCards() {
        for (int i = 0; i < columns.Count; i++)
        {
            columns[i].PosisionateCards();
        }
    }
}
