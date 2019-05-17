using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour {
    private List<CardDisplay> column;
    private BoxCollider boxCollider;
    private float spaceBetwen;


    void Awake () {
        column = new List<CardDisplay>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        spaceBetwen = 2.5f;
    }
	
	public void Add(CardDisplay card, int childCount) {
        if (column.Count == 0)
            boxCollider.enabled = false;

        column.Add(card);

        SetSpaceBetwen();

        if (childCount < 1)
            PosisionateCards();
    }

    public void Remove(CardDisplay card, int childCount) {
        column.Remove(card);

        if (column.Count == 0)
            boxCollider.enabled = true;

        SetSpaceBetwen();

        if (childCount < 1)
            PosisionateCards();
    }

    private void SetSpaceBetwen() {
        if (column.Count < 11)
            spaceBetwen = 2.5f;
        else
            spaceBetwen = 3.0f;
    }

    public CardDisplay GetCard(int idx) {
        return column[idx];
    }

    public int Count() {
        return column.Count;
    }

    public bool Contains(CardDisplay card) {
        return column.Contains(card);
    }

    public bool ContainCardAsLast(CardDisplay card) {
        if (column.Count > 0 && column[column.Count - 1] == card)
            return true;
        return false;
    }

    public void PosisionateCards() {
        if (column.Count > 0)
            column[0].transform.parent = null;

        for (int i = 0; i < column.Count; i++) {
            column[i].MoveCardToPosition(new Vector3(transform.position.x, transform.position.y - (float)i / spaceBetwen, transform.position.z - (float)i / 100f));
            column[i].SetCurrentPosition();

            if (column[i].IsFliped() && column.Count > 1 && i < column.Count - 1)
                column[i + 1].gameObject.transform.parent = column[i].gameObject.transform;
        }
    }
}
