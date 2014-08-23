using UnityEngine;

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

    Platform platform;
    Score score;

    public void Initialize()
    {
        platform.Initialize();
        score.Initialize();
        UpdateScore();
        isRunning = true;
    }

    public void UpdateScore()
    {
        var result = 0;

        foreach (var tile in platform.tiles)
        {
            result += tile.value;
        }

        score.value = result;
    }

    void HandleInput()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                isMoving = true;
                direction = Direction.Left;
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                isMoving = true;
                direction = Direction.Up;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                isMoving = true;
                direction = Direction.Right;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
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
    }

    void Start()
    {
        Initialize();
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
