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
    /// <param name="ownerObj">카드 소유주 게임오브젝트</param>
    /// <param name="createDelay">카드간의 생성 딜레이 시간</param>
    /// <returns>생성된 카드 개수</returns>
    public int CreateCards(IUseCard useCard, GameObject ownerObj, float createDelay = 0)
    {
        List<string> codes = useCard.GetCardCodes();
        
#if USE_DEBUG
        foreach (string code in codes) // 유효한 카드 코드를 입력했는지 검증
        {
            Type cardType = Type.GetType($"{code},Assembly-CSharp");
            Debug.AssertFormat(cardType != null, "{0} 은 존재하지 않는 카드 타입입니다", code);
        }
#endif
        
        StartCoroutine(CreateCardsCoroutine(codes, ownerObj, createDelay));
        return codes.Count;
    }
    
    /// <summary> 카드를 생성한다 (여러 적 카드 생성 할 때) </summary>
    /// <param name="useCards">IUseCard 인터페이스 리스트</param>
    /// <param name="ownerObjs">카드 소유주 게임오브젝트</param>
    /// <param name="createDelay">카드간의 생성 딜레이 시간</param>
    /// <returns>생성된 카드 개수</returns>
    public int CreateCards(List<IUseCard> useCards, List<GameObject> ownerObjs, float createDelay = 0)
    {
        Debug.Assert(useCards.Count == ownerObjs.Count);

        List<string> codes = new List<string>();
        List<GameObject> ownerObjectList = new List<GameObject>();
        
        for (int i = 0; i < useCards.Count; ++i)
        {
            foreach (string cardCode in useCards[i].GetCardCodes())
            {
                codes.Add(cardCode);
                ownerObjectList.Add(ownerObjs[i]);
            }
        }
        
#if USE_DEBUG
        foreach (string code in codes) // 유효한 카드 코드를 입력했는지 검증
        {
            Type cardType = Type.GetType($"{code},Assembly-CSharp");
            Debug.AssertFormat(cardType != null, "{0} 은 존재하지 않는 카드 타입입니다", code);
        }
#endif
        
        StartCoroutine(CreateCardsCoroutine(codes, ownerObjectList, createDelay));
        return codes.Count;
    }

    /// <summary> 카드 생성 코루틴 </summary>
    /// <param name="codes">생성 카드 코드 List</param>
    /// <param name="ownerObj">카드 소유주 게임오브젝트</param>
    /// <param name="createDelay">카드간의 생성 딜레이 시간</param>
    /// <returns></returns>
    private IEnumerator CreateCardsCoroutine(List<string> codes, GameObject ownerObj, float createDelay = 0)
    {
        List<GameObject> createCardObjs = new List<GameObject>();
        PositionSorterInfo sorterInfo = new PositionSorterInfo()
        {
            CardWidth = 225,
            CardHeight = 330,
            CardPaddingX = 20,
            CardPaddingY = 20,
            DicePaddingX = 0
        };

        int colummMaxCount = codes.Count > 5 ? codes.Count / 2 + codes.Count % 2 : codes.Count;
        List <Vector3> posList = PositionSorter.SortCard(codes.Count, colummMaxCount, sorterInfo);
        
        for (int i = 0; i < codes.Count; ++i) // 생성 가능한 카드 코드를 가지고 미리 카드 생성 후 Active 끄기
        {
            GameObject cardObj = Instantiate(_cardPrefabs, _cardParent);
            cardObj.transform.localPosition = posList[i];
            cardObj.GetComponent<CardSettor>().SetCard(codes[i]);
            cardObj.SetActive(false);
            cardObj.GetComponent<Card>().OwnerObj = ownerObj;
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
    
    /// <summary> 카드 생성 코루틴 (여러 적 카드 생성해야 할 때) </summary>
    /// <param name="codes">생성 카드 코드 List</param>
    /// <param name="ownerObjs">카드 소유주 게임오브젝트 List</param>
    /// <param name="createDelay">카드간의 생성 딜레이 시간</param>
    /// <returns></returns>
    private IEnumerator CreateCardsCoroutine(List<string> codes, List<GameObject> ownerObjs, float createDelay = 0)
    {
        Debug.Assert(codes.Count == ownerObjs.Count);
        
        List<GameObject> createCardObjs = new List<GameObject>();
        PositionSorterInfo sorterInfo = new PositionSorterInfo()
        {
            CardWidth = 225,
            CardHeight = 330,
            CardPaddingX = 20,
            CardPaddingY = 20,
            DicePaddingX = 0
        };

        int colummMaxCount = codes.Count > 5 ? codes.Count / 2 + codes.Count % 2 : codes.Count;
        List<Vector3> posList = PositionSorter.SortCard(codes.Count, colummMaxCount, sorterInfo);

        for (int i = 0; i < codes.Count; ++i) // 생성 가능한 카드 코드를 가지고 미리 카드 생성 후 Active 끄기
        {
            GameObject cardObj = Instantiate(_cardPrefabs, _cardParent);
            cardObj.transform.localPosition = posList[i];
            cardObj.GetComponent<CardSettor>().SetCard(codes[i]);
            cardObj.SetActive(false);
            cardObj.GetComponent<Card>().OwnerObj = ownerObjs[i];
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

    /// <summary> 특정 카드 제거 </summary>
    /// <param name="card">제거할 카드</param>
    public void RemoveCard(Card card)
    {
        Debug.Assert(_cards.Remove(card));
        if (_cards.Count == 0)
        {
            BattleManager.Instance.NextTurn();
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
