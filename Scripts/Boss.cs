using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField] float health = 500f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.1f;
    [SerializeField] float maxTimeBetweenShots = 2f;
    [SerializeField] GameObject laserBossIndex;
    [SerializeField] GameObject laserBossIndex2;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.2f;
    [SerializeField] int scoreValue = 100;
    //[SerializeField] AudioClip deathSFX;
    //[SerializeField] float deathSoundVolume = 1f;

    // Use this for initialization
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserBossIndex, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
        AudioSource.PlayClipAtPoint(shootSound,
            Camera.main.transform.position,
            shootSoundVolume);
    }

    // give collision to enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    // decrease health when the lazer touches the above collision
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
            FindObjectOfType<Level>().WinScene();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddScore(scoreValue);
        Destroy(gameObject);
        ExplosionParticle();
    }

    private void ExplosionParticle()
    {
        GameObject explosionParticle = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosionParticle, 0.55f);
        //AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
    }

}
