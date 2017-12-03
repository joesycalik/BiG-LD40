using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public int playerID;
    public SpriteRenderer tintableSprite;
    public Transform feet;
    public Transform shootPoint;
    public Transform gemPoint;
    public bool facingRight = true;
    public float currentSpeedMultiplier = 1f;
    public float maxRunSpeed = 10f;
    public float jumpForce = 400f;
    public LayerMask groundLayer = 1;
    public Projectile projectilePrefab;
    public float projectileForce = 100f;
    public float fireRate = 2;
    public List<Gem> gems = new List<Gem>();
    public AnimationCurve gemSpeedCurve = new AnimationCurve(
        new Keyframe(0, 1),
        new Keyframe(1, 0.8f),
        new Keyframe(5, 0.5f));
    public float hitPauseDuration = 1;

    public string horizontalName { get { return "Player" + playerID + "Horizontal"; } }
    public string verticalName { get { return "Player" + playerID + "Vertical"; } }
    public string jumpButtonName { get { return "Player" + playerID + "Jump"; } }
    public string fireButtonName { get { return "Player" + playerID + "Fire1"; } }

    private const float GroundCheckRadius = 0.2f;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator animator;
    private bool jump = false;
    private int jumpsLeft = 1;
    private bool fire = false;
    private float nextFireTime;
    private float pauseTimeLeft = 0;
    private bool hasSetPlayerNumber = false;
    private LevelUI levelUI;
    private ParticleSystem particleSystem;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        if (rb == null) Debug.Log(name + " missing Rigidbody2D", this);
        if (animator == null) Debug.Log(name + " missing Animator", this);
        if (feet == null) Debug.Log(name + " assign Feet", this);
        if (shootPoint == null) Debug.Log(name + " assign Feet", this);
        if (projectilePrefab == null) Debug.Log(name + " assign Projectile Prefab", this);
        
    }

    private void Start()
    {
        levelUI = FindObjectOfType<LevelUI>();
        if (!hasSetPlayerNumber) SetPlayer(playerID);
    }

    public void SetPlayer(int playerNumber)
    {
        hasSetPlayerNumber = true;
        playerID = playerNumber;
        if (tintableSprite != null) tintableSprite.color = GetPlayerColor(playerNumber);
    }

    private Color GetPlayerColor(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1: return Color.blue;
            case 2: return Color.red;
            case 3: return Color.green;
            case 4: return Color.yellow;
            default: return Color.magenta;
        }
    }

    private void Update()
    {
        if (!jump) jump = Input.GetButtonDown(jumpButtonName);
        if (!fire) fire = (playerID == 1 && Input.GetButtonDown(fireButtonName)) || (Mathf.Abs(Input.GetAxis(fireButtonName)) > 0);
    }

    private void FixedUpdate()
    {
        if (pauseTimeLeft > 0)
        {
            pauseTimeLeft -= Time.deltaTime;
            if (pauseTimeLeft > 0) return;
        }
        var wasGrounded = isGrounded;
        isGrounded = CheckGround();
        if (isGrounded && !wasGrounded) jumpsLeft = 1;
        if (animator != null) animator.SetFloat("vSpeed", rb.velocity.y);
        horizontal = Input.GetAxis(horizontalName);
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

    //// Debug:
    private float horizontal;
    private float moveSpeed;
    //private void OnGUI()
    //{
    //    GUILayout.Label("moveSpeed=" + moveSpeed + ", horiz=" + horizontal);
    //}

    public void Move(float moveSpeed, bool jump)
    {
        this.moveSpeed = moveSpeed;
        if (isGrounded || Mathf.Abs(moveSpeed) >= 0.1f)
        {
            // Move:
            moveSpeed *= currentSpeedMultiplier;
            if (!isGrounded) moveSpeed *= 0.5f;
            if (animator != null) animator.SetFloat("Speed", Mathf.Abs(moveSpeed));
            rb.velocity = new Vector2(moveSpeed * maxRunSpeed, rb.velocity.y);
            if ((moveSpeed > 0 && !facingRight) || (moveSpeed < 0 && facingRight))
            {
                FlipSprite(); // Changed direction.
            }
        }
        if (jump && (isGrounded || jumpsLeft > 0))
        {
            // Jump:
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce * currentSpeedMultiplier));
            jumpsLeft--;
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
        if (Time.time >= nextFireTime) // && gems.Count > 0)
        {
            particleSystem.Play();
            if (animator != null) animator.SetTrigger("Fire");
            var projectile = Instantiate<Projectile>(projectilePrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            projectile.player = this;
            var force = projectileForce * new Vector3(facingRight ? 1 : -1, 0, 0);
            projectile.GetComponent<Rigidbody2D>().AddForce(force);
            nextFireTime = Time.time + fireRate;
        }
    }

    public void GetHit()
    {
        if (animator != null) animator.SetTrigger("Hit");
        LoseGem();
        pauseTimeLeft = hitPauseDuration;
    }

    public void GainGem(Gem gem)
    {
        if (gem == null || !gem.isAvailable) return;
        gems.Add(gem);
        gem.HoldBy(gemPoint);
        UpdateSpeedMultiplier();
        levelUI.GemCounts[playerID - 1].text = gems.Count.ToString();
    }

    public void LoseGem()
    {
        if (gems.Count == 0) return;
        var gem = gems[0];
        gems.RemoveAt(0);
        gem.Release();
        UpdateSpeedMultiplier();
        levelUI.GemCounts[playerID - 1].text = gems.Count.ToString();
    }

    private void UpdateSpeedMultiplier()
    {
        currentSpeedMultiplier = (gems.Count == 0) ? 1 : gemSpeedCurve.Evaluate(gems.Count);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            GainGem(collision.gameObject.GetComponent<Gem>());
        }
    }

    public void Respawn()
    {
        var spawnpoint = GameObject.FindGameObjectWithTag("Spawnpoint");
        if (spawnpoint == null) Debug.LogError("Can't find an GameObject tagged SpawnPoint", this);
        
        transform.position =new Vector3(spawnpoint.transform.position.x, spawnpoint.transform.position.y, spawnpoint.transform.position.z);
    }

}
