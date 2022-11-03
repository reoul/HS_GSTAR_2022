using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _contextText;
    [SerializeField] private Image _gem;
    [SerializeField] private ItemRatingType _rank;
    [SerializeField] private Sprite[] _gemSprite;

    void Start()
    {
        SetCard(_nameText.text, _contextText.text, _rank);
    }

    void Update()
    {

    }

    public void SetCard(string inputName, string InputContext, ItemRatingType rank){

        _nameText.text = inputName;
        _contextText.text = InputContext;
        _rank = rank;
        _gem.sprite = _gemSprite[(int)rank];
    }

    public string GetCardName() { 
        return _nameText.text;
    }
    public string GetContext() { 
        return _contextText.text;
    }
    public ItemRatingType GetRank() { 
        return _rank;
    }
}
