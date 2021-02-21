using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance;
	private static int score;

	void Start()
	{
		DontDestroyOnLoad(this);

		Instance = this;

		ResetScore();

	}

	public void ResetScore()
	{
		score = 0;
	}

	public void AddToScore(int toAdd)
	{
		score += toAdd;
	}

	public int GetScore()
	{
		return score;
	}

	
}
