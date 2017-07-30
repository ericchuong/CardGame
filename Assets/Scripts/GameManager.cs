using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Sprite[] cardFace;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text royalCounter;

    private bool _init = false;
    private int _counter = 0;   // counter for number of royals clicked
    public int columnCounter = 1; // counter for column
    public int rowCounter = 4; // counter for row
    public bool replace; // true to replace face up cards after a royal is drawn

    private List<int> initUsedCardValues = new List<int>(); // list of values of cards used during initialization
    private List<int> usedCards = new List<int>(); // list of cards used
    private List<int> usedCardValues = new List<int>(); // list of values of cards used

    // Update is called once per frame
    void Update()
    {
        if (!_init)
            initializeCards(); // initialize every card when the game starts
        if (Input.GetMouseButtonUp(0)) // every time a card is clicked
            checkCards();
        if (replace)
            replaceCards();
    }

    void initializeCards()
    {
        int choice;
        for (int i = 0; i < cards.Length; i++)
        // initializing cards to a value
        {
            choice = Random.Range(0, 52); // each card is chosen at random
            while (initUsedCardValues.Contains(choice)) // make sure there are no duplicate cards in the deck
            {
                choice = Random.Range(0, 52);
            }
            cards[i].GetComponent<Card>().cardValue = choice;
            cards[i].GetComponent<Card>().initialized = true;
            initUsedCardValues.Add(choice);
        }

        foreach (GameObject c in cards)
        {
            c.GetComponent<Card>().setupGraphics();
            _init = true;
        }
    }

    public Sprite getCardBack()
    {
        return cardBack;
    }

    public Sprite getCardFace(int i)
    {
        return cardFace[i];
    }

    void checkCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<Card>().state == 1 && !usedCardValues.Contains(cards[i].GetComponent<Card>().cardValue))
            {
                // if card is face up (state = 1) and hasn't been used before
                if (cards[i].GetComponent<Card>().column == columnCounter)
                {
                    // if card is in the next column
                    if (cards[i].GetComponent<Card>().row == (rowCounter - 1))
                    // if the card is in an adjacent row
                    {
                        // if Card is not already included in the Used Cards List, add it
                        usedCards.Add(i);

                        // add Card Value to the Used Card Values List
                        usedCardValues.Add(cards[i].GetComponent<Card>().cardValue);
                        columnCounter++;

                        //when card is flipped, check if it is a royal
                        replace = cardComparison(usedCards);
                        rowCounter--;
                    }
                    else if (cards[i].GetComponent<Card>().row == (rowCounter + 1))
                    // if the card is in an adjacent row
                    {
                        // if Card is not already included in the Used Cards List, add it
                        usedCards.Add(i);

                        // add Card Value to the Used Card Values List
                        usedCardValues.Add(cards[i].GetComponent<Card>().cardValue);
                        columnCounter++;

                        //when card is flipped, check if it is a royal
                        replace = cardComparison(usedCards);
                        rowCounter++;
                    }
                    else
                        {
                            //if the selected card is not in the appropirate row, flip it back over
                            cards[i].GetComponent<Card>().flipCardBack();
                            replace = false;
                        }
                }
                else
                {
                    //if the selected card is not in the appropirate column, flip it back over
                    cards[i].GetComponent<Card>().flipCardBack();
                    replace = false;
                }
            }
        }
    }

    bool cardComparison(List<int> c)
    {
        if (cards[c[c.Count - 1]].GetComponent<Card>().cardValue > 35)
        {
            // CARD IS A ROYAL
            _counter++;
            royalCounter.text = "Royals Drawn " + _counter;

            if (_counter == 16)
            {
                // Game Over
                royalCounter.text = "Game Over";
                //SceneManager.LoadScene("MainMenu");
            }
            return true;
        }
        else
            return false;
    }

    void replaceCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<Card>().state == 1)
            {
                cards[i].GetComponent<Card>().initialized = false;
                reinitializeCard(cards[i]);
                cards[i].GetComponent<Card>().flipCardBack();

                //reset column to start
                columnCounter = 1;
                //reset row to start
                rowCounter = 4;
            }
        }
    }

    void reinitializeCard(GameObject card)
    {
        int choice;
        int tempCounter = 0;
        // reinitializing cards to a value

        choice = Random.Range(0, 52); // each card is chosen at random
        while (initUsedCardValues.Contains(choice)) // make sure there are no duplicate cards in the deck
        {
            choice = Random.Range(0, 52);
            tempCounter++;
            if (tempCounter > 1000)
            {
                //Game Over
                //SceneManager.LoadScene("MainMenu");
                royalCounter.text = "Game Over";
                goto here;
            }
        }
        card.GetComponent<Card>().cardValue = choice;
        card.GetComponent<Card>().initialized = true;
        initUsedCardValues.Add(choice);
        card.GetComponent<Card>().setupGraphics();
        here:;
    }
}