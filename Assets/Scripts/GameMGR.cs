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

    [SerializeField] float minimumAstroidVelocity;
    [SerializeField] float maximumAstroidVelocity;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;

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
        player.OnKillEvent.AddListener(OnPlayerShipDestroyed);
        playerController.changePOVEvent.AddListener(ChangePointOfView);
        enemiesSpawner.init(this.player,this,timeBetweenSpawns,MaxNumOfEnemies,enemyHealth,enemiesPerSpawn);
        astroidMGR.init(minimumAstroidVelocity,maximumAstroidVelocity,minSize,maxSize);
        enemiesSpawner.OnEnemyDestroyed.AddListener(OnEnemyShipDestroyed);
    }

    private IEnumerator LevelSuquence()
    {
        cameraMGR.ToggleOverviewCamera();
        uiMGR.ShowControls(true);
        StartCoroutine(uiMGR.StartTimer(3,"GO!"));
        musicMGR.PlaySound(MusicMGR.SoundTypes.Launch);
        yield return new WaitForSeconds(3);
        musicMGR.PlaySound(MusicMGR.SoundTypes.BG_Music);
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
                musicMGR.PlaySound(MusicMGR.SoundTypes.Win);
                uiMGR.OnLevelFinish(true);
            }
        }
    }

    private void OnPlayerShipDestroyed()
    {
        ChangeToOverviewCamera();
        musicMGR.PlaySound(MusicMGR.SoundTypes.Lose);
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
        }
    }

}
