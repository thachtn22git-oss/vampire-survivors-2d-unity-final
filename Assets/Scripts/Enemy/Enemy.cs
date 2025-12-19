using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float health = 30f;
    [SerializeField] private int experienceToGive = 1;

    [Header("Push Back")]
    [SerializeField] private float pushTime = 0.2f;
    private float pushCounter;
    private float baseMoveSpeed;

    [Header("Player Damage")]
    [SerializeField] private float damageCooldown = 0.5f;
    private float damageTimer;

    [Header("Drop & Death")]
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private float pointDropChance = 0.5f;
    [SerializeField] private GameObject destroyEffect;

    private bool isDead = false;

    void Start()
    {
        baseMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (PlayerController.Instance != null &&
            PlayerController.Instance.gameObject.activeSelf)
        {
            // Flip sprite theo hướng player
            spriteRenderer.flipX =
                PlayerController.Instance.transform.position.x < transform.position.x;

            Vector2 direction =
                (PlayerController.Instance.transform.position - transform.position).normalized;

            float currentSpeed = baseMoveSpeed;

            // Push back khi bị đánh
            if (pushCounter > 0)
            {
                pushCounter -= Time.fixedDeltaTime;
                currentSpeed = -baseMoveSpeed;
            }

            rb.linearVelocity = direction * currentSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Player") && damageTimer <= 0)
        {
            PlayerController.Instance.TakeDamage(damage);
            damageTimer = damageCooldown;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        health -= damageAmount;

        DamageNumberController.Instance.CreateNumber(
            damageAmount,
            transform.position
        );

        pushCounter = pushTime;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // 50% rớt POINT
        if (pointPrefab != null && Random.value <= pointDropChance)
        {
            Instantiate(pointPrefab, transform.position, Quaternion.identity);
        }

        // Cho EXP
        PlayerController.Instance.GetExperience(experienceToGive);

        // Hiệu ứng chết
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
        }

        AudioController.Instance.PlayModifiedSound(
            AudioController.Instance.enemyDie
        );

        Destroy(gameObject);
    }
}
