using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceValue : MonoBehaviour
{
    public int value;

    public void GenerateRandomValue(int min, int max)
    {
        value = Random.Range(min, max + 1);
    }
}
