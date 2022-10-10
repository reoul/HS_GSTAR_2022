using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
    [SerializeField] private List<Transform> _cardCreatePos;
    [SerializeField] private GameObject _cardPrefabs;
    [SerializeField] private Transform _cardParent;

    public void CreateCards(IUseCard useCard)
    {
        int index = 0;
        foreach (string code in useCard.GetCardCodes())
        {
            GameObject cardObj = Instantiate(_cardPrefabs, _cardParent);
            cardObj.transform.position = _cardCreatePos[index++].position;
            cardObj.GetComponent<CardSettor>().SetCard(code);
        }
    }
}
