using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Canvas gameCanvas;
    public Transform respawnPoint;
    // The default head count, set by player or system default from last Scene.
    public int headCount;

    public float player1_knockValue = 0.0f;
    public float player2_knockValue = 0.0f;

    //GameObject player1_character;
    //GameObject player2_character;

    int player1_headCount;
    int player2_headCount;

    private void Start()
    {
        //player1_character = player1.transform.GetChild(0).gameObject;
        //player2_character = player2.transform.GetChild(0).gameObject;

        player1_headCount = headCount;
        player2_headCount = headCount;

        updateCanvas();
    }

    public void player1Die()
    {
        if(player1_headCount == 0)
        {
            Destroy(player1);
            Debug.Log("Player2 wins!");
            return;
        }
        // if player died, these things will happen:
        // 1. knock_val reset;
        // 2. headCound --;
        // 3. uiCanvas update according to 1 and 2;
        player1_knockValue = 0f;
        player1_headCount--;
        updateCanvas();
        player1.SetActive(false);
        player1.transform.position = respawnPoint.position;
        player1.SetActive(true);
    }
    public void player2Die()
    {
        if(player2_headCount == 0)
        {
            Destroy(player2);
            Debug.Log("Player1 wins!");
            return;
        }
        player2_knockValue = 0f;
        player2_headCount--;
        player2.SetActive(false);
        updateCanvas();
        player2.transform.position = respawnPoint.position;
        player2.SetActive(true);
        
    }

    private void updateCanvas()
    {
        gameCanvas.GetComponent<gameUISystem>().updateKnockValue(player1_knockValue, player2_knockValue);
        gameCanvas.GetComponent<gameUISystem>().updateHeadCount(player1_headCount, player2_headCount);
    }
}
