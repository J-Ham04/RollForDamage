using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D4Spawn : MonoBehaviour
{
    private DiceValue dv;
    private Animator anim;

    private void Awake()
    {
        dv = GetComponent<DiceValue>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        dv.GenerateRandomValue(1, 4);

        switch (dv.value)
        {
            default:
            case 1:
                anim.SetInteger("face", 1);
                break;
            case 2:
                anim.SetInteger("face", 2);
                break;
            case 3:
                anim.SetInteger("face", 3);
                break;
            case 4:
                anim.SetInteger("face", 4);
                break;
        }
    }
}
