using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemigos : MonoBehaviour

{
    public int health = 1;
    public int points = 100;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bird"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            ScoreManager.Instance.AddScore(points);
            GameManager.Instance.EnemyDied();
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameManager.Instance.RegisterEnemy();
    }




}