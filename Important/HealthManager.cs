using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HealthManager : MonoBehaviour
{
    public float maxPlayerHealth;
    public static float playerHealth;

    private EnergyManager energy;
    public bool isDead;
    public Slider healthBar;
    private LevelManager levelManager;

    public Player player;
    public Animator anim;
    public float regen = 3f;
    internal static object damageToGive;

    public float DmgOverTime;
    public static bool dodmg;

    void Start ()
    {
        DmgTextController.Initialize();
        player = FindObjectOfType<Player>();
        healthBar = GetComponent<Slider>();
        playerHealth = maxPlayerHealth;
        levelManager = FindObjectOfType<LevelManager>();
        isDead = false;
    }

    void Update ()
    {
        healthBar.value = playerHealth;
        if (playerHealth < maxPlayerHealth)
        {
            playerHealth += regen * Time.deltaTime / 4;
        }
        if (playerHealth <= 0 && !isDead)
        {
            playerHealth = 0;
            levelManager.RespawnPlayer();
            isDead = true;
            anim.SetBool("isDead", true);
        }
        if (dodmg == true)
        {
            playerHealth -= DmgOverTime * Time.deltaTime;
            Invoke("stopDmg", 5);
        }
        healthBar.value = playerHealth;
    }
    void stopDmg()
    {
        dodmg = false;
    }
    public virtual void giveDamageAtAoe(int damageToGive)
    {
        dodmg = true;
    }

    public static void HurtPlayer (int damageToGive)
    {
        playerHealth -= damageToGive;
    }

    public void FullHealth()
    {
        playerHealth = maxPlayerHealth;
        anim.SetBool("isDead", false);
    }
}
