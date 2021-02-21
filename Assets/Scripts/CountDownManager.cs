using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownManager : MonoBehaviour
{

   Text TimerNum;

    public static CountDownManager Instance;

    //public delegate void TriggerGameOver();
    //public TriggerGameOver GameOverTrigger;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        TimerNum = GetComponent<Text>();
        CountDownTimer.GetInstance().CountDown("main_timer", 60f, (t) =>
        {
            TimerNum.text = string.Format("{0:0}", t);
        }, () =>
        {
            Debug.Log("time is up");

            if (ScoreManager.Instance.GetScore() > 70)
            {
                Game.GetInstance().ToHighScore();
                //SceneManager.LoadScene("HighScore");
            }

            else if (ScoreManager.Instance.GetScore() < 30)
            {
                Game.GetInstance().ToLowScore();
                //SceneManager.LoadScene("LowScore");
            }

            else 
            {
                Game.GetInstance().ToMidScore();
                //SceneManager.LoadScene("MidScore");
            }

            CountDownTimer.GetInstance().ClearAllTimers();
        });
    }

    public static CountDownManager GetInstance()
    {
        return Instance;
    }
}
