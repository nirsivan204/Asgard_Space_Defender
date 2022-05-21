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
    [SerializeField] private int MaxNumOfEnemies;
    [SerializeField] private int enemyHealth;
    [SerializeField] private int enemiesPerSpawn;
    [SerializeField] private int timeBetweenSpawns;

    [SerializeField] float minimumVelocity;
    [SerializeField] float maximumVelocity;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;

    //public float ArenaRadius { get => arenaRadius; set => arenaRadius = value; }

    private void Start()
    {
        GameInit();
    }

    private void GameInit()
    {
        this.player.init(10000, playerController);
        playerController.changePOVEvent.AddListener(ChangePointOfView);
        enemiesSpawner.init(this.player,this,timeBetweenSpawns,MaxNumOfEnemies,enemyHealth,enemiesPerSpawn);
        //astroidMGR.init(minimumVelocity,maximumVelocity,minSize,maxSize);
        enemiesSpawner.OnEnemyDestroyed.AddListener(onEnemyShipDestroyed);
        enemiesSpawner.StartSpawning();
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
        uiMGR.OnLevelFinish(false);
    }

    public void Win()
    {
        uiMGR.OnLevelFinish(true);
    }


}
