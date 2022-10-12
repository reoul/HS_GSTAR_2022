using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
    [SerializeField] private List<Transform> _cardCreatePosList;
    [SerializeField] private GameObject _cardPrefabs;
    [SerializeField] private Transform _cardParent;
    [SerializeField] private List<Card> _cards;

    /// <summary> 카드를 생성한다 </summary>
    /// <param name="useCard">IUseCard 인터페이스</param>
    /// <param name="createDelay">카드간의 생성 딜레이 시간</param>
    /// <returns>생성된 카드 개수</returns>
    public int CreateCards(IUseCard useCard, float createDelay = 0)
    {
        List<string> codes = useCard.GetCardCodes();
        StartCoroutine(CreateCardsCoroutine(codes, createDelay));
        return codes.Count;
    }

    /// <summary> 카드 생성 코루틴 </summary>
    /// <param name="codes">생성 카드 코드 List</param>
    /// <param name="createDelay">카드간의 생성 딜레이 시간</param>
    /// <returns></returns>
    private IEnumerator CreateCardsCoroutine(List<string> codes, float createDelay = 0)
    {
        List<GameObject> createCardObjs = new List<GameObject>();
        for (int i = 0; i < codes.Count; ++i) // 생성 가능한 카드 코드를 가지고 미리 카드 생성 후 Active 끄기
        {
            GameObject cardObj = Instantiate(_cardPrefabs, _cardParent);
            cardObj.transform.position = _cardCreatePosList[i].position;
            cardObj.GetComponent<CardSettor>().SetCard(codes[i]);
            cardObj.SetActive(false);
            _cards.Add(cardObj.GetComponent<Card>());
            createCardObjs.Add(cardObj);
        }

        WaitForSeconds waitDelay = new WaitForSeconds(createDelay);
        foreach (GameObject cardObj in createCardObjs) // 딜레이 시간동안 하나씩 키기
        {
            cardObj.SetActive(true);
            yield return waitDelay;
        }
    }

    /// <summary> 현재 생성된 모든 카드 제거 </summary>
    public void RemoveAllCard()
    {
        StopAllCoroutines();
        foreach (Card card in _cards)
        {
            Destroy(card.gameObject);
        }

        _cards.Clear();
    }
}
