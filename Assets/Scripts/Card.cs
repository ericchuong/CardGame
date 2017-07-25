using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public static bool DO_NOT = false;

    [SerializeField]
    private int _state;
    [SerializeField]
    private int _cardValue;
    [SerializeField]
    private bool _initialized = false;
    [SerializeField]
    private int _column;

    private Sprite _cardBack;
    public Sprite _cardFace;

    private GameObject _manager;

    private void Start()
    {
        _state = 0; // state 0 = face down
        _manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void setupGraphics()
    {
        _cardBack = _manager.GetComponent<GameManager>().getCardBack();
        _cardFace = _manager.GetComponent<GameManager>().getCardFace(_cardValue);
    }

    public void flipCardFace()
    {
        if (_state == 0)
        {
            // if card is showing its back, flip to show face
            GetComponent<Image>().sprite = _cardFace;
            _state = 1;
        }
    }

    public void flipCardBack()
    {
        if (_state == 1)
        {
            // if card is showing its face, flip to show back
            GetComponent<Image>().sprite = _cardBack;
            _state = 0;
        }
    }

    public int cardValue
    {
        get { return _cardValue; }
        set { _cardValue = value; }
    }

    public int state
    {
        get { return _state; }
        set { _state = value; }
    }

    public bool initialized
    {
        get { return _initialized; }
        set { _initialized = value; }
    }

    public int column
    {
        get { return _column; }
        set { _column = value; }
    }
}
