using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f; // time between shots in seconds

    private float nextFireTime = 0f;
    private SFXManager sfxManager;
    private bool autoFireEnabled = false;
    public float autoFireTime = 5f; // default autofire time is 5 seconds
    private float autoFireEndTime = 0f;

    // DELETE AFTER POWERUP CREATED
    private float timeStart;
    private bool firstTime;

    private void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();

        // DELETE AFTER POWERUP CREATED
        timeStart = Time.time;
        firstTime = true;
    }

    void Update()
    {
        if (autoFireEnabled && Time.time < autoFireEndTime) // autofire mode
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
        else // normal firing mode
        {
            // check if there are any bullets on screen before firing another
            if (GameObject.FindGameObjectWithTag("Bullet") == null && Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }

        // DELETE AFTER POWERUP CREATED
        if (Time.time > timeStart + 10 && firstTime)
        {
            EnableAutoFire(autoFireTime);
            Debug.Log("Enabling autofire");
            firstTime = !firstTime;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        sfxManager.Play("Shoot");
    }

    public void EnableAutoFire(float time)
    {
        autoFireEnabled = true;
        autoFireTime = time;
        autoFireEndTime = Time.time + time;
        nextFireTime = Time.time;
    }

    // DELETE AFTER POWERUP CREATED (Probably...)
    public void DisableAutoFire()
    {
        autoFireEnabled = false;
    }
}
