using UnityEngine;

public class EnemyTest : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float takenDamage)
    {
        currentHealth -= takenDamage;
        if (IsDead())
        {
            Destroy(gameObject);
        }
        else
        {
            // nothing
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0.0f;
    }
}
