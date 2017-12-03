using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    public int playerID;
    public SpriteRenderer tintableSprite;
    public Transform feet;
    public Transform shootPoint;
    public Transform gemPoint;
    public Transform bagPoint;
    public float gemBagScale = 0.7f;
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
    public float invulnerableDuration = 2;

    public string horizontalName { get { return "Player" + playerID + "Horizontal"; } }
    public string verticalName { get { return "Player" + playerID + "Vertical"; } }
    public string jumpButtonName { get { return "Player" + playerID + "Jump"; } }
    public string fireButtonName { get { return "Player" + playerID + "Fire1"; } }

    private const float GroundCheckRadius = 0.2f;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator animator;
    private bool jump = false;
    private int jumpsLeft = 2;
    private bool fire = false;
    private float nextFireTime;
    private float pauseTimeLeft = 0;
    private bool hasSetPlayerNumber = false;
    private ParticleSystem fireParticleSystem;
    private LevelManager levelManager;
    private float invulnerableTimeLeft = 0;
    private bool isInvulnerable { get { return invulnerableTimeLeft > 0; } }
    private AudioSource runAudioSource;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        runAudioSource = GetComponent<AudioSource>();
        fireParticleSystem = GetComponentInChildren<ParticleSystem>();
        if (rb == null) Debug.Log(name + " missing Rigidbody2D", this);
        if (animator == null) Debug.Log(name + " missing Animator", this);
        if (feet == null) Debug.Log(name + " assign Feet", this);
        if (shootPoint == null) Debug.Log(name + " assign Feet", this);
        if (projectilePrefab == null) Debug.Log(name + " assign Projectile Prefab", this);
        
    }

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
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
        if (invulnerableTimeLeft > 0) invulnerableTimeLeft -= Time.deltaTime;
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
        if (isGrounded && !wasGrounded)
        {
            jumpsLeft = 2;
            GameSoundManager.instance.PlayJumpLand();
        }
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
        runAudioSource.enabled = isGrounded && Mathf.Abs(moveSpeed) > 0.1f;
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
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce * currentSpeedMultiplier));
            GameSoundManager.instance.PlayJump();
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
        if (Time.time >= nextFireTime) // && gems.Count > 0) // Can still fire without gems.
        {
            fireParticleSystem.gameObject.transform.position = shootPoint.transform.position;
            fireParticleSystem.Play();

            GameSoundManager.instance.PlayFire();
            if (animator != null) animator.SetTrigger("Fire");
            var projectile = Instantiate<Projectile>(projectilePrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            projectile.player = this;
            var force = projectileForce * new Vector3(facingRight ? 1 : -1, 0, 0);
            projectile.GetComponent<Rigidbody2D>().AddForce(force);
            nextFireTime = Time.time + fireRate * (gems.Count + 1);
        }
    }

    public void GetHit()
    {
        if (isInvulnerable) return;
        if (gems.Count == 0) GameSoundManager.instance.PlayHit();
        if (animator != null) animator.SetTrigger("Hit");
        LoseGem();
        pauseTimeLeft = hitPauseDuration;
        invulnerableTimeLeft = invulnerableDuration;
    }

    public void GainGem(Gem gem)
    {
        if (gem == null || !gem.isAvailable) return;
        GameSoundManager.instance.PlayGetGem();
        gems.Add(gem);
        gem.HoldBy(gemPoint);
        gem.gameObject.SetActive(false);

        if (gem.gemSpawn != null && gem.gemSpawn.occupied == true)
        {
            gem.gemSpawn.occupied = false;
            gem.gemSpawn = null;
        }
        
        UpdateSpeedMultiplier();
        UpdateBagSize();
        levelManager.GemCounts[playerID - 1].text = gems.Count.ToString();
        if (gems.Count == 10)
        {
            levelManager.gemWinConReached = true;
        }
    }

    public void LoseGem()
    {
        if (gems.Count == 0) return;
        GameSoundManager.instance.PlayLoseGem();
        var gem = gems[0];
        gems.RemoveAt(0);
        gem.gameObject.SetActive(true);
        gem.Release();
        UpdateSpeedMultiplier();
        UpdateBagSize();
        levelManager.GemCounts[playerID - 1].text = gems.Count.ToString();
    }

    private void UpdateSpeedMultiplier()
    {
        currentSpeedMultiplier = (gems.Count == 0) ? 1 : gemSpeedCurve.Evaluate(gems.Count);
    }

    private void UpdateBagSize()
    {
        bagPoint.localScale = new Vector3(1 + (gemBagScale * gems.Count), 1 + (gemBagScale * gems.Count), 1);
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
        foreach (var gem in gems)
        {
            
            gem.transform.SetParent(null);
            Destroy(gem.gameObject);
        }
        gems.Clear();
        GameSoundManager.instance.PlayRespawn();
        var spawnpoint = GameObject.FindGameObjectWithTag("Spawnpoint");
        if (spawnpoint == null) Debug.LogError("Can't find an GameObject tagged SpawnPoint", this);
      
        transform.position = new Vector3(spawnpoint.transform.position.x, spawnpoint.transform.position.y, spawnpoint.transform.position.z);
        invulnerableTimeLeft = invulnerableDuration;
        UpdateSpeedMultiplier();
        UpdateBagSize();
    }

}
