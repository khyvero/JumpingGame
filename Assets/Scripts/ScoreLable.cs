using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLable : MonoBehaviour
{

    Text scoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        scoreLabel = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLabel();
    }

    void UpdateLabel()
    {
        scoreLabel.text = ScoreManager.Instance.GetScore().ToString();
        //scoreLabel.text = "Score:" + ScoreManager.Instance.GetScore().ToString();
    }
}
