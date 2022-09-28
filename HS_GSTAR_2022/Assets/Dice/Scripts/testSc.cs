using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class testSc : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.GetComponent<Animator>().Play("Cube|Result_1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.GetComponent<Animator>().Play("Cube|Result_2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.GetComponent<Animator>().Play("Cube|Result_3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.GetComponent<Animator>().Play("Cube|Result_4");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            this.GetComponent<Animator>().Play("Cube|Result_5");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            this.GetComponent<Animator>().Play("Cube|Result_6");
        }
    }
}
