using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private BowlingLane bowlingLane;

    private int currentRoll = 1;
    private int fallenInFirstRoll = 0;
    private float ballSpawnTime;
    private const float MIN_BALL_TRAVEL_TIME = 1.0f;

    private BallController subscribedBallController;

    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        currentRoll = 1;
        fallenInFirstRoll = 0;

        levelManager.LoadLevel();
        bowlingLane.SpawnBall();

        SubscribeToCurrentBall();

        uiManager.UpdateLevel(levelManager.CurrentLevelIndex);
        uiManager.ShowMessage("Roll 1");
    }

    private void SubscribeToCurrentBall()
    {
        if (subscribedBallController != null)
            subscribedBallController.OnBallStopped -= OnBallStopped;

        var ballGO = bowlingLane.CurrentBall;
        if (ballGO == null)
        {
            Debug.LogWarning("[GameManager.SubscribeToCurrentBall] CurrentBall is null");
            subscribedBallController = null;
            return;
        }

        var bc = ballGO.GetComponent<BallController>();
        if (bc == null)
        {
            Debug.LogWarning("[GameManager.SubscribeToCurrentBall] BallController missing on CurrentBall");
            subscribedBallController = null;
            return;
        }

        subscribedBallController = bc;
        subscribedBallController.OnBallStopped += OnBallStopped;
        ballSpawnTime = Time.time;
    }

    public void OnBallStopped()
    {
        if (Time.time - ballSpawnTime < MIN_BALL_TRAVEL_TIME)
            return;

        Debug.Log("Ball has stopped.");
        int fallen = levelManager.BottleManager.CountFallen();

        levelManager.BottleManager.RemoveFallen();
        uiManager.UpdateScore(fallen);

        if (currentRoll == 1)
        {
            fallenInFirstRoll = fallen;

            if (levelManager.BottleManager.AllCleared())
            {
                LevelCompleted();
                return;
            }

            currentRoll = 2;
            uiManager.ShowMessage("Roll 2");

            bowlingLane.ResetLane();
            SubscribeToCurrentBall();
        }
        else
        {
            if (levelManager.BottleManager.AllCleared())
            {
                LevelCompleted();
            }
            else
            {
                LevelFailed();
            }
        }
    }

    private void LevelCompleted()
    {
        uiManager.ShowMessage("Level Complete!");
        levelManager.NextLevel();
        StartLevel();
    }

    private void LevelFailed()
    {
        uiManager.ShowMessage("Try Again!");
        StartLevel();
    }
}
