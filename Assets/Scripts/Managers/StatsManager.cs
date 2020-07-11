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

    private void DisplayRunStats() {
        FindStatObjectByTag("ScoreDisplau").text = "SCORE: " + score.ToString();
        FindStatObjectByTag("TimeDisplay").text  = "TIME SURVIVED: " + TimeToString(survivalTime);
        FindStatObjectByTag("KillDisplay").text  = "ENEMIES KILLED: " + enemiesKilled.ToString();
    }

    #region Helper Method For DisplayStats
    private TextMeshProUGUI FindStatObjectByTag(string tag) {
        return GameObject.FindGameObjectWithTag(tag).GetComponent<TextMeshProUGUI>();
    }

    private string TimeToString(float seconds) {
        if (seconds < 60) {
            return seconds.ToString("F1") + "s";
        }
        int minutes = (int)(seconds / 60);
        seconds = seconds - (minutes * 60);
        if (minutes < 60) {
            return minutes.ToString() + "m " + seconds.ToString("F0") + "s";
        }
        int hours = minutes / 60;
        minutes = minutes - (hours * 60);
        if (hours < 24) {
            return hours.ToString() + "h " + minutes.ToString() + "m";
        }
        int days = hours / 24;
        hours = hours - (days * 24);
        return days.ToString() + "d " + hours.ToString() + "h ";
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
    }

    public void AddEnemyKill() {
        enemiesKilled++;
    }
}
