using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Contadores")]
    public int enemiesRemaining = 0;
    public int birdsRemaining = 3;

    private bool levelEnded = false;

    void Awake()
    {
        Instance = this;
    }

    // 🐷 Se llama cuando un enemigo aparece
    public void RegisterEnemy()
    {
        enemiesRemaining++;
    }

    // 💥 Se llama cuando un enemigo muere
    public void EnemyDied()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            WinLevel();
        }
    }

    // 🐦 Se llama cuando disparas un pájaro
    public void UseBird()
    {
        birdsRemaining--;

        if (birdsRemaining <= 0)
        {
            // esperar un poco antes de perder
            Invoke(nameof(CheckLoseCondition), 2f);
        }
    }

    void CheckLoseCondition()
    {
        // si ya no hay pájaros y aún hay enemigos → perder
        if (enemiesRemaining > 0)
        {
            LoseLevel();
        }
    }

    void WinLevel()
    {
        if (levelEnded) return;

        levelEnded = true;
        Debug.Log("GANASTE 🎉");

        Invoke(nameof(RestartLevel), 2f);
    }

    void LoseLevel()
    {
        if (levelEnded) return;

        levelEnded = true;
        Debug.Log("PERDISTE 💀");

        Invoke(nameof(RestartLevel), 2f);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
