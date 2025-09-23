using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Timer + Score
    private float elapsedTime = 0f;
    private int score = 0;

    public float scoreMultiplier = 10f;
    public float thrustForce = 1f;

    private Rigidbody2D rb;

    // UI References
    public UIDocument uiDocument;
    private Label scoreText;
    private Label highScoreText;
    private Button restartButton;

    // Explosion prefab
    public GameObject explosionEffect;

    // Borders parent reference
    public GameObject borderParent;

    // Booster flame
    public GameObject boosterFlame;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get UI elements
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        highScoreText = uiDocument.rootVisualElement.Q<Label>("HighScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");

        if (scoreText == null) Debug.LogError("? ScoreLabel not found in UI Document!");
        if (highScoreText == null) Debug.LogError("? HighScoreLabel not found in UI Document!");
        if (restartButton == null) Debug.LogError("? RestartButton not found in UI Document!");

        // Hide restart button at start
        if (restartButton != null)
        {
            restartButton.style.display = DisplayStyle.None;
            restartButton.clicked += ReloadScene;
        }

        // Show saved high score if available
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + savedHighScore;
        }

        // Ensure booster flame starts disabled
        if (boosterFlame != null)
        {
            boosterFlame.SetActive(false);
        }
    }

    void Update()
    {
        // Update score over time
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        // Handle thrust + booster
        if (Mouse.current.leftButton.isPressed)
        {
            // Convert mouse position to world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mousePos - transform.position).normalized;

            // Rotate ship to face the mouse
            transform.up = direction;

            // Apply thrust
            rb.AddForce(direction * thrustForce);

            // Enable booster flame
            if (boosterFlame != null) boosterFlame.SetActive(true);
        }
        else
        {
            // Disable booster flame when not thrusting
            if (boosterFlame != null) boosterFlame.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Play explosion
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // Save new high score if beaten
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }

        // Disable borders so obstacles fly out
        if (borderParent != null)
        {
            Debug.Log("Disabling borders on game over...");
            borderParent.SetActive(false);
        }
        else
        {
            Debug.LogWarning("borderParent not assigned in Inspector!");
        }

        // Show restart button
        if (restartButton != null)
        {
            restartButton.style.display = DisplayStyle.Flex;
        }

        // Destroy player
        Destroy(gameObject);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
