using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
        
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public bool isDead;

    public GameObject player;
    public GameObject deathUI;
    public GameObject ingameUI;
    public GameObject weapon;
    public GameObject spawners;
    public HeathBar healthBar;
    public StaminaBar staminaBar;

    public float HP = 100;
    public float maxHP = 100;
    public float stamina = 100;

    private void Update()
    {
        if (HP <= 0 && !isDead)
        {
            Death();
        }
    }

    private void FixedUpdate()
    {
        stamina = FirstPersonController.FPCinstance.m_Stamina;
        staminaBar.SetStamina(stamina);
    }

    private void Start()
    {
        isDead = false;
        deathUI.SetActive(false);
        ingameUI.SetActive(true);
        weapon.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Damage(float damage)
    {
        HP -= damage;
        healthBar.SetHealth(HP);
    }

    public void Heal(float heal)
    {
        if (HP <= maxHP)
        {
            if (HP + heal > maxHP)
            {
                HP = maxHP;
            } else
            {
                HP += heal;
            }
        } 
        healthBar.SetHealth(HP);
    }

    private void Death()
    {
        isDead = true;
        deathUI.SetActive(true);
        ingameUI.SetActive(false);deathUI.SetActive(true);
        ingameUI.SetActive(false);
        weapon.SetActive(false);
        spawners.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }
}