using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // FIELDS
    [SerializeField] private Spawn[] spawners;

    public TMP_Text startTimerUI;
    public float startTimer = 4;
    int previousTimerNum;

    bool isGameRunning = false;

    private AudioManager am;

    // METHODS
    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();

        Time.timeScale = 1f;

        foreach (Spawn s in spawners)
        {
            s.timeBeforeNextSpawn = s.timeBeforeFirstSpawn;
        }
    }

    private void Update()
    {


        if (startTimer > 0)
        {
            if (previousTimerNum != (int)startTimer)
            {
                previousTimerNum = (int)startTimer;

                switch ((int)startTimer)
                {
                    default:
                        am.Play("Timer Go");
                        break;
                    case 4:
                        break;
                    case 3:
                        am.Play("Timer Countdown");
                        break;
                    case 2:
                        am.Play("Timer Countdown");
                        break;
                    case 1:
                        am.Play("Timer Countdown");
                        break;
                }
            }
            startTimer -= Time.deltaTime;
        }
        else
        {
            startTimerUI.enabled = false;
            isGameRunning = true;
        }

        if (startTimerUI)
        {
            startTimerUI.text = ((int)startTimer).ToString();
        }

        TickSpawnTimers();
    }

    private void TickSpawnTimers()
    {
        if (isGameRunning == false)
        {
            return;
        }

        foreach(Spawn s in spawners)
        {
            s.timeBeforeNextSpawn -= Time.deltaTime;
            
            if(s.timeBeforeNextSpawn <= 0)
            {
                Spawn(s);
            }
        }
    }

    private void Spawn(Spawn spawn)
    {
        Vector2 spawnPoint = new Vector2(Random.Range(-6f, 6f), Random.Range(-4f, 4f));

        Instantiate(spawn.spawner, spawnPoint, Quaternion.identity);

        spawn.timeBeforeNextSpawn = spawn.timeBetweenSpawn;

        if (spawn.speedUpOverTime)
        {
            spawn.timeBetweenSpawn *= 0.99f;
        }
    }
}
