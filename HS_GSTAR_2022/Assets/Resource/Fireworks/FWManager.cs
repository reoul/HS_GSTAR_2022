using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] fireWorks;

    float cooltime;
    float maxTime;

    private void Start()
    {
        cooltime = 5;
        maxTime = cooltime;
    }

    void Update()
    {
        if (cooltime >= maxTime)
        {
            GameObject firework = GameObject.Instantiate(fireWorks[Random.Range(0,2)],transform);
            firework.transform.localPosition = new Vector3(Random.Range(0, 1920) - 1920 * 0.5f, Random.Range(0, 1080) - 1080 * 0.5f, 0);

            cooltime = 0;
        }
        cooltime += 0.1f;
    }
}
