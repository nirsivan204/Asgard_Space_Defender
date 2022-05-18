using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInitiable<T>
{
    public void init(T parameter);
}
