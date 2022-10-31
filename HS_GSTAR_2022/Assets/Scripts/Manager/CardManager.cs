using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
    [SerializeField] private GameObject _cardPrefabs;
    [SerializeField] private Transform _cardParent;
    [SerializeField] private List<Card> _cards;

    /// <summary> 카드를 생성한다 </summary>
    /// <param name="useCard">IUseCard 인터페이스</param>
    /// <param name="ownerObj">카드 소유주 게임오브젝트</param>
    /// <param name="createDelay">카드간의 생성 딜레이 시간</param>
    /// <returns>생성된 카드 개수</returns>
    public int CreateCards(IUseCard useCard, GameObject ownerObj, Vector3 scale, float createDelay = 0)
    {
        List<string> codes = useCard.GetCardCodes();

#if USE_DEBUG
        foreach (string code in codes) // 유효한 카드 코드를 입력했는지 검증
        {
            Type cardType = Type.GetType($"{code},Assembly-CSharp");
            Debug.AssertFormat(cardType != null, "{0} 은 존재하지 않는 카드 타입입니다", code);
        }
#endif

        StartCoroutine(CreateCardsCoroutine(codes, ownerObj, scale, createDelay));
        return codes.Count;
    }

    /// <summary> 카드를 생성한다 </summary>
    /// <param name="cardList">생성할 카드 코드 리스트</param>
    /// <param name="ownerObj">카드 소유주 게임오브젝트</param>
    /// <param name="createDelay">카드간의 생성 딜레이 시간</param>
    /// <returns>생성된 카드 개수</returns>
    public int CreateCards(List<string> cardList, GameObject ownerObj, Vector3 scale, float createDelay = 0)
    {
#if USE_DEBUG
        foreach (string code in cardList) // 유효한 카드 코드를 입력했는지 검증
        {
            Type cardType = Type.GetType($"{code},Assembly-CSharp");
            Debug.AssertFormat(cardType != null, "{0} 은 존재하지 않는 카드 타입입니다", code);
        }
#endif

        StartCoroutine(CreateCardsCoroutine(cardList, ownerObj, scale, createDelay));
        return cardList.Count;
    }

    /// <summary> 카드를 생성한다 (여러 적 카드 생성 할 때) </summary>
    /// <param name="useCards">IUseCard 인터페이스 리스트</param>
    /// <param name="ownerObjs">카드 소유주 게임오브젝트</param>
    /// <param name="createDelay">카드간의 생성 딜레이 시간</param>
    /// <returns>생성된 카드 개수</returns>
    public int CreateCards(List<IUseCard> useCards, List<GameObject> ownerObjs, Vector3 scale, float createDelay = 0)
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

        StartCoroutine(CreateCardsCoroutine(codes, ownerObjectList, scale, createDelay));
        return codes.Count;
    }

    /// <summary> 카드 생성 코루틴 </summary>
    /// <param name="codes">생성 카드 코드 List</param>
    /// <param name="ownerObj">카드 소유주 게임오브젝트</param>
    /// <param name="createDelay">카드간의 생성 딜레이 시간</param>
    /// <returns></returns>
    private IEnumerator CreateCardsCoroutine(List<string> codes, GameObject ownerObj, Vector3 scale,
        float createDelay = 0)
    {
        List<GameObject> createCardObjs = new List<GameObject>();
        PositionSorterInfo sorterInfo = new PositionSorterInfo()
        {
            CardWidth = 225 * scale.x,
            CardHeight = 330 * scale.y,
            CardPaddingX = 20,
            CardPaddingY = 20,
            DicePaddingX = 0
        };

        int colummMaxCount = codes.Count > 5 ? codes.Count / 2 + codes.Count % 2 : codes.Count;
        List<Vector3> posList = PositionSorter.SortCard(codes.Count, colummMaxCount, sorterInfo);

        for (int i = 0; i < codes.Count; ++i) // 생성 가능한 카드 코드를 가지고 미리 카드 생성 후 Active 끄기
        {
            GameObject cardObj = Instantiate(_cardPrefabs, _cardParent);
            Vector3 pos = posList[i] + new Vector3((sorterInfo.CardWidth + sorterInfo.CardPaddingX) / 2, 0, 0);
            cardObj.transform.localPosition = pos;
            cardObj.transform.localScale = scale;
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
    private IEnumerator CreateCardsCoroutine(List<string> codes, List<GameObject> ownerObjs, Vector3 scale,
        float createDelay = 0)
    {
        Debug.Assert(codes.Count == ownerObjs.Count);

        List<GameObject> createCardObjs = new List<GameObject>();
        PositionSorterInfo sorterInfo = new PositionSorterInfo()
        {
            CardWidth = 225 * scale.x,
            CardHeight = 330 * scale.y,
            CardPaddingX = 20,
            CardPaddingY = 20,
            DicePaddingX = 0
        };

        int colummMaxCount = codes.Count > 5 ? codes.Count / 2 + codes.Count % 2 : codes.Count;
        List<Vector3> posList = PositionSorter.SortCard(codes.Count, colummMaxCount, sorterInfo);

        for (int i = 0; i < codes.Count; ++i) // 생성 가능한 카드 코드를 가지고 미리 카드 생성 후 Active 끄기
        {
            GameObject cardObj = Instantiate(_cardPrefabs, _cardParent);
            Vector3 pos = posList[i] + new Vector3((sorterInfo.CardWidth + sorterInfo.CardPaddingX) / 2, 0, 0);
            cardObj.transform.localPosition = pos;
            cardObj.transform.localScale = scale;
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


    /// <summary> 카드를 생성한다 </summary>
    /// <param name="cardList">생성할 카드 코드 리스트</param>
    /// <param name="ownerObj">카드 소유주 게임오브젝트</param>
    /// <param name="createDelay">카드간의 생성 딜레이 시간</param>
    /// <returns>생성된 카드 개수</returns>
    public int CreateCards(List<EventCardInfo> infos, List<GameObject> ownerObjs, Vector3 scale,
        float createDelay = 0)
    {

        StartCoroutine(CreateCardsCoroutine(infos, ownerObjs, scale, createDelay));
        return infos.Count;
    }
    
    private IEnumerator CreateCardsCoroutine(List<EventCardInfo> infos, List<GameObject> ownerObjs, Vector3 scale,
        float createDelay = 0)
    {
        //Debug.Assert(infos.Count == ownerObjs.Count);

        List<GameObject> createCardObjs = new List<GameObject>();
        PositionSorterInfo sorterInfo = new PositionSorterInfo()
        {
            CardWidth = 225 * scale.x,
            CardHeight = 330 * scale.y,
            CardPaddingX = 20,
            CardPaddingY = 20,
            DicePaddingX = 0
        };

        int colummMaxCount = infos.Count > 5 ? infos.Count / 2 + infos.Count % 2 : infos.Count;
        List<Vector3> posList = PositionSorter.SortCard(infos.Count, colummMaxCount, sorterInfo);

        for (int i = 0; i < infos.Count; ++i) // 생성 가능한 카드 코드를 가지고 미리 카드 생성 후 Active 끄기
        {
            GameObject cardObj = Instantiate(_cardPrefabs, _cardParent);
            Vector3 pos = posList[i] + new Vector3((sorterInfo.CardWidth + sorterInfo.CardPaddingX) / 2, 0, 0);
            cardObj.transform.localPosition = pos;
            cardObj.transform.localScale = scale;
            //cardObj.GetComponent<CardSettor>().SetCard(infos[i]);
            switch (infos[i].Type)
            {
                case EventCardType.Two:
                {
                    CardBase222 card = cardObj.AddComponent<CardBase222>();
                    card.Name = infos[i].Title;
                    card.Description12 = infos[i].Context1;
                    card.Description34 = infos[i].Context2;
                    card.Description56 = infos[i].Context3;
                    card.EffectType12 = infos[i].CardEffectInfos1[0].EventCardEffectType;
                    card.EffectType34 = infos[i].CardEffectInfos2[0].EventCardEffectType;
                    card.EffectType56 = infos[i].CardEffectInfos3[0].EventCardEffectType;
                    card.EffectNum12 = infos[i].CardEffectInfos1[0].Num;
                    card.EffectNum34 = infos[i].CardEffectInfos2[0].Num;
                    card.EffectNum56 = infos[i].CardEffectInfos3[0].Num;
                }
                    break;
                case EventCardType.Three:
                {
                    CardBase33 card = cardObj.AddComponent<CardBase33>();
                    card.Name = infos[i].Title;
                    card.Description123 = infos[i].Context1;
                    card.Description456 = infos[i].Context2;
                    card.EffectType123 = infos[i].CardEffectInfos1[0].EventCardEffectType;
                    card.EffectType456 = infos[i].CardEffectInfos2[0].EventCardEffectType;
                    card.EffectNum123 = infos[i].CardEffectInfos1[0].Num;
                    card.EffectNum456 = infos[i].CardEffectInfos2[0].Num;
                }
                    break;
                case EventCardType.Six:
                {
                    CardBase6 card = cardObj.AddComponent<CardBase6>();
                    card.Name = infos[i].Title;
                    card.Description = infos[i].Context6;
                    card.EventCardEffectType = infos[i].EventCardEffectType6;
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            cardObj.SetActive(false);
            //cardObj.GetComponent<Card>().OwnerObj = ownerObjs[i];
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
        Logger.Assert(_cards.Remove(card));
        if (_cards.Count == 0)
        {
            FadeManager.Instance.StartFadeOut();
            StageManager.Instance.OpenStageAddListener(StageManager.Instance.GetNextStage());
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
