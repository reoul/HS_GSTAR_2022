using UnityEngine;
using TMPro;

public class DisplayMoney : MonoBehaviour
{
    private float _displayMoney, _targetMoney;
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _displayMoney = Mathf.Lerp(_displayMoney, _targetMoney, Time.deltaTime);
        _text.text = Mathf.Round(_displayMoney).ToString();
    }

    public void SetTargetMoney(int money)
    {
        _targetMoney = money;
    }
}
