using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesSpawner : AbstractSpawner
{
    private int timeBetweenSpawns;
    private int totalAliveEnemies;
    private int MaxNumOfEnemies;
    private int enemyHealth;
    private IEnumerator spawningCoroutine;
    private int enemiesPerSpawn;
    private PlayerShip playerShip;
    private GameMGR gameMGR;

    public UnityEvent OnEnemyDestroyed { get; } = new UnityEvent();


    public void init(PlayerShip player, GameMGR gameMGR, int timeBetweenSpawns, int MaxNumOfEnemies,int enemyHealth, int enemiesPerSpawn)
    {
        this.timeBetweenSpawns = timeBetweenSpawns;
        this.MaxNumOfEnemies = MaxNumOfEnemies;
        this.enemyHealth = enemyHealth;
        this.enemiesPerSpawn = enemiesPerSpawn;
        this.playerShip = player;
        this.gameMGR = gameMGR;
    }
    private void EnemyDestroyed()
    {
        OnEnemyDestroyed.Invoke();
        totalAliveEnemies--;
    }

    public override void StartSpawning()
    {
        base.StartSpawning();
        spawningCoroutine = makeEnemies();
        StartCoroutine(spawningCoroutine);

    }

    public override void StopSpawning()
    {
        base.StopSpawning();
        StopCoroutine(spawningCoroutine);
    }


    IEnumerator makeEnemies()
    {
        while (isSpawning)
        {

            if (totalAliveEnemies < MaxNumOfEnemies)
            {
                List<GameObject> enemiesCreated;
                SpawnAroundPlayer(Mathf.Min(enemiesPerSpawn, MaxNumOfEnemies - totalAliveEnemies), out enemiesCreated);
                foreach (GameObject enemy in enemiesCreated)
                {
                    SimpleEnemyShip enemyClone = enemy.GetComponent<SimpleEnemyShip>();
                    enemyClone.init(playerShip,this.gameMGR, enemyHealth);
                    enemyClone.OnKillEvent.AddListener(EnemyDestroyed);
                    enemyClone.StartMoving();
                    totalAliveEnemies++;
                }
            }
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
