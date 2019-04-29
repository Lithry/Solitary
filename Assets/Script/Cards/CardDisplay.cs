using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDisplay : MonoBehaviour {
    #region Variables
    public Card card;
	private SpriteRenderer image;
    private Sprite cardImage;
    private Sprite cardBack;
    private Vector3 oldPosition;
    private bool selected;
    private bool check;
    private bool fliped;
    private BoxCollider coll;
    #endregion

    void Awake () {
        image = GetComponent<SpriteRenderer>();
        selected = false;
        check = false;
        fliped = false;
        coll = GetComponent<BoxCollider>();
	}

    private void Update() {
        if (selected)
            MoveCard(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f)));

        RightClic();
        LeftClic();
    }

    #region Movement
    private void RightClic() {
        GameObject click = InputManager.instance.RightClic();
        if (click != null && click.transform.name == gameObject.transform.name)
        {
            selected = true;
        }
        if (selected && InputManager.instance.RightClicEnded())
        {
            selected = false;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit))
            {
                if (hit.collider.tag == "SuitDeck" && transform.childCount == 0)
                {
                    SuitDeck sDeck = hit.collider.gameObject.GetComponent<SuitDeck>();
                    if (sDeck.Check(this))
                    {
                        RemoveFromContainer();
                        sDeck.AddCard(this);
                        SuitDeckManager.instance.PositionateCard();
                    }
                    else
                    {
                        Return();
                    }
                }
                else if (hit.collider.tag == "Card")
                {
                    CardDisplay cardHit = hit.collider.gameObject.GetComponent<CardDisplay>();
                    int idx;
                    if (ColumnsManager.instance.ContainCardAsLast(cardHit, out idx))
                    {
                        if (cardHit.card.value - 1 == card.value && cardHit.card.black != card.black)
                        {
                            MoveToColumn(idx);
                            ColumnsManager.instance.PosisionateCards();
                        }
                        else
                        {
                            Return();
                        }
                    }
                    else if (transform.childCount == 0 && SuitDeckManager.instance.Contain(cardHit, out idx) && cardHit.card.value == card.value - 1 && cardHit.card.type == card.type)
                    {
                        MoveToSuitDeck(idx);
                        SuitDeckManager.instance.PositionateCard(idx);
                    }
                    else
                    {
                        Return();
                    }
                }
                else if (hit.collider.tag == "Column")
                {
                    Column column = hit.collider.gameObject.GetComponent<Column>();
                    if (card.value == 13)
                    {
                        MoveToColumn(column);
                        column.PosisionateCards();
                    }
                    else
                    {
                        Return();
                    }
                }
                else
                    Return();
            }
            else
            {
                Return();
            }
        }
    }

    private void LeftClic() {
        GameObject click = InputManager.instance.LeftClic();
        if (click != null && click.transform.name == gameObject.transform.name)
        {
            check = true;
        }
        if (check && InputManager.instance.LeftClicEnded())
        {
            check = false;
            int idx;
            if (transform.childCount == 0 && SuitDeckManager.instance.CheckCard(this, out idx))
            {
                RemoveFromContainer();
                SuitDeckManager.instance.AddCard(this, idx);
                SuitDeckManager.instance.PositionateCard(idx);
            }
        }
    }

    private void MoveCard(Vector3 position) {
        transform.position = position;
    }
    #endregion

    #region Position
    public void SetCurrentPosition() {
        oldPosition = transform.position;
    }

    private void Return() {
        transform.position = oldPosition;
    }

    public void MoveCardToPosition(Vector3 position) {
        transform.position = position;
        SetCurrentPosition();
    }

    private void MoveToColumn(int indexFromCol) {
        RemoveFromContainer();
        ColumnsManager.instance.AddCardByIndex(this, indexFromCol);
        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.GetComponent<CardDisplay>().MoveToColumn(indexFromCol);
    }

    private void MoveToColumn(Column column) {
        RemoveFromContainer();
        column.Add(this);
        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.GetComponent<CardDisplay>().MoveToColumn(column);
    }

    private void MoveToSuitDeck(int index) {
        RemoveFromContainer();
        SuitDeckManager.instance.AddCard(this, index);
    }

    public void RemoveFromContainer()
    {
        DeckManager.instance.RemoveCard(this);
        ColumnsManager.instance.RemoveCard(this);
        SuitDeckManager.instance.RemoveCard(this);
    }
    #endregion

    #region Card Side & Enable
    public void FlipCard() {
        if (!fliped) {
            image.sprite = card.img;
            fliped = true;
        }
        else {
            image.sprite = card.imgBack;
            fliped = false;
        }
    }

    public void FlipCardUp() {
        if (!fliped) {
            image.sprite = card.img;
            fliped = true;
            coll.enabled = true;
        }
    }

    public void FlipCardDown() {
        if (fliped) {
            image.sprite = card.img;
            fliped = false;
            coll.enabled = false;
        }
    }

    public void ColliderActive(bool active) {
        coll.enabled = active;
    }

    public bool IsFliped() {
        return fliped;
    }
    #endregion

    public void LoadCard(Card cardData) {
        if (cardData != null) {
            card = cardData;
            image.sprite = cardData.imgBack;
            fliped = false;
            transform.name = card.type.ToString() + "_" + card.value.ToString();
        }
        else {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Cards/PlayingCards");
            image.sprite = sprites.Single(s => s.name == "NULL");
            transform.name = "null";
        }
    }
}
