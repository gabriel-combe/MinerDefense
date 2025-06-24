using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Properties")]
    [SerializeField]
    private int life = 10;
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float walkSpeed = 5f;

    private Transform target;

    private void Update()
    {
        if (target == null)
        {
            target = FindClosestBuilding();

            if (target != null)
                transform.position = Vector3.Lerp(transform.position, target.position, walkSpeed * Time.deltaTime);
        }
    }

    Transform FindClosestBuilding()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        Transform closest = null;
        float shortestDist = Mathf.Infinity;

        foreach (var building in buildings)
        {
            float dist = Vector3.Distance(transform.position, building.transform.position);
            if (dist < shortestDist)
            {
                shortestDist = dist;
                closest = building.transform;
            }
        }

        return closest;
    }

    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
            Destroy(gameObject);
    }

    // On collision the enemy with a building the enemy is destroyed and the building take some damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Building")) return;
        
        BuildingObject building = collision.gameObject.GetComponent<BuildingObject>();
        building.TakeDamage(damage);

        Destroy(gameObject);
    }
}
