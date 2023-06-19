using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Weapon : MonoBehaviour
{
    #region Singleton

    public static Weapon weapon;

    private void Awake()
    {
        weapon = this;
    }

    #endregion

    public static int score = 0;
    public int totalScore;
    
    [Header("Insane upgrades")]
    public int lvl;
    public float damage;
    public float damageM;
    public float damageF;
    public float damageCost;
    public float damageCostM;
    public float fireRate;
    public float fireRateM;
    public float fireRateF;
    public float fireRateCost;
    public float fireRateCostM;
    public float range;
    public float rangeM;
    public float rangeF;
    public float rangeCost;
    public float rangeCostM;
    
    [Header("Shooting")]
    public float force = 155f;
    private float _shootDelay;
    public bool canShoot;
    public Transform bulletSpawn;
    public AudioClip shotSFX;
    public GameObject hitEffect;

    [Header("Ammo")]
    public int ammo = 30;
    public int maxAmmo = 30;

    [Header("OMG it's someshit")]
    public Animator anim;
    public TextMeshProUGUI TMPro;
    public TextMeshProUGUI w_damageValue;
    public TextMeshProUGUI w_damageCost;
    public TextMeshProUGUI w_fireRateValue;
    public TextMeshProUGUI w_fireRateCost;
    public TextMeshProUGUI w_distanceValue;
    public TextMeshProUGUI w_distanceCost;
    public ParticleSystem  muzzleFlash;
    public AudioClip reloadSFX;
    public AudioClip scopeSFX;
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public Camera cam;
    public DynamicTextData data;
    public bool isReloading;
    public Stat damageStat;
    public Stat shootSpeedStat;
    public Stat distanceStat;

    private void Start()
    {
        score = 0;
        totalScore= 0;

        anim = GetComponent<Animator>();
        fill.color = gradient.Evaluate(1f);

        damageCost = (int)damageStat.cost;
        damage = (int)damageStat.value;
        damageCostM = damageStat.costMultiplier;
        damageM = damageStat.valueMultiplier;
        fireRateCost = (int)shootSpeedStat.cost;
        fireRate = (int)shootSpeedStat.value;
        fireRateCostM = shootSpeedStat.costMultiplier;
        fireRateM = shootSpeedStat.valueMultiplier;
        rangeCost = (int)distanceStat.cost;
        range = (int)distanceStat.value;
        rangeCostM = distanceStat.costMultiplier;
        rangeM = distanceStat.valueMultiplier;
        w_damageValue.text = "Значение: " + ((int)weapon.damage).ToString();
        w_damageCost.text = "Стоимость: " + ((int)weapon.damageCost).ToString();
        w_fireRateValue.text = "Значение: " + ((int)weapon.fireRate).ToString();
        w_fireRateCost.text = "Стоимость: " + ((int)weapon.fireRateCost).ToString();
        w_distanceValue.text = "Значение: " + ((int)weapon.range).ToString();
        w_distanceCost.text = "Стоимость: " + ((int)weapon.rangeCost).ToString();
    }

    private void Update()
    {
        if (canShoot) IsShooting();
        ScoreOutput();
        Reload();
        IdleRandom();
        Scope();
        GetInput();
    }

    private void IdleRandom()
    {
        if (Input.GetKey("f") && !anim.GetBool("isScoping"))
        {
            anim.Play("idle_random");
        }

        if (Input.GetKey("f") && anim.GetBool("isScoping"))
        {
            anim.Play("idle_scope_random");
        }
    }

    private void Scope()
    {
        if (Input.GetMouseButtonUp(1))
        {
            cam.fieldOfView += 20;
            anim.SetBool("isScoping", false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            audioSource1.PlayOneShot(scopeSFX);
            cam.fieldOfView -= 20;
            anim.SetBool("isScoping", true);
        }
    }
    
    private void ScoreOutput()
    {
        TMPro.text = score.ToString();
    }

    private void IsShooting()
    {
        if (Input.GetButton("Fire1") && Time.time > _shootDelay && ammo > 0)
        {
            _shootDelay = Time.time + 1f / fireRate;
            Shoot();
            ammo--;
            SetAmmoSlider(ammo);
        }
    }
    
    private void Shoot()
    {   
        if (anim.GetBool("isScoping"))
        {
            anim.Play("scope_shoot");
        }
        else
        {
            anim.Play("shoot");
        }

        audioSource.Stop();
        audioSource.PlayOneShot(shotSFX);
        muzzleFlash.Play();

        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.rigidbody != null) 
            {
                hit.rigidbody.AddForceAtPosition(-hit.normal * force, hit.point);
            }


            var hitBox = hit.collider.GetComponent<HitBox>();
            if (hitBox)
            {
                GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
                DynamicTextManager.CreateText(hit.point, damage.ToString(), data);
                hitBox.OnRaycastHit(this);
            }
        }
    }

    public IEnumerator PlayReload()
    {
        anim.Play("reload");
        yield return new WaitForSeconds(3);
        ammo = maxAmmo;
        SetAmmoSlider(ammo);
        isReloading = false;
        canShoot = true;
    }

    public void Reload()
    {
        if (Input.GetKeyDown("r") && ammo < maxAmmo && !isReloading) 
        {
            canShoot = false;
            isReloading = true;
            audioSource1.PlayOneShot(reloadSFX);
            StartCoroutine(PlayReload());
        } 

    }

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetAmmoSlider(float ammo)
    {
        slider.value = ammo;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void GetInput()
    {
        //Damage
        if (Input.GetKeyDown("z")) 
        {
            damageStat.UpgradeStat();
            w_damageValue.text = damageStat.valueText.text;
            w_damageCost.text = damageStat.costText.text;
            damageCost = (int)damageStat.cost;
            damage = (int)damageStat.value;
            damageCostM = damageStat.costMultiplier;
            damageM = damageStat.valueMultiplier;
            damageF = damageStat.valueFlat;
        }

        //Shootspeed
        if (Input.GetKeyDown("x"))
        {
            shootSpeedStat.UpgradeStat();
            w_fireRateValue.text = shootSpeedStat.valueText.text;
            w_fireRateCost.text = shootSpeedStat.costText.text;
            fireRateCost = (int)shootSpeedStat.cost;
            fireRate = (int)shootSpeedStat.value;
            fireRateCostM = shootSpeedStat.costMultiplier;
            fireRateM = shootSpeedStat.valueMultiplier;
            fireRateF = shootSpeedStat.valueFlat;
        }

        //Distance
        if (Input.GetKeyDown("c"))
        {
            distanceStat.UpgradeStat();
            w_distanceValue.text = distanceStat.valueText.text;
            w_distanceCost.text = distanceStat.costText.text;
            rangeCost = (int)distanceStat.cost;
            range = (int)distanceStat.value;
            rangeCostM = distanceStat.costMultiplier;
            rangeM = distanceStat.valueMultiplier;
            rangeF = distanceStat.valueFlat;
        }
    }
}