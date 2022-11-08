using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextMover : MonoBehaviour
{
    private float _runningTime;
    private TMP_Text _text;
    private Vector3 _targetPos, _startPos;

    private void Update()
    {
        if(_text.color.a <= 0)
        {
            this.gameObject.SetActive(false);
        }
        _runningTime += Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, _targetPos, 0.003f) + new Vector3(Mathf.Cos(_runningTime * 10) * 0.01f,0,0);
        Color tmpColor = _text.color;
        tmpColor.a -= 0.0035f;
        _text.color = tmpColor;

    }

    public void Enable(int damage, Vector3 startPos)
    {
        this.gameObject.SetActive(true); 
        _text = transform.GetComponent<TMP_Text>();
        _text.text = $"-{damage.ToString()}";
        _startPos = startPos;
        _targetPos = _startPos + Vector3.forward * 3;
        transform.position = _startPos;
        Color tmpColor = _text.color;
        tmpColor.a = 1;
        _text.color = tmpColor;
        _runningTime = 0;
    }

}
