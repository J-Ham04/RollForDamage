using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimation : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    
    public void SpawnEnemy(int index)
    {
        Instantiate(enemies[index], transform.position, Quaternion.identity);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void D20Sound()
    {
        FindObjectOfType<AudioManager>().Play("D20 Drop");
        FindObjectOfType<CameraShaker>().Shake();
    }
}
