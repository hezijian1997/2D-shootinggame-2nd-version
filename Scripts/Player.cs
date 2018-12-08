using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration parameters
    [Header("Player Setting")]
    [SerializeField] float speed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 500;

    [Header("Laser Setting")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float laserFiringPeriod = 0.1f;

    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0,1)] float shootSoundVolume = 0.2f;

    Coroutine firingCoroutine;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

	// Use this for initialization
	void Start ()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update ()
    {
        Move();
        Fire();
        
    }

    // build collision to get attacked
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageFromEnemy = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageFromEnemy)
        {
            return;
        }
        ProcessHit(damageFromEnemy);
    }

    // Get hit by enemy and dead
    private void ProcessHit(DamageDealer damageFromEnemy)
    {
        health -= damageFromEnemy.GetDamage();
        damageFromEnemy.Hit();
        if (health <= 0)
        {
            Dead();
            FindObjectOfType<Level>().LoadGameOver();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    private void Dead()
    {
        Destroy(gameObject);
        ExplosionParticle();
    }

    private void ExplosionParticle()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 0.55f);
    }
    // Fire the Lazer
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    // Make the mouse click once and continue the laser
    IEnumerator FireContinuously()
    {
        while (true)
        {
            // copy gameobject prefabs of laser and make them one gameobject
            GameObject laser = Instantiate
                (laserPrefab, transform.position, Quaternion.identity)
                as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);


            // Give laser sound
            AudioSource.PlayClipAtPoint(shootSound, 
                Camera.main.transform.position, 
                shootSoundVolume);

            yield return new WaitForSeconds(laserFiringPeriod);
        }
    }
        // Move the Spaceship
        private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
        
    }

    // Set up camera range to stop Spaceship moving over the screen horizontally and vertically
    private void SetUpMoveBoundaries()
    {
        Camera camera = Camera.main;
        xMin = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    
}
