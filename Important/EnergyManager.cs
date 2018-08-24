using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EnergyManager : MonoBehaviour
{
    public int maxPlayerEnergy;
    public static float playerEnergy;
    public float regen = 3f;

    public Slider energyBar;
    private LevelManager levelManager;

    void Start()
    {
        energyBar = GetComponent<Slider>();
        playerEnergy = maxPlayerEnergy;
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        energyBar.value = playerEnergy;
        if(playerEnergy < maxPlayerEnergy)
        {
            playerEnergy += 3 * regen * Time.deltaTime; 
        }
    }
    public static void exhaustPlayer(int pointsToGive)
    {
        playerEnergy -= pointsToGive;
    }

    public void FullEnergy()
    {
        playerEnergy = maxPlayerEnergy;
    }
}
