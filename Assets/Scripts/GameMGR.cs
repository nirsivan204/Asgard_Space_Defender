using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMGR : MonoBehaviour
{
    public ParticleMGR particleMGR;
    [SerializeField] float arenaRadius;
    [SerializeField] PlayerShip player;

    public float ArenaRadius { get => arenaRadius; set => arenaRadius = value; }

    private void Start()
    {
        player.init(100);
    }
}
