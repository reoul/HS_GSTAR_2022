using System;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : Singleton<DiceManager>
{
    [SerializeField] private GameObject _dicePrefab;
    [SerializeField] private List<Transform> _diceCreatePosList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            CreateDices(8);
        }
    }

    /// <summary> 주사위 생성 </summary>
    /// <param name="diceCount">주사위 개수</param>
    public void CreateDices(int diceCount)
    {
        for (int i = 0; i < diceCount; ++i)
        {
            GameObject diceObj = Instantiate(_dicePrefab);
            diceObj.transform.position = _diceCreatePosList[i].position;
        }
    }
}
