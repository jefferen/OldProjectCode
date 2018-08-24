using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    // public GameObject currentCheckPoint;
    public GameObject BlueChampSpawnPoint;
    public GameObject RedChampSpawnPoint;
    private Player player;

   // public GameObject deathEffect;
   // public GameObject RespawnEffect;

    public float respawnDelay;
    private new CameraControl camera;
    private HealthManager healthManager;
    private EnergyManager energyManager;
    private Animator anim;

    void Start ()
    {
        player = FindObjectOfType<Player>();

        camera = FindObjectOfType<CameraControl>();

        energyManager = FindObjectOfType<EnergyManager>();

        healthManager = FindObjectOfType<HealthManager>();

        anim = GetComponent<Animator>();

    }
	
	void Update ()
    {
	
	}
    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }
    public IEnumerator RespawnPlayerCo()
    {
        //Instantiate(deathEffect, player.transform.position, player.transform.rotation);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.enabled = false;
        //player.renderer.enabled = false;
        //camera.isFollowing = false;
        yield return new WaitForSeconds(respawnDelay);
        if(player.tag == "EnemyRed")
        {
            player.transform.position = RedChampSpawnPoint.transform.position;
        }
        if (player.tag == "EnemyBlue")
        {
            player.transform.position = BlueChampSpawnPoint.transform.position;
        }
        //player.transform.position = currentCheckPoint.transform.position;
        player.enabled = true;
        //player.renderer.enabled = true;
        //camera.isFollowing = true;
        energyManager.FullEnergy();
        healthManager.FullHealth();
        healthManager.isDead = false;
        // Instantiate(RespawnEffect, currentCheckPoint.transform.position, currentCheckPoint.transform.rotation);
    }
}
