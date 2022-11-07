using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextMover : MonoBehaviour
{
    private float _runningTime;
    private TMP_Text _text;
    private Vector3 _targetPos = new Vector3(0, 200, 0);

    private void Update()
    {
        if(_text.color.a <= 0)
        {
            this.gameObject.SetActive(false);
        }
        _runningTime += Time.deltaTime;

        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPos, 0.003f) + new Vector3(Mathf.Cos(_runningTime * 10),0,0);
        Color tmpColor = _text.color;
        tmpColor.a -= 0.0035f;
        _text.color = tmpColor;

        //this.transform.localPosition = new Vector3(Mathf.Tan(_runningTime) * 100, Mathf.Tan(_runningTime) * 100, 0);
    }

    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;
    }

    public void Enable(int damage)
    {
        this.gameObject.SetActive(true); 
        _text = transform.GetComponent<TMP_Text>();
        _text.text = $"-{damage.ToString()}";
        Color tmpColor = _text.color;
        tmpColor.a = 1;
        _text.color = tmpColor;
        _runningTime = 0;
    }

}
