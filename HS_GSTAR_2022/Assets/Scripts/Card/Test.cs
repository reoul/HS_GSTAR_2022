using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    public TMP_Text Text;
    void Start()
    {
        StartCoroutine(UpdateAnimation());
    }

    IEnumerator UpdateAnimation()
    {
        int i = 0;
        while (true)
        {
            i = ++i % 9;
            Text.text = $"    <size=15><sprite={i}></size>내가 최고야";
            yield return new WaitForSeconds(0.2f);
        }
    }
}
