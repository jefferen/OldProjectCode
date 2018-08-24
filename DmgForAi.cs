using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgForAi : MonoBehaviour
{
    public bool upB1;
    public bool upB2;
    public bool upB3;
    public bool upB4;
    public bool upB5;

    public bool upC1;
    public bool upC2;
    public bool upC3;
    public bool upC4;
    public bool upC5;

    public bool upO1;
    public bool upO2;
    public bool upO3;
    public bool upO4;
    public bool upO5;

    public bool upD1;
    public bool upD2;
    public bool upD3;

    public GameObject Ball;
    public GameObject Orb;
    public GameObject Charm;
    public GameObject Dash;

    public GameObject BallB;
    public GameObject OrbB;
    public GameObject CharmB;
    public GameObject DashB;

    GameObject theSystem;

    public int lvlupOpacity;
    public int levelsynchro;

    void Start()
    {
        lvlupOpacity = 0;
        theSystem = GameObject.Find("LvlCounterPlayer");

        Ball = GameObject.Find("Abillity Icon Ball");
        Orb = GameObject.Find("Abillity Icon OrbingOrbs");
        Charm = GameObject.Find("Abillity Icon Charm");
        Dash = GameObject.Find("Abillity Icon Dash");

        BallB = GameObject.Find("ChooseButtonForBall");
        OrbB = GameObject.Find("ChooseButtonForOrb");
        CharmB = GameObject.Find("ChooseButtonForCharm");
        DashB = GameObject.Find("ChooseButtonForDash");
    }

    void setActiveToFalse()
    {
        theSystem.GetComponent<LevelSystem>().lebool = false;
        BallB.transform.Find("underBall").gameObject.SetActive(false);
        OrbB.transform.Find("underOrb").gameObject.SetActive(false);
        CharmB.transform.Find("underCharm").gameObject.SetActive(false);
        DashB.transform.Find("underDash").gameObject.SetActive(false);
    }

    void Update()
    {
        if (theSystem.GetComponent<LevelSystem>().number != levelsynchro)
        {
            levelsynchro += 1;
            lvlupOpacity += 1;
        }
    }

    public void upgradeMagicBall()
    {
        if (upB5 == false)
        {
            lvlupOpacity += -1;
        }
        if (upB5 == false && upB4 == true)
        {
            BallB.transform.Find("underBall").gameObject.SetActive(false);
            upB5 = true;
        }
        if (upB4 == false && upB3 == true)
        {
            upB4 = true;
        }
        if (upB3 == false && upB2 == true)
        {
            upB3 = true;
        }
        if (upB2 == false && upB1 == true)
        {
            upB2 = true;
        }
        if (upB1 == false)
        {
            Ball.GetComponent<AbillityCoolDown>().coolDownDuration = 7;
            Ball.GetComponent<AbillityCoolDown>().enabled = true;
            upB1 = true;
        }
        if (lvlupOpacity >= 1)
        {
        }
        else
        {
            setActiveToFalse();
        }
    }
    public void upgradeCharm()
    {
        if (upC5 == false)
        {
            lvlupOpacity += -1;
        }
        if (upC5 == false && upC4 == true)
        {
            CharmB.transform.Find("underCharm").gameObject.SetActive(false);
            upC5 = true;
        }
        if (upC4 == false && upC3 == true)
        {
            upC4 = true;
        }
        if (upC3 == false && upC2 == true)
        {
            upC3 = true;
        }
        if (upC2 == false && upC1 == true)
        {
            upC2 = true;
        }
        if (upC1 == false)
        {
            Charm.GetComponent<AbillityCoolDown>().coolDownDuration = 12;
            Charm.GetComponent<AbillityCoolDown>().enabled = true;
            upC1 = true;
        }
        if (lvlupOpacity >= 1)
        {
        }
        else
        {
            setActiveToFalse();
        }
    }
    public void upgradeOrbingOrbs()
    {
        if (upO5 == false)
        {
            lvlupOpacity += -1;
        }
        if (upO5 == false && upO4 == true)
        {
            OrbB.transform.Find("underOrb").gameObject.SetActive(false);
            Orb.GetComponent<AbillityCoolDown>().coolDownDuration = 5;
            upO5 = true;
        }
        if (upO4 == false && upO3 == true)
        {
            Orb.GetComponent<AbillityCoolDown>().coolDownDuration = 6;
            upO4 = true;
        }
        if (upO3 == false && upO2 == true)
        {
            Orb.GetComponent<AbillityCoolDown>().coolDownDuration = 7;
            upO3 = true;
        }
        if (upO2 == false && upO1 == true)
        {
            Orb.GetComponent<AbillityCoolDown>().coolDownDuration = 8;
            upO2 = true;
        }
        if (upO1 == false)
        {
            Orb.GetComponent<AbillityCoolDown>().coolDownDuration = 9;
            Orb.GetComponent<AbillityCoolDown>().enabled = true;
            upO1 = true;
        }
        if (lvlupOpacity >= 1)
        {
        }
        else
        {
            setActiveToFalse();
        }
    }
    public void upgradeDash()
    {
        if (GameObject.Find("LvlCounterEnemy").GetComponent<LevelSystem>().ultReady == true)
        {
            if (upD3 == false)
            {
                lvlupOpacity += -1;
                GameObject.Find("LvlCounterEnemy").GetComponent<LevelSystem>().keepMeUp = false;
            }
            if (upD3 == false && upD2 == true)
            {
                DashB.transform.Find("underDash").gameObject.SetActive(false);
                Dash.GetComponent<AbillityCoolDown>().coolDownDuration = 80;
                upD3 = true;
            }
            if (upD2 == false && upD1 == true)
            {
                Dash.GetComponent<AbillityCoolDown>().coolDownDuration = 95;
                upD2 = true;
            }
            if (upD1 == false)
            {
                Dash.GetComponent<AbillityCoolDown>().coolDownDuration = 110;
                Dash.GetComponent<AbillityCoolDown>().enabled = true;
                upD1 = true;
            }
            if (lvlupOpacity >= 1)
            {
            }
            else
            {
                setActiveToFalse();
                GameObject.Find("LvlCounter").GetComponent<LevelSystem>().keepMeUp = false;
            }
        }
        GameObject.Find("LvlCounter").GetComponent<LevelSystem>().ultReady = false;
        if (GameObject.Find("LvlCounter").GetComponent<LevelSystem>().ultReady == false && GameObject.Find("LvlCounter").GetComponent<LevelSystem>().keepMeUp == false)
        {
            DashB.transform.Find("underDash").gameObject.SetActive(false);
        }
    }
}
