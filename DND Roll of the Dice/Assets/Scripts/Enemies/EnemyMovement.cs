using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool stunned;
    private float timeUntilEndOfStun;

    private void LateUpdate()
    {
        if (timeUntilEndOfStun > 0)
        {
            timeUntilEndOfStun -= Time.deltaTime;
            stunned = true;
        }
        else stunned = false;
    }

    public void Stun(float stunTime)
    {
        timeUntilEndOfStun = stunTime;
    }
}
