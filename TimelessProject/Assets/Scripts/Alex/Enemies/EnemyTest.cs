using UnityEngine;

public class EnemyTest : MonoBehaviour, IDamagable
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        CheckDeath();
        Debug.Log(currentHealth);
    }

    public void CheckDeath()
    {
        if(currentHealth <= 0) {
            // Play animation

            Destroy(gameObject);
        }
    }
}
