using UnityEngine;
using System.Collections;

public enum Direction
{
    Left = 1,
    Up = 2,
    Right = 3,
    Down = 4
}

public class GameManager : MonoBehaviour
{
    public bool isMoving { get; private set; }
    public bool isRunning { get; private set; }
    public Direction direction { get; private set; }

    KeyCode currentKey;
    Platform platform;
    Score score;

    public void Initialize()
    {
        platform.Initialize();
        score.Initialize();
        isRunning = true;
    }

    public void UpdateScore(int value)
    {
        score.value += value;
    }

    void HandleInput()
    {
        if (Input.anyKeyDown && !isMoving)
        {
            if (currentKey == KeyCode.A || currentKey == KeyCode.LeftArrow)
            {
                isMoving = true;
                direction = Direction.Left;
            }
            else if (currentKey == KeyCode.W || currentKey == KeyCode.UpArrow)
            {
                isMoving = true;
                direction = Direction.Up;
            }
            else if (currentKey == KeyCode.D || currentKey == KeyCode.RightArrow)
            {
                isMoving = true;
                direction = Direction.Right;
            }
            else if (currentKey == KeyCode.S || currentKey == KeyCode.DownArrow)
            {
                isMoving = true;
                direction = Direction.Down;
            }
        }
    }

    void Awake()
    {
        platform = GameObject.Find("Tile Panel").GetComponent<Platform>();
        score = GameObject.Find("Score").GetComponent<Score>();
        isRunning = true;
    }

    void OnGUI()
    {
        currentKey = Event.current.keyCode;
    }

    void Update()
    {
        if (isRunning)
        {
            if (!isMoving)
            {
                // Await input.
                HandleInput();

                if (isMoving)
                {
                    // Calculate changes.
                    Calculator.Compute(platform, direction);

                    // Start animating.
                    platform.StartAnimating();
                }
            }
            else if (platform.animationState == AnimationState.Stopped)
            {
                isMoving = false;
                isRunning = Calculator.CanMove(platform);
            }
        }
        else
        {
            // Debug.Log("The game has ended.");
        }
    }
}
