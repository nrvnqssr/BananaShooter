using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PausingMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject playerWeapon;
    public GameObject pauseMenuUI;
    public GameObject ingameUI;
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerManager.instance.isDead)
        {
            if (GameIsPaused) 
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        ingameUI.SetActive(true);
        playerWeapon.SetActive(true);
        Weapon.weapon.canShoot = true;
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        ingameUI.SetActive(false);
        playerWeapon.SetActive(false);
        Weapon.weapon.canShoot = false;
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
    }
}