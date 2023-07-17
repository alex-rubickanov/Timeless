using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(int damage);
    public void CheckDeath();
}
