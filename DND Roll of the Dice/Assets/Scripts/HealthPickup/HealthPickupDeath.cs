using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupDeath : MonoBehaviour
{
    [SerializeField] private GameObject pickupDeath;

    private void OnDestroy()
    {
        Instantiate(pickupDeath, transform.position, Quaternion.identity);
    }
}
