using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Goes up and down. Fires forward when there are targets.
/// </summary>
public class Monster : MonoBehaviour
{

    public float verticalSpeed = 1f;
    public bool facingRight = true;
    public Transform shootPoint;
    public Projectile projectilePrefab;
    public float projectileForce = 100f;
    public float fireRate = 0.5f;
    public float monsterMoveDistance = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 originalPosition;
    public float direction = 1;
    private HashSet<Player> targets = new HashSet<Player>();

    private IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalPosition = transform.position;
        var delay = new WaitForSeconds(fireRate);
        while (true)
        {
            if (targets.Count > 0) Fire();
            yield return delay;
        }
    }

    private void Update()
    {
        if (targets.Count == 0)
        {
            if (Mathf.Abs(transform.position.y - originalPosition.y) > monsterMoveDistance)
            {
                direction *= -1;
            }
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y + Time.deltaTime * direction * verticalSpeed));
        }
    }

    private void Fire()
    {
        GameSoundManager.instance.PlayFire();
        if (animator != null) animator.SetTrigger("Fire");
        var projectile = Instantiate<Projectile>(projectilePrefab, shootPoint.position, shootPoint.rotation);
        var force = projectileForce * new Vector3(facingRight ? 1 : -1, 0, 0);
        projectile.GetComponent<Rigidbody2D>().AddForce(force);
        projectile.monster = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            // Target the player:
            targets.Add(player);
            Fire();
        }
        else if (collision.GetComponent<Killzone>() != null)
        {
            // If hit killzone, respawn:
            transform.position = originalPosition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            targets.Remove(player);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = Mathf.Sign(transform.position.y - collision.transform.position.y);
    }

}
