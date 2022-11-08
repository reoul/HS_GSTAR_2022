using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
    [SerializeField]
    private GameObject _textPref;

    private Queue<DamageTextMover> _objPool;
    private int _maxCount = 20;

    private void Start()
    {
        InitPool();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DamageCount(transform.position);
        }
    }

    private void InitPool()
    {
        _objPool = new Queue<DamageTextMover>();
        for (int i = 0; i < _maxCount; i++)
        {
            GameObject tmpObj = GameObject.Instantiate(_textPref, this.transform);
            tmpObj.transform.localPosition = Vector3.zero;
            tmpObj.SetActive(false);
            _objPool.Enqueue(tmpObj.GetComponent<DamageTextMover>());
        }
    }

    public void DamageCount(Vector3 pos)
    {
        DamageTextMover tmpObj = _objPool.Dequeue();
        tmpObj.Enable(100, pos);
        _objPool.Enqueue(tmpObj);
    }
}
