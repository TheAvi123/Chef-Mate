using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    //Reference Variables
    public static StatsManager sharedInstance;

    //Current Run Statistics
    private int score;
    private float survivalTime;
    private int enemiesKilled;

    //State Variables
    private bool trackRunTime = false;

    //Internal Methods
    private void Awake() {
        SetSharedInstance();
    }

    private void SetSharedInstance() {
        sharedInstance = this;
    }

    private void OnSceneChange() {
        if (SceneManager.GetActiveScene().name == "PlayScene") {
            ToggleRunTimeTracking(true);
            ResetStats();
            DisplayScoreStat();
        }
        if (SceneManager.GetActiveScene().name == "GameOver") {
            ToggleRunTimeTracking(false);
            DisplayRunStats();
        }
    }   //Called Through Singleton

    private void ResetStats() {
        score = 0;
        survivalTime = 0;
        enemiesKilled = 0;
    }

    private void DisplayScoreStat() {
        FindStatObjectByTag("ScoreDisplay").text = "SCORE: " + score.ToString();
    }

    private void DisplayRunStats() {
        FindStatObjectByTag("ScoreDisplay").text = "SCORE: " + score.ToString();
        FindStatObjectByTag("TimeDisplay").text  = "TIME SURVIVED: " + TimeToString(survivalTime);
        FindStatObjectByTag("KillDisplay").text  = "ENEMIES KILLED: " + enemiesKilled.ToString();
    }

    #region Helper Method For DisplayStats
    private TextMeshProUGUI FindStatObjectByTag(string tag) {
        return GameObject.FindGameObjectWithTag(tag).GetComponent<TextMeshProUGUI>();
    }

    private string TimeToString(float seconds) {
        if (seconds < 60) {
            return seconds.ToString("F1") + " SEC";
        }
        int minutes = (int)(seconds / 60);
        seconds = seconds - (minutes * 60);
        if (minutes < 60) {
            return minutes.ToString() + " M " + seconds.ToString("F0") + " S";
        }
        int hours = minutes / 60;
        minutes = minutes - (hours * 60);
        if (hours < 24) {
            return hours.ToString() + " H " + minutes.ToString() + " M";
        }
        int days = hours / 24;
        hours = hours - (days * 24);
        return days.ToString() + " D " + hours.ToString() + " H";
    }
    #endregion

    private void Update() {
        TickRunTime();
    }

    private void TickRunTime() {
        if (trackRunTime) {
            survivalTime += Time.deltaTime;
        }
    }

    private void ToggleRunTimeTracking(bool track) {
        trackRunTime = track;
    }

    //Public Methods
    public void AddScore(int amount) {
        score += amount;
        DisplayScoreStat();
    }

    public void AddEnemyKill() {
        enemiesKilled++;
    }
}
