using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Size Settings")]
    public float minSize = 0.5f;
    public float maxSize = 2.0f;

    [Header("Speed Settings")]
    public float minSpeed = 50f;
    public float maxSpeed = 150f;
    public float speedMultiplier = 1.05f;   // How much to boost speed each bounce
    public float maxVelocity = 500f;        // Clamp to avoid infinite speed

    [Header("Effects")]
    public GameObject bounceEffectPrefab;   // Assign in Inspector

    private Rigidbody2D rb;

    void Start()
    {
        // Randomize obstacle size
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        // Randomize direction and speed
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // Apply physics force
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Make sure gravity isn't pulling them down
        rb.AddForce(randomDirection * randomSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb != null)
        {
            // Increase difficulty: boost velocity each bounce
            rb.linearVelocity *= speedMultiplier;

            // Clamp speed to avoid infinite acceleration
            if (rb.linearVelocity.magnitude > maxVelocity)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
            }
        }

        if (bounceEffectPrefab != null)
        {
            // Get the contact point
            Vector2 contactPoint = collision.GetContact(0).point;

            // Spawn the bounce effect
            GameObject bounceEffect = Instantiate(bounceEffectPrefab, contactPoint, Quaternion.identity);

            // Scale bounce effect by impact velocity
            float scaleFactor = Mathf.Clamp(rb.linearVelocity.magnitude / 10f, 0.5f, 2f);
            bounceEffect.transform.localScale *= scaleFactor;

            // Destroy effect after 1 second
            Destroy(bounceEffect, 1f);
        }
    }
}
