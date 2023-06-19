using System.Drawing;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections.LowLevel.Unsafe;
using TMPro;
using System;

public class Spawner : MonoBehaviour
{
    #region Singleton

    public static Spawner instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public float HP;
    public float damage;
    public float prize;
    public float totalTime;

    [Header("Spanwer params")]
    [Range(1,5)]
    [SerializeField] private float HPscale = 1;
    [Range(1, 5)]
    [SerializeField] private float DMGscale = 1;
    [Range(1, 5)]
    [SerializeField] private float PZscale = 1;
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private Transform spawner;
    [SerializeField] private float upscaleTimer = 1000f;
    [SerializeField] bool isEndless = true;
    [SerializeField] TextMeshProUGUI waves;

    [Header("Solo endless")]
    [SerializeField] private float time = 0f;
    [SerializeField] private float waitingTime = 1000f;

    [Header("Waves endless")]
    [SerializeField] public int w_wave = 0;
    [SerializeField] private int w_numberOfBananamen = 10;
    [SerializeField] private float w_time = 0f;
    [SerializeField] private float w_waitingTime = 1000f;

    private void Start()
    {
        StartCoroutine(Upscale(HPscale, DMGscale, PZscale));
    }

    private void Update()
    {
        StartSpawn();
    }

    private void StartSpawn()
    {
        TimerTick();
        switch (isEndless)
        {
            case true:
                {
                    Check();
                    break;
                }
            case false:
                {
                    InputCheck();
                    break;
                }
        }
    }

    private void TimerTick()
    {
        time += Time.deltaTime;
        w_time += Time.deltaTime;
    }

    private void Check()
    {
        if (time > waitingTime)
        {
            Spawn();
            TimerReset();
        }
    }

    private void StartWave()
    {
        for (int i = 0; i < w_numberOfBananamen; i++)
            Instantiate(spawnObject, spawner);
        w_wave++;
        WaveOutput();
    }

    private void WaveOutput()
    {
        waves.text = w_wave.ToString();
    }

    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) || w_time > w_waitingTime)
        {
            StartWave();
            TimerReset();
        }
    }

    private void TimerReset()
    {
        time = 0f;
        w_time = 0f;
    }

    public void Spawn()
    {
        Instantiate(spawnObject, spawner);
    }

    int amount = 0;
    IEnumerator Upscale(float HPscale, float DMGscale, float PZscale)
    {
        while (true)
        {
            HP *= HPscale;
            damage *= DMGscale;
            prize *= PZscale;
            float HPscaleam = 1;
            float DMGscaleam = 1;
            float PZscaleam = 1;
            for (int i = 0; i < amount; i++)
            {
                HPscaleam *= HPscale;
                DMGscaleam *= DMGscale;
                PZscaleam *= PZscale;
            }
            amount++;
            Debug.Log("HPscale = " + HPscaleam + "DMGscale = " + DMGscaleam + "PZscale = " + PZscaleam);
            yield return new WaitForSeconds(upscaleTimer);
        }
    }
}