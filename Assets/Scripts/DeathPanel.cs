using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class DeathPanel : MonoBehaviour
{
    public Text Waves;
    public Text Times;
    public Text Bananas;
    public int waves;
    public int times;
    public float timers;
    public int bananas;

    private void FixedUpdate()
    {
        Tick();
        GetValues();
        Output();
    }

    private void GetValues()
    {
        waves = Spawner.instance.w_wave;
        times = (int)timers;
        bananas = Weapon.weapon.totalScore;
    }

    private void Output()
    {
        Waves.text = waves.ToString();
        Times.text = times.ToString();
        Bananas.text = bananas.ToString();
    }

    private void Tick()
    {
        timers = !PlayerManager.instance.isDead ? timers + Time.deltaTime : timers + 0;
    }
}
