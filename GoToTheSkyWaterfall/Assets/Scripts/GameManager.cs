using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex; 
    public int exIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;
    public string[] ex;


    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public Text UIExplain;
    public GameObject UIRestartBtn;

    void Update()
    {
        UIPoint.text = "SCORE: " + (totalPoint + stagePoint).ToString();
    }

    public void NextStage()
    {
        //Change Stage
        if (stageIndex < Stages.Length - 1)
        {
            if (stageIndex == 3)
            {
                if ((totalPoint + stagePoint) >= 2000)
                {
                    Stages[stageIndex].SetActive(false);
                    stageIndex++;
                    Stages[stageIndex].SetActive(true);
                    PlayerReposition();
                    UIStage.text = "STAGE " + (stageIndex);
                    exIndex = exIndex + 1;
                    UIExplain.text = ex[exIndex];

                }

            }
            else
            {
                Stages[stageIndex].SetActive(false);
                stageIndex++;
                Stages[stageIndex].SetActive(true);
                PlayerReposition();
                UIStage.text = "STAGE " + (stageIndex);
                exIndex = exIndex + 1;
                UIExplain.text = ex[exIndex];

            }
        }
        else { //Game Clear
            //Player Contol Lock
            Time.timeScale = 0;
            //Result UI
            Debug.Log("게임 클리어!");
            //Restart Button UI
            UIStage.text = "FINISH";
            UIExplain.text = "수고하셨습니다!!";
            //UIRestartBtn.SetActive(true);
            //Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            //btnText.text = "Clear!";
            //UIRestartBtn.SetActive(true);
        }

        //Calculate Piooint
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0);
        }
        else
        {
            //All HealthUI off
            UIhealth[0].color = new Color(1, 0, 0, 0);
            //Player Die Effect
            player.OnDie();

            //Result UI
            Debug.Log("죽었습니다!");
            //Retry Button UI
            UIRestartBtn.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Player Reposition
            if (health > 1)
                PlayerReposition();

            //Health Down
            HealthDown();
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
