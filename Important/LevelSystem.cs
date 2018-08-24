using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public static int myLvL;
    Text text;
    Text lvlIndicator;
    bool bb;

    public static int currentExp;
    public static int currentLevel;

    public static int currentMoney;

    public GameObject effectHere;
    public int GetReadyForNextLvl;

    public GameObject lvlUp;

    public int smallIncreaseInMoney;

    private GameObject Ball;
    private GameObject Orb;
    private GameObject Charm;
    private GameObject Dash;

    public bool ultReady;
    public bool keepMeUp;

    public EnemyHealthManager player;
    [HideInInspector]
    public bool lebool;

    public int number;

    public GameObject what;

    void Awake()
    {
            what = GameObject.Find("DmgCounter");
            player = GameObject.Find("Champ").GetComponent<EnemyHealthManager>();
    }

    void Start ()
    {
        lebool = true;
        text = GetComponent<Text>();
        lvlIndicator = GameObject.Find("LvlIndicatorP").GetComponent<Text>();
        myLvL = 1;

        Ball = GameObject.Find("ChooseButtonForBall");
        Orb = GameObject.Find("ChooseButtonForOrb");
        Charm = GameObject.Find("ChooseButtonForCharm");
        Dash = GameObject.Find("ChooseButtonForDash");
    
    ScoreManager.AddPoints(currentMoney);
        currentMoney = 0;
        currentLevel = 1;
        GetReadyForNextLvl = currentLevel;

        InvokeRepeating("PocketMoney", 0f, 1f);
    }
    void PocketMoney()
    {
        currentMoney += 4;
        ScoreManager.AddPoints(4);
    }
	void Update()
    {
        text.text = "" + myLvL;
        number = myLvL;
        lvlIndicator.text = "" + myLvL;
        if (myLvL <= 17)
        {
            if (currentLevel > GetReadyForNextLvl || Input.GetKeyDown(KeyCode.G))
            {
                lebool = true;
                myLvL += 1;
                player.maxEnemyHealth += 90;
                player.enemyHealth += 90;
                player.baseDmg += 7;
                if (what.GetComponent<WhatIsMyDmg>().upB5 != true)
                {
                    Ball.transform.Find("underBall").gameObject.SetActive(true);
                }
                if (what.GetComponent<WhatIsMyDmg>().upC5 != true)
                {
                    Charm.transform.Find("underCharm").gameObject.SetActive(true);
                }
                if (what.GetComponent<WhatIsMyDmg>().upO5 != true)
                {
                    Orb.transform.Find("underOrb").gameObject.SetActive(true);
                }
                if (ultReady == true)
                {
                    Dash.transform.Find("underDash").gameObject.SetActive(true);
                }
                var me = Instantiate(lvlUp,effectHere.transform.position, effectHere.transform.rotation);
                me.transform.parent = effectHere.transform;
                GetReadyForNextLvl = currentLevel;
            }
        }

        if (myLvL == 5) // display mylvl
        {
            ultReady = true;
        }
        if (myLvL == 10)
        {
            keepMeUp = true;
            ultReady = true;
        }
        if (myLvL == 14)
        {
            keepMeUp = true;
            ultReady = true;
        }
        if (keepMeUp == true)
        {
            ultReady = true;
        }
    }
    public static void UpdateXP(int exp)
    {
        Debug.Log("I gained exp player");
        currentExp += exp;

        int ourLvl = (int)(0.1f * Mathf.Sqrt(currentExp));

        if(ourLvl != currentLevel)
        {
            currentLevel = ourLvl;
        }

        int expNextLvl = 100 * (currentLevel + 1) * (currentLevel + 1);

        int differnceExp = expNextLvl - currentExp;

        int totalDifference = expNextLvl - (100 * currentLevel * currentLevel); 
    }
    public static void UpdateMoney(int money)
    {
        currentMoney += money;
    }
    void callme()
    {
        bb = false;
    }
}