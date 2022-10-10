using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
    [SerializeField] private List<Transform> _cardCreatePosList;
    [SerializeField] private GameObject _cardPrefabs;
    [SerializeField] private Transform _cardParent;

    /// <summary> 카드를 생성한다 </summary>
    /// <param name="useCard">IUseCard 인터페이스</param>
    /// <returns>생성된 카드 개수</returns>
    public int CreateCards(IUseCard useCard)
    {
        int cnt = 0;
        foreach (string code in useCard.GetCardCodes())
        {
            GameObject cardObj = Instantiate(_cardPrefabs, _cardParent);
            cardObj.transform.position = _cardCreatePosList[cnt++].position;
            cardObj.GetComponent<CardSettor>().SetCard(code);
        }

        return cnt;
    }
}
