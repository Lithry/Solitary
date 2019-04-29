using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour {
    public static DeckManager instance;
    public const string path = "Cards";
    public GameObject cardPrefab;
    public GameObject deckPosition;
    public GameObject playCardPosition;
    private List<CardDisplay> cards;
    private List<CardDisplay> cardsToUse;

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        List<Card> toInstantiate = Shuffle(CardLoader.LoadCards(path));
        cards = new List<CardDisplay>();
        cardsToUse = new List<CardDisplay>();

        foreach (Card card in toInstantiate) {
            CardDisplay c = Instantiate(cardPrefab).GetComponent<CardDisplay>();
            c.LoadCard(card);
            c.ColliderActive(false);
            cards.Add(c);
        }

        PosisionateCards();
    }

    void Update() {
        GameObject clic = InputManager.instance.RightClic();
        if (clic != null && clic.tag == "Deck") {
            GetNextCards();
        }
    }

    private void GetNextCards() {
        if (cards.Count > 0) {
            if (cardsToUse.Count > 0) {
                cardsToUse[cardsToUse.Count - 1].ColliderActive(false);
            }

            for (int i = 0; i < 3; i++) {
                if (cards.Count > 0) {
                    cards[cards.Count - 1].FlipCard();
                    cardsToUse.Add(cards[cards.Count - 1]);
                    cards.Remove(cards[cards.Count - 1]);
                    
                }
            }

            cardsToUse[cardsToUse.Count - 1].ColliderActive(true);
        }
        else {
            if (cardsToUse.Count > 0) {
                cardsToUse[cardsToUse.Count - 1].ColliderActive(false);
            }

            while (cardsToUse.Count > 0) {
                cardsToUse[cardsToUse.Count - 1].FlipCard();
                cards.Add(cardsToUse[cardsToUse.Count - 1]);
                cardsToUse.Remove(cardsToUse[cardsToUse.Count - 1]);
            }
        }

        PosisionateCards();
    }

    private void PosisionateCards() {
        for (int i = 0; i < cardsToUse.Count; i++) {
            if (i != 0) {
                cardsToUse[i].MoveCardToPosition(new Vector3(playCardPosition.transform.position.x - (float)i / 500f, playCardPosition.transform.position.y + (float)i / 500f, playCardPosition.transform.position.z - (float)i / 100f));
            }
            else {
                cardsToUse[i].MoveCardToPosition(new Vector3(playCardPosition.transform.position.x, playCardPosition.transform.position.y, playCardPosition.transform.position.z));
            }
            cardsToUse[i].SetCurrentPosition();
        }
        for (int i = 0; i < cards.Count; i++) {
            if (i != 0) {
                cards[i].MoveCardToPosition(new Vector3(deckPosition.transform.position.x - (float)i / 500f, deckPosition.transform.position.y + (float)i / 500f, deckPosition.transform.position.z - (float)i / 100f));
            }
            else {
                cards[i].MoveCardToPosition(new Vector3(deckPosition.transform.position.x, deckPosition.transform.position.y, deckPosition.transform.position.z));
            }
            cards[i].SetCurrentPosition();
        }
    }

    public void RemoveCard(CardDisplay toRemove) {
        if (cards.Contains(toRemove)) {
            cards.Remove(toRemove);
        }
        else if (cardsToUse.Contains(toRemove)) {
            cardsToUse.Remove(toRemove);
            if (cardsToUse.Count > 0) {
                cardsToUse[cardsToUse.Count - 1].ColliderActive(true);
            }
        }
    }

    public CardDisplay GetLastCardOfDeck()
    {
        if (cards.Count > 0)
            return cards[cards.Count - 1];
        return null;
    }

    // Fisher_Yates_CardDeck_Shuffle
    public static List<Card> Shuffle(List<Card> aList) {
        System.Random _random = new System.Random();

        Card myGO;

        int n = aList.Count;
        for (int i = 0; i < n; i++) {
            // NextDouble returns a random number between 0 and 1.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }

        return aList;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (deckPosition) {
            Gizmos.DrawSphere(deckPosition.transform.position, 0.25f);
        }
        
        Gizmos.color = Color.blue;
        if (playCardPosition) {
            Gizmos.DrawSphere(playCardPosition.transform.position, 0.25f);
        }
    }
}
