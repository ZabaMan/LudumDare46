using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    TheGame theGame;
    bool alive = true;
    int playerID;
    public SpriteRenderer playerImage;
    public Sprite playerDead;
    public AudioSource playerDeathSound;

    public void SetupPlayer(TheGame theGame, int ID, Color playerColor)
    {
        this.theGame = theGame;
        playerID = ID;
        playerImage.color = playerColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Death" && alive)
        {
            alive = false;
            playerImage.sprite = playerDead;
            playerDeathSound.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<PlayerMove>().enabled = false;
            theGame.ReportDead(playerID);
        }
    }

    public void FreezePlayer()
    {
        if(GetComponent<BoxCollider2D>())
        GetComponent<BoxCollider2D>().enabled = false;
        if(GetComponent<PlayerMove>())
        GetComponent<PlayerMove>().enabled = false;
    }

}
