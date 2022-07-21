using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D4Shockwave : MonoBehaviour
{
    [SerializeField] private int shockwaveDuration;
    [SerializeField] private float stunTime;
    [SerializeField] private float blastSpeed;

    private AudioManager am;
    private CameraShaker camShake;

    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        camShake = FindObjectOfType<CameraShaker>();

        am.Play("D4 Landing");

        if (camShake)
        {
            camShake.Shake();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)
        {
            PlayerMovement pm = collision.GetComponent<PlayerMovement>();

            if (pm)
            {
                pm.stunned = stunTime;
            }
        }
    }
}
