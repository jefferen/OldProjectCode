using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbillityCoolDown : MonoBehaviour
{
    public string abillityButtonName;
    public Image darkMask;
    public Text coolDownTextDisplay;

    [SerializeField] public Abillity abillity;
    [SerializeField] private GameObject weaponHolder;
    private Image myButtonImage;
   // private AudioSource abillitySource;
    public float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    private bool ult;
    private bool ultSecond;

    public float currentCool;

    bool newbool;
    public GameObject spellIndi;

    public bool qReady;
    public bool eReady;

    void Start ()
    {
        spellIndi = GameObject.Find("SpellIndicator");
        Initialize(abillity, weaponHolder);
	}
    public void Initialize(Abillity selectedAbillity, GameObject weaponHolder)
    {
        abillity = selectedAbillity;
        myButtonImage = GetComponent<Image>();
        //abillitySource = GetComponent<AudioSource>();
        myButtonImage.sprite = abillity.aSprite;
        // coolDownDuration = abillity.shootRateBall; // check cooldownduratioon is important but what about shootrateBall??
        abillity.Initialize(weaponHolder);
        AbillityReady();
    }
    public void Upgrade()
    {
        Debug.Log("Upgrade in abillitycooldown is called! why? is it necesary!!! it makes no sense to me! if this shows, FIGURE IT OUT");
        GetComponent<MagicBallAbillity>().damageToGive = GetComponent<MagicBallAbillity>().damageToGive * 2; // cant change prefab at runtime
    }
    void Update()
    {
        if(spellIndi.GetComponent<SpellIndicator>().GetComponentInChildren<Light>().enabled == false)
        {
            qReady = false;
            eReady = false;
        }
        if (Input.GetKeyDown(KeyCode.Q) && !Input.GetKey(KeyCode.LeftControl))
        {
            if (abillityButtonName == "q")
            {
                spellIndi.GetComponent<SpellIndicator>().QPressed();
                if (spellIndi.GetComponent<SpellIndicator>().GetComponentInChildren<Light>().enabled == true)
                {
                    qReady = true;
                }
            }
        }
        //  E
        if (Input.GetKeyDown(KeyCode.E) && !Input.GetKey(KeyCode.LeftControl))
        {
            if (abillityButtonName == "e")
            {
                spellIndi.GetComponent<SpellIndicator>().EPressed();
                if (spellIndi.GetComponent<SpellIndicator>().GetComponentInChildren<Light>().enabled == true)
                {
                    eReady = true;
                }
            }
        }

        bool coolDownComplete = (Time.time > nextReadyTime);
        if (coolDownComplete)
        {
            AbillityReady();
            // Shoot
            if (spellIndi.GetComponent<SpellIndicator>().GetComponentInChildren<Light>().enabled == true && Input.GetMouseButtonDown(0))
            {
                if (abillityButtonName == "q")
                {
                    if (qReady == true)
                    {
                        ButtonTriggered();
                        qReady = false;
                        spellIndi.GetComponent<SpellIndicator>().GetComponentInChildren<Light>().enabled = false;
                    }
                }
                if (abillityButtonName == "e")
                {
                    if (eReady == true)
                    {
                        ButtonTriggered();
                        eReady = false;
                        spellIndi.GetComponent<SpellIndicator>().GetComponentInChildren<Light>().enabled = false;
                    }
                }
            }
            //   R
            if (Input.GetKeyDown(abillityButtonName) && !Input.GetMouseButtonDown(0) && Input.GetKeyDown(KeyCode.R) && ult == false && ultSecond == false && !Input.GetKey(KeyCode.LeftControl))
            {
                newbool = false;
                Invoke("Bool", 0.2f);
                Invoke("Reset", 16f);
                if (newbool == true)
                {
                }
                else
                {
                    coolDownDuration = 1.2f;
                }
            }
            if (Input.GetKeyDown(abillityButtonName) && !Input.GetMouseButtonDown(0) && Input.GetKeyDown(KeyCode.R) && ult == true && ultSecond == false && !Input.GetKey(KeyCode.LeftControl))
            {
                Invoke("BoolSecond", 0.2f);
                if(newbool == true)
                {
                }
                else
                {
                    coolDownDuration = 1.2f;
                }
            }
            if (Input.GetKeyDown(abillityButtonName) && !Input.GetMouseButtonDown(0) && Input.GetKeyDown(KeyCode.R) && ult == true && ultSecond == true && !Input.GetKey(KeyCode.LeftControl))
            {
                CancelInvoke("Reset");
                ult = false; ultSecond = false;
                coolDownDuration = currentCool;
            }
            if (Input.GetKeyDown(abillityButtonName) && !Input.GetMouseButtonDown(0) && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.R)) && !Input.GetKey(KeyCode.LeftControl))
            {
                ButtonTriggered();
            }
        }
         else
         {
             CoolDown();
         } 
    }
    void Reset()
    {
        ult = false; ultSecond = false;
        coolDownDuration = currentCool;
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;
        newbool = true;
    }
    void Bool()
    {
        ult = true;
    }
    void BoolSecond()
    {
        ultSecond = true;
    }

    private void AbillityReady()
    {
        coolDownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        coolDownTextDisplay.text = roundedCd.ToString();
        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }

    public void ButtonTriggered()
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;

        //abillitySource.clip = abillity.aSound;
        //abillitySource.Play();
          abillity.TriggerAbillity();         
    }
}