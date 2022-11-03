using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorks : MonoBehaviour
{
    [SerializeField]
    private float height;

    private Vector3 firstPos, targetPos;

    private float count;

    private void Start()
    {
        firstPos = transform.localPosition;
        targetPos = transform.localPosition + new Vector3(0, height, 0);
        count = 0;
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(firstPos, targetPos, count);
        if (count > 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            count += 0.01f;
        }
    }
}
