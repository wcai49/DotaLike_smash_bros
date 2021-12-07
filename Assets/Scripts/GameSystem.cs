using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Canvas gameCanvas;
    public Transform respawnPoint;

    public int headCount;

    public float player1_knockValue = 0.0f;
    public float player2_knockValue = 0.0f;

    int player1_headCount;
    int player2_headCount;

    private void Start()
    {
        player1_headCount = headCount;
        player2_headCount = headCount;

        updateCanvas();
    }

    public void player1Die()
    {
        if(player1_headCount == 0)
        {
            Destroy(player1);
            Debug.Log("Player2 win");
        }
        // if player died, these things will happen:
        // 1. knock_val reset;
        // 2. headCound --;
        // 3. uiCanvas update according to 1 and 2;
        player1_knockValue = 0f;
        player1_headCount--;
        updateCanvas();
        player1.transform.position = new Vector3(0,20f,0);        
    }
    public void player2Die()
    {
        player2_knockValue = 0f;
        player2_headCount--;
        updateCanvas();
        Destroy(player2);
    }

    private void updateCanvas()
    {
        gameCanvas.GetComponent<gameUISystem>().updateKnockValue(player1_knockValue, player2_knockValue);
        gameCanvas.GetComponent<gameUISystem>().updateHeadCount(player1_headCount, player2_headCount);
    }
}
