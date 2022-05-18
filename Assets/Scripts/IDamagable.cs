using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void kill();
    public void heal(int amount);
    public void hurt(int amount);
}
