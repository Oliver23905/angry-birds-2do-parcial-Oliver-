using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Resistencia")]
    public float health = 50f;

    [Header("Configuración de daño")]
    public float minImpactForce = 5f;
    public float damageMultiplier = 5f;

    [Header("Puntos")]
    public int points = 50;

    [Header("Efecto visual (opcional)")]
    public GameObject destroyEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Solo reacciona si el que golpea es el pájaro
        if (collision.gameObject.CompareTag("Bird"))
        {
            float impactForce = collision.relativeVelocity.magnitude;

            if (impactForce > minImpactForce)
            {
                float damage = impactForce * damageMultiplier;
                TakeDamage(damage);
            }
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        // Efecto visual (si tienes uno)
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }

        // Sumar puntos
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(points);
        }

        // Destruir objeto
        Destroy(gameObject);
    }
}