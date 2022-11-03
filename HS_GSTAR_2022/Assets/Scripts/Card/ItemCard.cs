using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _contextText;
    [SerializeField] private int _rank;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetCard(string inputName, string InputContext, int rank){

        _nameText.text = inputName;
        _contextText.text = InputContext;
    }

    public string GetCardName() { 
        return _nameText.text;
    }
    public string GetContext() { 
        return _contextText.text;
    }
}
