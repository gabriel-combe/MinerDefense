using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 0.25f;

    private int bulletDamage;

    private Transform target;
    private Vector3 targetDirection;

    // Update is called once per frame
    void Update()
    {
        transform.position += targetDirection * bulletSpeed * Time.deltaTime;
    }

    public void SetBulletDamage(int damage)
    {
        bulletDamage = damage;
    }

    public void SetBulletTarget(Transform target)
    {
        this.target = target;
        targetDirection = (target.position - transform.position).normalized;
    }

    // On collision with an enemy the bullet is destroyed and the enemy take some damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        
        Enemy enemy = collision.transform.GetComponent<Enemy>();

        enemy.TakeDamage(bulletDamage);

        Destroy(gameObject);
    }
}
