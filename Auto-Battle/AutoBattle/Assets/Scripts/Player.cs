using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public Identity playerIdentity;
    public Transform mainTarget;
    public LayerMask enemyMask;
    
    private Collider[] enemies;
    private GameObject child;
    private Transform currentEnemy;
    private float attackRadius;
    private float attackSpeed;
    private float health;
    private float spawnTime;

    private float radius = 6f;
    private float attackDamage;
    private float nextAttackTime;

    private HealthBar healthBar;

    private void Start()    
    {
        child = transform.GetChild(0).gameObject;
        mainTarget = GameObject.FindGameObjectWithTag("EnemyTower").transform;
        healthBar = GetComponentInChildren<HealthBar>();

        child.GetComponent<SpriteRenderer>().sprite = playerIdentity.artwork;
        attackDamage = playerIdentity.attack;
        attackSpeed = playerIdentity.attackSpeed;
        attackRadius = playerIdentity.attackRadius;
        health = playerIdentity.health;
        spawnTime = playerIdentity.spawnTime;
        speed = playerIdentity.speed;
        target = mainTarget;

        healthBar.setMaxHealth((int)health);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(updatePath());
        }
        
        enemies = checkEnemyInRadius(radius);

        if (enemies.Length > 0)
        {
            currentEnemy = getClosestGameObject(enemies);
            if(Vector3.Distance(transform.position,currentEnemy.position) < attackRadius)
            {
                if(Time.time >= nextAttackTime)
                {
                    attack(currentEnemy.gameObject);
                    nextAttackTime = Time.time + 1f / attackSpeed;
                }
            }
            else
            {
                speed = playerIdentity.speed;
            }
            target = currentEnemy;
        }
        else
        {
            target = mainTarget;
            speed = playerIdentity.speed;
        }

        if(health <= 0)
        {
            dead();
        }
    }

    private Collider[] checkEnemyInRadius(float _radius)
    {
        return Physics.OverlapSphere(transform.position, _radius, enemyMask);
    }

    private void attack(GameObject enemy)
    {
        speed = 0;
        if(enemy != null)
        {
            enemy.GetComponent<Enemy>().subtractHealth(attackDamage);
        }
    }

    public void subtractHealth(float damage)
    {
        health -= damage;
        healthBar.setHealth((int)health);
    }

    void dead()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
