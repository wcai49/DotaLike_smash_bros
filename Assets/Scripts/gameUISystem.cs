using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameUISystem : MonoBehaviour
{
    public Text p1_knock;
    public Text p2_knock;
    public int p1_headcount = 0;
    public int p2_headcount = 0;

    public GameObject p1_img;
    public GameObject p2_img;
    
    public void updateKnockValue(float p1_k, float p2_k)
    {
        p1_knock.text = p1_k.ToString() + "%";
        p2_knock.text = p2_k.ToString() + "%";
    }
    public void updateHeadCount(int p1_hc, int p2_hc)
    {
        // player1 just got more head count than current
        if (p1_hc > p1_headcount)
        {
            Vector3 newPos = p1_knock.transform.position;
            // half width of p1_knock panel
            newPos.x -= 130.5f;
            newPos.y -= 120f;
            for(int i = p1_headcount; i < p1_hc; i++)
            {
                Instantiate(p1_img, newPos, Quaternion.identity, p1_knock.transform);
                newPos.x += 70f;
            }
            p1_headcount = p1_hc;
        }
        else if(p1_hc < p1_headcount)
        {
            Destroy(p1_knock.transform.GetChild(p1_headcount - 1).gameObject);
        }

        // so does player2
        if (p2_hc > p2_headcount)
        {
            Vector3 newPos = p2_knock.transform.position;
            // half width of p1_knock panel
            newPos.x -= 130.5f;
            newPos.y -= 120f;
            for (int i = p2_headcount; i < p2_hc; i++)
            {
                Instantiate(p2_img, newPos, Quaternion.identity, p2_knock.transform);
                newPos.x += 70f;
            }
            p2_headcount = p2_hc;
        }
        else if (p2_hc < p2_headcount)
        {
            Destroy(p2_knock.transform.GetChild(p2_headcount - 1).gameObject);
        }
    }

}
