using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxEnemyHealth;
    public int enemyHealth;
    private Slider healthBar;
    public int regen = 3;
    public int baseRegen = 3;

    public GameObject deathEffect;

    public int DmgOverTime;
    private bool dodmg;
    public bool Coin;
    public bool dieOnce = false;
    Animator anim;

    public Collider cool;

    public GameObject tim;
    public bool notActive;

    public Transform coinFire;
    private Transform playerCenter;

    public float respawnTime;
    public float t;
    public Transform respawnHereR;
    public Transform respawnHereB;

    public bool IAmBlue;
    public bool IAmRed;

    public ClickOnMe clickMe;

    public int GiveExp;

    GameObject playClick;

    public bool moneyToB;
    public bool moneyToR;
    public int GiveMoney;

    public int baseDmg;

    public bool yesAnotherOne;

    Text text;

    void Awake()
    {
        if(tag == "EnemyRed")
        {
            playClick = GameObject.Find("Champ");
        }
        else
        {
            playClick = GameObject.Find("ChampEnemy");
        }

        if (name == "Range(Clone)" || name == "SuperRange")
        {
            name = "Range";
        }
        else if (name == "Minion(Clone)" || name == "SuperMinion")
        {
            name = "Minion";
        }
        if (name == "TowerShot B(Clone)") // why is this called if it has already found it´s name??
        {
            name = "TowerShot B";
        }
        else if (name == "TowerShot R(Clone)")
        {
            name = "TowerShot R";
        }
    }
    void Start ()
    {
        if (name == "Champ")
        {
            t = Time.time;
        }
        baseDmg = 70;

        if (name == "TowerShot B" || name == "TowerShot R")
        {
        }
        else
        {
            healthBar = transform.FindChild("EnemyCanvas").FindChild("HealthBack").FindChild("HealthBar").GetComponent<Slider>();
        }
        clickMe = GetComponent<ClickOnMe>();
        tim = GameObject.Find("Timer_Spawner");
        anim = GetComponent<Animator>();
        dodmg = false;
        playerCenter = transform.FindChild("Center");
        DmgTextController.Initialize();
	}
    void callme()
    {
        GetComponent<MinonAI>().enabled = true;
    }
    void tp()
    {
        if(tag == "EnemyRed")
        {
            enemyHealth = maxEnemyHealth;
            transform.position = respawnHereR.transform.position;
            GetComponent<PlayerClickToMove>().targetPosition = respawnHereR.transform.position;
        }
        else
        {
            enemyHealth = maxEnemyHealth;
            transform.position = respawnHereB.transform.position;
            GetComponent<PlayerClickToMove>().targetPosition = respawnHereB.transform.position;
        }
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
            r.enabled = true;
    }
    void Respawn()
    {
        anim.Play("WAIT00");
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
            r.enabled = false;
        GetComponent<PlayerClickToMove>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
        cool.enabled = true;
        GetComponent<Rigidbody>().useGravity = false;
        Coin = false;
        dieOnce = false;
        if (IAmBlue == true)
        {
            tag = "EnemyBlue";
            playerCenter.tag = "PlayerCenterBlue";
            transform.position = respawnHereB.transform.position;
            Invoke("tp", 0.1f); 
            IAmBlue = false;
        }
        else if (IAmRed == true)
        {
            tag = "EnemyRed";
            playerCenter.tag = "PlayerCenterRed";
            transform.position = respawnHereR.transform.position;
            Invoke("tp", 0.1f);
            IAmRed = false;
        }
    }
    void HealthRegen()
    {
        enemyHealth += baseRegen + regen;
    }

    public void Update()
    {
        if(enemyHealth >= maxEnemyHealth) // call this when health is lower then max and dont call it when health is max
        {
            CancelInvoke("HealthRegen");
            enemyHealth = maxEnemyHealth;
        }

        if (dieOnce == true) // this is checked every frame!! call function instead
        {
             dieOnce = false;
        }
            healthBar.value = enemyHealth; // only call it when it is changed or it will be called every frame wich is waste of perfomance. It is used very frequently but yes!
        
        if (name == "Minion" || name == "Range") // this is checking name every frame! dont
        { // try to not call getcomponent in update! actually this should just be called in start
            if (GetComponent<MinonAI>().waypoints.ElementAt<GameObject>(1) != null && notActive == false)
            {
                notActive = true;
                Invoke("callme", 0.5f);
            }
        }
        if (enemyHealth <= maxEnemyHealth && name == "Champ" && yesAnotherOne == false) // this is also checking name every frame!
        {
            yesAnotherOne = true; // when this is called it will keep being tru for ever cause seting it to false will make it call
             //it again it will make to many calls and it will go over maxHealth
            InvokeRepeating("HealthRegen", 0f, 2f);
        }
        if (enemyHealth <= 0 && Coin == false && dieOnce == false) // this could possible be better but roughly ok!
        {
            Coin = true;
            dieOnce = true;
            if (name == "Champ") // dont check names in update..
            {
                GetComponent<PlayerClickToMove>().canMove = true; // nonono
                if (tag == "EnemyBlue") // nonono
                {
                    IAmBlue = true;
                }
                else
                    IAmRed = true;
            }
            // tag = "Untagged";     the point center is changed so you wont have a target! 
            // maybe not all projectiles are up to date, so it might be a problem
            if (name != "Tower" && name != "Inhib" && name != "Nexus") // no godanm it! 
            {
                playerCenter.tag = "Untagged"; // this is fine
            }
            enemyHealth = 0;
            Coin = true;
            coin();
            if(name == "Tower") // nope
            {
                Invoke("DieAnim", 2f);
            }
            if(name == "Champ") // nope
            {
                anim.Play("LOSE00");
                respawnTime = t / 5f + 1f;   // t/ 5f +10f
                Invoke("Respawn", respawnTime);
                GetComponent<PlayerClickToMove>().enabled = false; // dont use getcomponent in update
                GetComponent<NavMeshAgent>().enabled = false; // dont use getcomponent in update
                cool.enabled = false;
                GetComponent<Rigidbody>().useGravity = true; // dont use getcomponent in update
            }
            else
            {
                clickMe.enabled = false;
                Invoke("DieAnim", 5f);
            }
           // GetComponent<Collider>().enabled = false;
            //Instantiate(deathEffect, transform.position, transform.rotation);
        }
        if (gameObject.name == "tower") // no comment
        {
            if (maxEnemyHealth >= enemyHealth * 2)
            {

            }
            if (maxEnemyHealth >= enemyHealth * 4)
            {

            }
            if (enemyHealth <= 0)
            {
                // do effect and stuff
            }
        }
        if(gameObject.name == "Inhib") // NO
        {
            if (enemyHealth <= 0)
            {
                tim.GetComponent<Timer>().spawnSuperMinion = true;
                if (tag == "EnemyRedTower") // this is partly fine
                {
                    tim.GetComponent<Timer>().superMinionsR = true;
                }
                if (tag == "EnemyBlueTower")
                {
                    tim.GetComponent<Timer>().superMinionsB = true;
                }
            }
        }
        if (gameObject.name == "Nexus") // No you fool
        {

            if (enemyHealth <= 0)
            {
                text = GameObject.Find("WinnerText").GetComponent<Text>(); // not sure if this is best for perfomance! it is called when needed and it will only be called once
                if(gameObject.tag == "EnemyRedTower")                      // like it would be if it was called in start! but alot of things will be called when the game starts
                {                                                         // like many start functions will be called at the same time and it will make the start up quicker
                    text.text = "Blue Team wins";                        // if things that are called once is not called at start and pile up to make a slow game start!!
                }
                else
                {
                    text.text = "Red Team wins";
                }
                Time.timeScale = 0;
                // do effect and stuff
                // you WIN!!!!!! Sorry, but no
            }
        }
        if (dodmg == true) // this is checked when true!! maybe call function instead
        {
            enemyHealth -= DmgOverTime * (int)Time.deltaTime;
            Invoke("stopDmg", 5); // is Invoke better then IEnumerator? whats the diference? Invoke is a bit easier to use and can be used evry where
        }
    }
    void coin()
    {       
        Coin = true; // remember to distinguish between red and blue!  why do i need a bool? aint this fucntion called once? maybe the bool should be else where
        var dist = Vector3.Distance(playClick.transform.position, transform.position);

        if(tag == "EnemyRed") // Nay
        {
            if (moneyToB == true) // Nay
            {
                Debug.Log("give money once pr kill");
                moneyToB = false;
                LevelSystem.UpdateMoney(GiveMoney);
                ScoreManager.AddPoints(GiveMoney);
            }
        }

        if (tag == "EnemyBlue")
        {
            if (moneyToR == true)
            {
                Debug.Log("give money once pr kill");
                moneyToR = false;
                LevelSystem.UpdateMoney(GiveMoney);
                ScoreManager.AddPoints(GiveMoney);
            }
        }

        if (dist <= 20) // I think this would be a well fine thing to do
        {
            if(tag == "EnemyRed") // Nay
            {
                LevelSystem.UpdateXP(GiveExp); // good
            }
            else
            {
                LvlSystemForAi.UpdateXP(GiveExp); // good
            }
        }
        Instantiate(deathEffect, coinFire.position, coinFire.rotation);
    }
    void stopDmg()
    {
        dodmg = false; // a bit of a waste of space perhabs?
    }
    public virtual void giveDamage(int damageToGive)
    {
        if(name == "TowerShot B" || name == "TowerShot R") // Nay I say
        {
            enemyHealth -= damageToGive; // oh yeah this gives dmg to something that shouldn´t even have a health system!!! Great for perfomance....
        }
        else
        {
            enemyHealth -= damageToGive;
            DmgTextController.CreateFloatingText(damageToGive.ToString(), transform);
        }         
    }
    public virtual void giveDamageAtAoe(int damageToGive)
    {
        //enemyHealth -= damageToGive;
        dodmg = true; // ok..
    }
    void dmg(int damageToGive)
    {
        Debug.Log("continue dmg");
        enemyHealth -= damageToGive;
        DmgTextController.CreateFloatingText(damageToGive.ToString(), transform);
    }
    void DieAnim()
    {
        Destroy(gameObject);
    }
}