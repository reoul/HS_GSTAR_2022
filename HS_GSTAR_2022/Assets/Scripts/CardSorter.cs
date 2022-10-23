using System.Collections.Generic;
using UnityEngine;

public class CardSorter
{
    [SerializeField]
    private float _cardWidth, _cardHeight, _cardInterverX, _cardInterverY;
    [SerializeField]
    private float _diceInterverX;
    [SerializeField]
    private int _numberOfDice;

    public List<Vector3> SortCard(List<Card> cards)
    {
        Vector3 _currentVector3 = Vector3.zero;

        int repeat = (int)Mathf.Ceil(cards.Count / 4f); //반복할 횟수

        _currentVector3 -= new Vector3(0, ((_cardInterverY + _cardHeight) * repeat) / 2f, 0);

        List<Vector3> vector3s = new List<Vector3>(); //반환할 좌표들을 담을 리스트

        for(int i= 0; i < repeat; i++)
        {
            if (i < repeat - 1)
            {
                _currentVector3.x = -((_cardInterverX + _cardWidth) * 3) / 2f;
            }
            else
            {
                _currentVector3.x = -((_cardInterverX + _cardWidth) * ((cards.Count % 4))) / 2f;
            }
            for(int j=0; j < 4; j++)
            {
                vector3s.Add(_currentVector3);
                _currentVector3.x += _cardInterverX + _cardWidth;
            }
            _currentVector3.y += _cardInterverY + _cardHeight;
        }

        return vector3s;
    }

    public List<Vector3> SortDice(int count)
    {
        Vector3 _currentVector3 = Vector3.zero;
        _currentVector3 -= new Vector3((_diceInterverX * (count - 1)) / 2f, 0, 0);

        List<Vector3> vector3s = new List<Vector3>(); //반환할 좌표들을 담을 리스트

        for (int i = 0; i < count; i++)
        {
            vector3s.Add(_currentVector3);
            _currentVector3 += new Vector3(_diceInterverX, 0, 0);
        }

        return vector3s;
    }
}
