using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnsManager : MonoBehaviour {
    public static ColumnsManager instance;
    public GameObject[] col;
    public GameObject columnPrefab;
    private Vector3 pos;
    public DeckManager deck;
    private List<Column> columns;
	
	void Start () {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        pos = new Vector3(-4.7f, 2.0f, -0.05f);
        columns = new List<Column>();
        for (int i = 0; i < GameConfig.columnsNum; i++)
        {
            columns.Add(Instantiate(columnPrefab, pos, new Quaternion(0, 0, 0, 1), transform).GetComponent<Column>());
            pos.x += 2.0f;
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

    public void AddCardByIndex(CardDisplay card, int idx, int childCount) {
        if (idx > -1 && idx < columns.Count) {
            columns[idx].Add(card, childCount);
        }
    }

    public void RemoveCard(CardDisplay toRemove, int childCount) {
        for (int i = 0; i < columns.Count; i++) {
            if (columns[i].Contains(toRemove)) {
                columns[i].Remove(toRemove, childCount);

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
                columns[i].Add(card, 0);
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
