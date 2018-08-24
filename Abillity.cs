using UnityEngine;
using System.Collections;

public abstract class Abillity : MonoBehaviour
{
    private string aName = "New Abillity";
    public Sprite aSprite;
    //public AudioClip aSound;
    public Transform target;
    public float speed;

    public float shootRateBall;
    public float shootRateTimeStamp;
    public Transform magicBallFirePoint;
    public Transform MinorOrb0;
    // public Transform kickFirePoint;
    //public GameObject Kick;
    //public bool KickAttack;
    public bool DeadAnim;
    public int damageToGive;
    public int pointsForKill;

    public GameObject magicBall;
    public GameObject charm;
    public GameObject minorObs;
    public GameObject minorObsUltimate;
    public GameObject dash;

    public Animator anim;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbillity();

    Player playerController;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        // damageToGive = Random.Range(6, 15);

        if (Random.Range(1, 101) == 100)
        {
            damageToGive = Random.Range(150, 200);
        }
        else if (Random.Range(1, 105) == 101)
        {
            damageToGive = 0;
        }
        else
        {
            damageToGive = Random.Range(105, 107);
        }
    }
}
