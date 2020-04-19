using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class TheGame : MonoBehaviour
{
    class CurrentPlayers
    {
        public PlayerManager playerManager;
        public bool dead { get; set; }

        public CurrentPlayers(PlayerManager playerManager)
        {
            this.playerManager = playerManager;
            dead = false;
        }

        public void Dead()
        {
            dead = true;
        }
    }

    private List<CurrentPlayers> currentPlayers = new List<CurrentPlayers>();

    public AudioSource startGameSound;
    public List<PlayerClass> players = new List<PlayerClass>();
    private int currentLevel = 0;
    public GameObject[] levels;
    public GameObject playersParent;
    public GameObject playersScoresParent;
    public GameObject playerPrefab;
    public GameObject playerScorePrefab;
    public GameObject scoreboard;
    public Text scoreboardTimer;
    private float scoreboardTimeCurrent;
    public Transform bulletParent;

    private int amountDead = 0;

    private bool gameActive = false;
    private bool scoreboardActive = false;
    private float levelTime = 0.0f;

    public float maxLevelTime;
    public Text levelTimer;

    public GameObject gameFinished;
    private bool canRestart = false;

    public void SetupGame(List<PlayerClass> newPlayers)
    {
        players = newPlayers;
        StartLevel();
    }

    private void StartLevel()
    {
        startGameSound.Play();
        for(int i = 0; i < players.Count; i++)
        {
            var newPlayer = Instantiate(playerPrefab, playersParent.transform);
            newPlayer.GetComponent<PlayerManager>().SetupPlayer(this, i, players[i].color);
            newPlayer.GetComponent<PlayerMove>().SetupControls(players[i].input);
            newPlayer.transform.position = new Vector2(Random.Range(-0.7f, 0.7f), Random.Range(-0.7f, 0.7f));

            currentPlayers.Add(new CurrentPlayers(newPlayer.GetComponent<PlayerManager>()));
        }
        levels[currentLevel].SetActive(true);
        levelTime = 0;
        gameActive = true;

    }

    private void Update()
    {
        if (gameActive)
        {
            levelTime += Time.deltaTime;

            levelTimer.text = Mathf.Round(maxLevelTime - levelTime).ToString();
            if(levelTime >= maxLevelTime)
            {
                for(int i = 0; i < currentPlayers.Count; i++)
                {
                    if (!currentPlayers[i].dead)
                    {
                        players[i].AddLevelTime(maxLevelTime);
                    }
                    currentPlayers[i].playerManager.FreezePlayer();
                }
                currentPlayers.Clear();
                EndLevel();
            }
        }
        else if (scoreboardActive)
        {
            scoreboardTimeCurrent -= Time.deltaTime;
            scoreboardTimer.text = ((int)scoreboardTimeCurrent).ToString();
            if(scoreboardTimeCurrent <= 0)
            {
                NextLevel();
            }
        }
        else if (canRestart)
        {
            if (Input.GetKey("space") && (Input.GetKey("left shift") || Input.GetKey("right shift")))
            {
                Scene scene = SceneManager.GetActiveScene(); 
                SceneManager.LoadScene(scene.name);
            }
        }
    }

    private void NextLevel()
    {
        scoreboard.SetActive(false);
        scoreboardActive = false;
        levels[currentLevel].SetActive(false);
        for (var i = 0; i < players.Count; i++)
        {
            Destroy(playersScoresParent.transform.GetChild(i).gameObject);
            Destroy(playersParent.transform.GetChild(i).gameObject);
        }
        foreach(Transform child in bulletParent)
        {
            Destroy(child.gameObject);
        }
        currentLevel++;
        amountDead = 0;
        StartLevel();
    }

    private void EndLevel()
    {
        gameActive = false;
        scoreboard.SetActive(true);
        List<PlayerClass> playerScores = new List<PlayerClass>(players);
        playerScores = playerScores.OrderByDescending(x => x.score).ToList();
        for(var i = 0; i < playerScores.Count; i++)
        {
            var newScore = Instantiate(playerScorePrefab, playersScoresParent.transform);

            newScore.transform.position = new Vector3(newScore.transform.position.x,
                newScore.transform.position.y - (75 * i),
                newScore.transform.position.z);

            var name = playerScores[i].input[0] + playerScores[i].input[1] + playerScores[i].input[2] + playerScores[i].input[3];
            newScore.GetComponent<SetScore>().Set(playerScores[i].color, playerScores[i].honorific, name.ToUpper(), 
                ((int)playerScores[i].currentLevelTime).ToString(), ((int)playerScores[i].score).ToString());
            print(playerScores[i].currentLevelTime + "| " + playerScores[i].score);
        }

        if (currentLevel != levels.Length - 1)
        {
            scoreboardTimeCurrent = 10;
            scoreboardActive = true;
        }
        else
        {
            Invoke("GameFinished", 5f);
        }
    }

    private void GameFinished()
    {
        levels[currentLevel].SetActive(false);
        gameFinished.SetActive(true);
        canRestart = true;
    }

    public void ReportDead(int ID)
    {
        players[ID].AddLevelTime(levelTime);
        currentPlayers[ID].Dead();
        amountDead++;
        if (amountDead == players.Count)
        {
            currentPlayers.Clear();
            EndLevel();
        }
    }
}
