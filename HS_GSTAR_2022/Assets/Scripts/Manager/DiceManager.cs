using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : Singleton<DiceManager>
{
    [SerializeField] private GameObject _dicePrefab;
    [SerializeField] private List<Transform> _diceCreatePosList;
    [SerializeField] private List<Dice> _dices;

    /// <summary> 주사위 생성 </summary>
    /// <param name="diceCount">주사위 개수</param>
    /// <param name="createDelay">주사위간의 생성 딜레이 시간</param>
    public void CreateDices(int diceCount, float createDelay = 0)
    {
        StartCoroutine(CreateDicesCoroutine(diceCount, createDelay));
    }

    private IEnumerator CreateDicesCoroutine(int diceCount, float createDelay = 0)
    {
        PositionSorterInfo sorterInfo = new PositionSorterInfo
        {
            DicePaddingX = 300
        };

        List<Vector3> positionList = PositionSorter.SortDice(diceCount, sorterInfo);

        List<GameObject> createDiceObjs = new List<GameObject>();
        for (int i = 0; i < diceCount; ++i) // 생성 가능한 주사위 미리 생성 후 Active 끄기
        {
            GameObject diceObj = Instantiate(_dicePrefab);
            diceObj.transform.localPosition = positionList[i];
            _dices.Add(diceObj.GetComponent<Dice>());
            diceObj.SetActive(false);
            createDiceObjs.Add(diceObj);
        }

        WaitForSeconds waitDelay = new WaitForSeconds(createDelay);
        foreach (GameObject diceObj in createDiceObjs) // 딜레이 시간동안 하나씩 키기
        {
            diceObj.SetActive(true);
            yield return waitDelay;
        }
    }

    public void RemoveDice(Dice dice)
    {
        Debug.Assert(_dices.Remove(dice));
    }

    /// <summary> 현재 생성된 모든 주사위 제거 </summary>
    public void RemoveAllDice()
    {
        StopAllCoroutines();
        foreach (Dice dice in _dices)
        {
            Destroy(dice.gameObject);
        }

        _dices.Clear();
    }
}
