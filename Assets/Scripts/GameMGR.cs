using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMGR : MonoBehaviour
{
    public ParticleMGR particleMGR;
    //[SerializeField] float arenaRadius;
    [SerializeField] PlayerShip player;
    [SerializeField] PlayerController playerController;
    [SerializeField] CameraMGR cameraMGR;
    [SerializeField] UIMGR uiMGR;
    int numOfKills;
    [SerializeField] int killsToWin;
    [SerializeField] AstroidMGR astroidMGR;
    [SerializeField] EnemiesSpawner enemiesSpawner;

    //public float ArenaRadius { get => arenaRadius; set => arenaRadius = value; }

    private void Start()
    {
        playerController.changePOVEvent.AddListener(ChangePointOfView);
        player.init(10000, playerController);
    }

    private void ChangePointOfView()
    {
        cameraMGR.ToggglePOV();
    }

    private void onEnemyShipDestroyed()
    {
        numOfKills++;
        uiMGR.setKillText(numOfKills);
        if(numOfKills == killsToWin)
        {
            EndGame();
            Win();
        }
    }

    private void OnPlayerShipDestroyed()
    {
        EndGame();
        Lose();
    }

    private void EndGame()
    {

    }

    public void Lose()
    {

    }

    public void Win()
    {

    }
}
