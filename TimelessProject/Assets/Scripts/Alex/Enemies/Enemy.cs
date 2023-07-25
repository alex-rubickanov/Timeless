using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth;
    private int _health;

    private void Start()
    {
        _health = Mathf.RoundToInt(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    public void CheckDeath()
    {
        if (_health <= 0.0f)
        {
            Debug.LogError("Death is not implemented!!!");
        }
    }
}
