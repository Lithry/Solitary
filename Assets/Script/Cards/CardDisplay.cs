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
    private DeckManager deck;
    private ColumnsManager columns;
    private bool selected;
    private bool fliped;
    private BoxCollider coll;
    #endregion

    void Awake () {
        image = GetComponent<SpriteRenderer>();
        selected = false;
        fliped = false;
        coll = GetComponent<BoxCollider>();
	}

    private void Update() {
        if (selected)
            MoveCard(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f)));

        GameObject click = InputManager.instance.Clic();
        if (click != null && click.transform.name == gameObject.transform.name){
            selected = true;
        }
        if (selected && InputManager.instance.ClicEnded()){
            selected = false;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit)) {
                if (hit.collider.tag == "SuitDeck") {
                    SuitDeck sDeck = hit.collider.gameObject.GetComponent<SuitDeck>();
                    if (!sDeck.Check(this)) {
                        Return();
                    }
                }
                else if (hit.collider.tag == "Card") {
                    CardDisplay cardHit = hit.collider.gameObject.GetComponent<CardDisplay>();
                    int idx;
                    if (columns.ContainCardAsLast(cardHit, out idx)) {
                        if (cardHit.card.value - 1 == card.value && cardHit.card.black != card.black) {
                            MoveToColumn(idx);
                            columns.PosisionateCards();
                        }
                        else {
                            Return();
                        }
                    }
                    else {
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
            else {
                Return();
            }
        }
    }

    #region Movement
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
        columns.AddCardByIndex(this, indexFromCol);
        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.GetComponent<CardDisplay>().MoveToColumn(indexFromCol);
    }

    private void MoveToColumn(Column column)
    {
        RemoveFromContainer();
        column.Add(this);
        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.GetComponent<CardDisplay>().MoveToColumn(column);
    }

    public void RemoveFromContainer()
    {
        deck.RemoveCard(this);
        columns.RemoveCard(this);
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

    public void LoadCard(Card cardData, DeckManager deckManager, ColumnsManager col) {
        if (cardData != null) {
            card = cardData;
            image.sprite = cardData.imgBack;
            fliped = false;
            transform.name = card.type.ToString() + "_" + card.value.ToString();
            deck = deckManager;
            columns = col;
        }
        else {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Cards/PlayingCards");
            image.sprite = sprites.Single(s => s.name == "NULL");
            transform.name = "null";
            deck = deckManager;
            columns = col;
        }
    }
}
