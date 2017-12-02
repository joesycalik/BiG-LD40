using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    public int player;
    public Transform feet;
    public Transform shootPoint;
    public Transform gemPoint;
    public bool facingRight = true;
    public float speedMultiplier = 1f;
    public float maxRunSpeed = 10f;
    public float jumpForce = 400f;
    public LayerMask groundLayer = 1;
    public Projectile projectilePrefab;
    public float projectileForce = 100f;
    public float fireRate = 2;
    public List<Gem> gems = new List<Gem>();

    public string horizontalName { get { return "Player" + player + "Horizontal"; } }
    public string verticalName { get { return "Player" + player + "Vertical"; } }
    public string jumpButtonName { get { return "Player" + player + "Jump"; } }
    public string fireButtonName { get { return "Player" + player + "Fire1"; } }

    private const float GroundCheckRadius = 0.2f;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator animator;
    private bool jump = false;
    private bool fire = false;
    private float nextFireTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (rb == null) Debug.Log(name + " missing Rigidbody2D", this);
        if (animator == null) Debug.Log(name + " missing Animator", this);
        if (feet == null) Debug.Log(name + " assign Feet", this);
        if (shootPoint == null) Debug.Log(name + " assign Feet", this);
        if (projectilePrefab == null) Debug.Log(name + " assign Projectile Prefab", this);
    }

    private void Update()
    {
        if (!jump) jump = Input.GetButtonDown(jumpButtonName);
        if (!fire) fire = Input.GetButtonDown(fireButtonName) || (Input.GetAxis(fireButtonName) > 0);
    }

    private void FixedUpdate()
    {
        isGrounded = CheckGround();
        if (animator != null) animator.SetFloat("vSpeed", rb.velocity.y);
        var horizontal = Input.GetAxis(horizontalName);
        Move(horizontal, jump);
        if (fire) Fire();
        jump = false;
        fire = false;
    }

    private bool CheckGround()
    {
        var colliders = Physics2D.OverlapCircleAll(feet.position, GroundCheckRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject) return true;
        }
        return false;
    }

    public void Move(float moveSpeed, bool jump)
    {
        if (isGrounded)
        {
            // Move:
            moveSpeed *= speedMultiplier;
            if (animator != null) animator.SetFloat("hSpeed", Mathf.Abs(moveSpeed));
            rb.velocity = new Vector2(moveSpeed * maxRunSpeed, rb.velocity.y);
            if ((moveSpeed > 0 && !facingRight) || (moveSpeed < 0 && facingRight))
            {
                FlipSprite(); // Changed direction.
            }
        }
        if (isGrounded && jump)
        {
            // Jump:
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }

    private void FlipSprite()
    {
        facingRight = !facingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            if (animator != null) animator.SetTrigger("Fire");
            var projectile = Instantiate<Projectile>(projectilePrefab, shootPoint);
            projectile.player = this;
            var force = projectileForce * new Vector3(facingRight ? 1 : -1, 0, 0);
            projectile.GetComponent<Rigidbody2D>().AddForce(force);
            nextFireTime = Time.time + fireRate;
        }
    }

    public void GainGem(Gem gem)
    {
        gems.Add(gem);
        gem.transform.SetParent(gemPoint);
        gem.transform.localPosition = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(0, 0.1f), 0);
        //gem.Hold();

    }

    public void LoseGem()
    {
        if (gems.Count == 0) return;
        var gem = gems[0];
        gems.RemoveAt(0);
        gem.isHeld = false;
        //gem
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem")){
            other.gameObject.SetActive(false);
        }
    }

}
