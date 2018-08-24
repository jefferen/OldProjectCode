using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlSystemForAi : MonoBehaviour
{
    public static int myLvL;
    Text lvlIndicator;

    public static int currentExp;
    public static int currentLevel;

    public GameObject effectHere;
    public int GetReadyForNextLvl;

    public GameObject lvlUp;

    public GameObject Ball;
    public GameObject Orb;
    public GameObject Charm;
    public GameObject Dash;

    public bool ultReady;

    public EnemyHealthManager player;
    [HideInInspector]
    public bool lebool;

    bool ball;
    bool orb;
    bool charm;
    bool dash;

    void Awake()
    {
        ball = true;
        //what = GameObject.Find("DmgCounter"); // find his own counter
        player = GameObject.Find("ChampEnemy").GetComponent<EnemyHealthManager>();
    }

    void Start()
    {
        lebool = true;
        lvlIndicator = GameObject.Find("LvlIndicatorE").GetComponent<Text>();
        myLvL = 1;

        currentLevel = 1;
        GetReadyForNextLvl = currentLevel;
    }

    void Update()
    {
        lvlIndicator.text = "" + myLvL;
        if (myLvL <= 17)
        {
            if (currentLevel > GetReadyForNextLvl || Input.GetKeyDown(KeyCode.H))
            {
                lebool = true;
                myLvL += 1;
                Debug.Log("Lvl up ai");
                player.maxEnemyHealth += 90;
                player.enemyHealth += 90;
                player.baseDmg += 7; // set this to his dmg counter

                if(myLvL == 2) { orb = true; }             
                if (myLvL == 3) { charm = true; }
                if (myLvL == 4) { orb = true; } // max ball first then charm then orb
                if (myLvL == 5) { orb = true; }
                if (myLvL == 6) { dash = true; }  // dash
                if (myLvL == 7) { orb = true; }
                if (myLvL == 8) { orb = true; }
                if (myLvL == 9) { orb = true; }
                if (myLvL == 10) { orb = true; }
                if (myLvL == 11) { orb = true; } // dash upgrade
                if (myLvL == 12) { orb = true; }
                if (myLvL == 13) { orb = true; }
                if (myLvL == 14) { orb = true; }
                if (myLvL == 15) { orb = true; } // dash last upgrade
                if (myLvL == 16) { orb = true; }
                if (myLvL == 17) { orb = true; }
                if (myLvL == 18) { orb = true; }

                var me = Instantiate(lvlUp, effectHere.transform.position, effectHere.transform.rotation);
                me.transform.parent = effectHere.transform;
                GetReadyForNextLvl = currentLevel;
            }
        }
    }
    public static void UpdateXP(int exp)
    {
        currentExp += exp;

        int ourLvl = (int)(0.1f * Mathf.Sqrt(currentExp));

        if (ourLvl != currentLevel)
        {
            currentLevel = ourLvl;
        }

        int expNextLvl = 100 * (currentLevel + 1) * (currentLevel + 1);

        int differnceExp = expNextLvl - currentExp;

        int totalDifference = expNextLvl - (100 * currentLevel * currentLevel);
    }
}