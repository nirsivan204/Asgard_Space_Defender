using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMGR : MonoBehaviour
{
    public ParticleMGR particleMGR;
    public MusicMGR musicMGR;

    [SerializeField] PlayerShip player;
    [SerializeField] PlayerController playerController;
    [SerializeField] CameraMGR cameraMGR;
    [SerializeField] UIMGR uiMGR;
    int numOfKills;
    [SerializeField] int killsToWin;
    [SerializeField] int playerStartingHealth;
    bool isGameFinished = false;

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
        StartCoroutine(LevelSuquence());
    }

    private void GameInit()
    {
        this.player.init(playerStartingHealth, playerController);
        player.OnLivesChangedEvent.AddListener(OnPlayerLivesChanged);
        uiMGR.SetLivesText(playerStartingHealth);
        playerController.changePOVEvent.AddListener(ChangePointOfView);
        enemiesSpawner.init(this.player,this,timeBetweenSpawns,MaxNumOfEnemies,enemyHealth,enemiesPerSpawn);
        astroidMGR.init(minimumVelocity,maximumVelocity,minSize,maxSize);
        enemiesSpawner.OnEnemyDestroyed.AddListener(OnEnemyShipDestroyed);
    }

    private IEnumerator LevelSuquence()
    {
        cameraMGR.ToggleOverviewCamera();
        uiMGR.ShowControls(true);
        StartCoroutine(uiMGR.StartTimer(3,"GO!"));
        musicMGR.Play_Sound(MusicMGR.SoundTypes.Launch);
        yield return new WaitForSeconds(3);
        musicMGR.Play_Sound(MusicMGR.SoundTypes.BG_Music);
        cameraMGR.ToggleOverviewCamera();
        uiMGR.ShowControls(false);
        player.StartMoving();
        enemiesSpawner.StartSpawning();
        astroidMGR.StartSpawning();
    }

    private void ChangePointOfView()
    {
        cameraMGR.ToggglePOV();
    }

    private void ChangeToOverviewCamera()
    {
        cameraMGR.ToggleOverviewCamera();
    }

    private void OnEnemyShipDestroyed()
    {
        if (!isGameFinished)
        {
            numOfKills++;
            uiMGR.setKillText(numOfKills);
            if (numOfKills >= killsToWin)
            {
                StartCoroutine(EndGame());
                musicMGR.Play_Sound(MusicMGR.SoundTypes.Win);
                uiMGR.OnLevelFinish(true);
            }
        }
    }

    private void OnPlayerShipDestroyed()
    {
        ChangeToOverviewCamera();
        musicMGR.Play_Sound(MusicMGR.SoundTypes.Lose);
        StartCoroutine(EndGame());
        uiMGR.OnLevelFinish(false);
    }

    private IEnumerator EndGame()
    {
        isGameFinished = true;
        ChangeToOverviewCamera();
        player.OnLivesChangedEvent.RemoveAllListeners();
        StartCoroutine(uiMGR.StartTimer(3, "Restarting"));
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }

    public void OnPlayerLivesChanged(int lives)
    {
        if (!isGameFinished)
        {
            if (lives < 0)
            {
                lives = 0;
            }
            uiMGR.SetLivesText(lives);
            if (lives <= 0)
            {
                OnPlayerShipDestroyed();
            }
        }
    }

}
