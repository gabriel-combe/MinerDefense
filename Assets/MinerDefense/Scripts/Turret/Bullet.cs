using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int bulletDamage;

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime); 
    }

    public void SetBulletDamage(int damage)
    {
        bulletDamage = damage;
    }

    // On collision with an enemy the bullet is destroyed and the enemy take some damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
        if (!collision.collider.CompareTag("Enemy")) return;
        
        Enemy enemy = collision.transform.GetComponent<Enemy>();

        enemy.TakeDamage(bulletDamage);

        Destroy(this.gameObject);
    }
}
