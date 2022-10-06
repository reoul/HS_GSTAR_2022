using System;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (Dice dice in FindObjectsOfType<Dice>())
            {
                dice.Roll();
            }
        }
    }
}
