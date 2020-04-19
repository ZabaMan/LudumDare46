using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayer : MonoBehaviour
{
    public TheGame theGame;
    public AudioSource playerJoinSound;

    public List<PlayerClass> players = new List<PlayerClass>();
    public List<string> chosenControls = new List<string>();

    public GameObject playerSetup;
    public GameObject canvas;
    public GameObject titleScreen;

    private bool enteringInput = false;

    public Color[] playerColors;
    private List<Color> colorsLeft;

    private int rowCount = 0, colCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        colorsLeft = new List<Color>(playerColors);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return") && !enteringInput)
        {
            playerJoinSound.Play();
            players.Add(new PlayerClass());
            var newPlayerSetup = Instantiate(playerSetup, canvas.transform);
            players[players.Count - 1].SetPlayerSetup(newPlayerSetup);
            newPlayerSetup.GetComponent<PlayerSetup>().SetAddPlayer(this);
            rowCount++;
            var posX = newPlayerSetup.transform.position.x + (300 * colCount);
            var posY = newPlayerSetup.transform.position.y - (120 * rowCount);
            if (rowCount == 5)
            {
                rowCount = 0;
                colCount++;
            }
            newPlayerSetup.transform.position = new Vector2(posX, posY);

            var playerColor = GetPlayerRandomColor();
            var honorific = GetPlayerRandomHonorific();
            players[players.Count - 1].AddColorAndHonorific(playerColor, honorific);
            newPlayerSetup.GetComponent<PlayerSetup>().SetPlayerColorAndHonorific(playerColor, honorific);

            enteringInput = true;
        }
        else if (Input.GetKeyDown("backspace"))
        {
            // TODO setup backspace
        }
        else if ((Input.GetKey("space") && (Input.GetKey("left shift") || Input.GetKey("right shift"))) && players.Count > 0)
        {
            InitiateGame();
        }
    }

    public void SetCurrentPlayersControls(string[] controls)
    {
        players[players.Count - 1].AddInput(controls);
        enteringInput = false;
    }

    public void AddChosenControl(string control)
    {
        chosenControls.Add(control);
    }

    public bool CheckChosenControls(string control)
    {
        foreach(var chosenControl in chosenControls)
        {
            if (chosenControl == control)
                return false;
        }

        return true;
    }

    private void InitiateGame()
    {
        PlayerClass playerToRemove = null;
        foreach(var player in players)
        {
            if(player.setupComplete != true)
            {
                playerToRemove = player;
            }
        }
        if(playerToRemove != null)
            players.Remove(playerToRemove);

        theGame.SetupGame(players);

        titleScreen.SetActive(false);
        canvas.SetActive(false);
        this.enabled = false;

    }

    private Color GetPlayerRandomColor()
    {
        var colorIndex = Random.Range(0, colorsLeft.Count);
        var color = colorsLeft[colorIndex];
        colorsLeft.Remove(colorsLeft[colorIndex]);
        return color;
    }

    private string GetPlayerRandomHonorific()
    {
        var mrOrMrs = Random.Range(0, 2);
        print(mrOrMrs);
        if(mrOrMrs== 0)
        {
            return "MRS";
        }
        return "MR";
    }
}
