using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : BuildingObject
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletSpawnPoint;

    // Turret Properties
    private int damage;
    private float attackSpeed;
    private float attackCooldown = 0f;

    // Boolean to activate or deactivate the turret
    private bool isTurretActive = false;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        damage = gameManager.GetTurretDamage();
        attackSpeed = gameManager.GetTurretAttackSpeed();
    }

    public override void OnPlaced()
    {
        isTurretActive = true;
    }

    public override void OnMoved()
    {
        isTurretActive = false;
    }

    private void Update()
    {
        if (!isTurretActive) return;

        attackCooldown -= Time.deltaTime;

        if (attackCooldown < 0f)
        {
            Transform target = FindClosestEnemy();
            if (target != null)
            {
                Shoot(target);
                attackCooldown = 1f / attackSpeed;
            }
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform closest = null;
        float shortestDist = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < shortestDist)
            {
                shortestDist = dist;
                closest = enemy.transform;
            }
        }

        return closest;
    }

    void Shoot(Transform target)
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.target = target;

        Destroy(bullet.gameObject, 8f);
    }
}
