using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class MoveSettings
    {
        public float rotateVel;
        public float forwardVel;
        public float jumpVel;
        public float distanceToGround = 0.1f;
        public LayerMask ground;
    }
    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = 0.75f;
    }
    [System.Serializable]
    public class InputSettigns
    {
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
    }

    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettigns inputSetting = new InputSettigns();

    public Vector3 velocity = Vector3.zero;

    public HealthManager healthManager;

    public Animator anim;
    private Rigidbody rBody;

    private float inputH;
    private float inputV;
    public bool run;

    private bool jump;
    private bool isDead;
    private bool KickAttack;
    private bool slide;
    public float turnInput, jumpInput;
    public float forwardInput;

    public float speed;
    public float speedo;
    public Transform player;
    private EnergyManager energy;

    private int sideLTotal = 0;
    private int sideRTotal = 0;
    private int sideFTotal = 0;
    private float sideTimeDelay = 0;

    public bool dash;
    public bool dashR;
    public bool dashL;

    public LevelManager levelManager;

    public bool canMove;
    int damageToGive = 10;

    Quaternion targetRotation;
    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }
    private bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, moveSetting.distanceToGround, moveSetting.ground);
    }

    void Start()
    {
        canMove = true;
        dash = false;
        dashR = false;
        dashL = false;
        healthManager = FindObjectOfType<HealthManager>();
        levelManager = FindObjectOfType<LevelManager>();
        targetRotation = transform.rotation;
        rBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        run = false;

        forwardInput = turnInput = jumpInput = 0;
    }
    void Update()
    {
        Turn();
        if (canMove) { 
        if (dash == true)
        {
            transform.Translate(Vector3.forward * moveSetting.forwardVel * speedo * Time.smoothDeltaTime);
            Invoke("StopMoving", 1.15f);
        }
        if (dashR == true)
        {
            transform.Translate(Vector3.right * moveSetting.forwardVel * speedo * Time.smoothDeltaTime);
            Invoke("StopMoving", 1.15f);
        }
        if (dashL == true)
        {
            transform.Translate(Vector3.left * moveSetting.forwardVel * speedo * Time.smoothDeltaTime);
            Invoke("StopMoving", 1.15f);
        }
        if (Input.GetKeyDown("0"))
        {
            int n = UnityEngine.Random.Range(0, 2);
            if (n == 1)
            {
                anim.Play("WAIT01", -1, 0f);
            }
            else
            {
                anim.Play("WAIT03", -1, 0f);
            }
        }
            if (moveSetting.forwardVel < 4f)
        {
            if (Input.GetKey(KeyCode.R))
            {
                run = true;
                moveSetting.forwardVel = 3.5f;
            }
            else
            {
                run = false;
                moveSetting.forwardVel = 2f;
            }
        }
        if (forwardInput == -1)
        {
            moveSetting.forwardVel = 1f;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            sideLTotal += 1;
        }
        if ((sideLTotal == 1) && (sideTimeDelay < .3))
            sideTimeDelay += Time.deltaTime;

        if ((sideLTotal == 1) && (sideTimeDelay >= .3))
        {
            sideTimeDelay = 0;
            sideLTotal = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            sideRTotal += 1;
        }
        if ((sideRTotal == 1) && (sideTimeDelay < .3))
            sideTimeDelay += Time.deltaTime;

        if ((sideRTotal == 1) && (sideTimeDelay >= .3))
        {
            sideTimeDelay = 0;
            sideRTotal = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            sideFTotal += 1;
        }
        if ((sideFTotal == 1) && (sideTimeDelay < .3))
            sideTimeDelay += Time.deltaTime;

        if ((sideFTotal == 1) && (sideTimeDelay >= .3))
        {
            sideTimeDelay = 0;
            sideFTotal = 0;
        }

        if ((sideFTotal == 2) && (sideTimeDelay < .3) && EnergyManager.playerEnergy >= 10)
        {
            dash = true;
                EnergyManager.exhaustPlayer(100);
                Invoke("stopSlide", 1.15f);
            sideRTotal = 0;
            sideFTotal = 0;
            sideLTotal = 0;
            anim.Play("SLIDE00");
            anim.SetBool("Slide", true);
            slide = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(LateCall());
        }
        if ((sideLTotal == 2) && (sideTimeDelay < .3) && EnergyManager.playerEnergy >= 10)
        {
            dashL = true;
                EnergyManager.exhaustPlayer(100);
                Invoke("stopSlide", 1.15f);
            sideRTotal = 0;
            sideFTotal = 0;
            sideLTotal = 0;
            anim.Play("SLIDE00");
            anim.SetBool("Slide", true);
            slide = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(LateCall());
        }
        if ((sideRTotal == 2) && (sideTimeDelay < .3) && EnergyManager.playerEnergy >= 10)
        {
            dashR = true;
                EnergyManager.exhaustPlayer(100);
                Invoke("stopSlide", 1.15f);
            sideRTotal = 0;
            sideFTotal = 0;
            sideLTotal = 0;
            anim.Play("SLIDE00");
            anim.SetBool("Slide", true);
            slide = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(LateCall());
        }

        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        anim.SetFloat("InputH", turnInput);
        anim.SetFloat("InputV", forwardInput);
        anim.SetBool("Run", run);

        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            anim.SetBool("Jump", true);
            jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS);

            jump = true;

            Invoke("StopJumping", 0.5f);
        }
        else
        {
            jump = false;
            jumpInput = 0;
        }
        if (Input.GetKeyDown(KeyCode.V) && EnergyManager.playerEnergy >= 100)
        {
            dash = true;
                EnergyManager.exhaustPlayer(100);
                anim.SetBool("Slide", true);
            slide = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(LateCall());
        }
        else
        {
            slide = false;
        }
        anim.SetBool("Slide", slide);
    }
    }
    void StopMoving()
    {
        dash = false;
        dashL = false;
        dashR = false;
    }
    IEnumerator LateCall()
    {
        yield return new WaitForSecondsRealtime(1);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }
    void FixedUpdate()
    {
        Run();
        Jump();
        rBody.velocity = transform.TransformDirection(velocity);
    }
   public void Run()
    {
        Mathf.Abs(forwardInput);
        velocity.z = moveSetting.forwardVel * forwardInput;
    }
    void Turn()
    {
           if (turnInput != 0)
        {
            targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);
            transform.rotation = targetRotation;;
        }
    }
    void Jump()
    {
        if(jumpInput > 0 && Grounded())
        {
            velocity.y = moveSetting.jumpVel;
        }
        else if (jumpInput == 0 && Grounded())
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y -= physSetting.downAccel;
        }
    }
    void StopJumping()
    {
        anim.SetBool("Jump", false);
        jumpInput = 0;
    }
}
