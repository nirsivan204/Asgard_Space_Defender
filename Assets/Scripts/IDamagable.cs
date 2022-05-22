using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void Kill();
    public void Heal(int amount);
    public void Hurt(int amount);
}
