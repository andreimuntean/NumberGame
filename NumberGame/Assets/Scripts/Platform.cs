using UnityEngine;
using System.Collections.Generic;

public enum AnimationState
{
    Stopped,
    Moving,
    Upgrading
}

public class Platform : MonoBehaviour
{
    public AnimationState animationState { get; private set; }
    public Vector2[,] slots { get; private set; }

    public Tile[,] tiles;

    public int height = 4;
    public int width = 4;
    public float spacing = 1.36f;
    public float speed = 5;

    GameManager gameManager;

    public void Initialize()
    {
        animationState = AnimationState.Stopped;

        for (var y = 0; y < height; ++y)
        {
            for (var x = 0; x < width; ++x)
            {
                tiles[y, x].position = tiles[y, x].targetPosition = slots[y, x];
                tiles[y, x].value = GenerateTile();
            }
        }
    }

    public void StartAnimating()
    {
        animationState = AnimationState.Moving;
    }
    
    int GenerateTile()
    {
        return Random.Range(1, 9);
    }

    Vector2 GetSlotIndex(Vector2 position)
    {
        for (var y = 0; y < height; ++y)
        {
            for (var x = 0; x < width; ++x)
            {
                if (slots[y, x] == position)
                {
                    return new Vector2(x, y);
                }
            }
        }

        return Vector2.zero;
    }

    void Awake()
    {
        gameManager = GameObject.Find("Tile Panel").GetComponent<GameManager>();
        slots = new Vector2[height, width];
        tiles = new Tile[height, width];

        for (var y = 0; y < height; ++y)
        {
            for (var x = 0; x < width; ++x)
            {
                slots[y, x] = new Vector2(spacing * (x - (width - 1) * 0.5f), spacing * ((height - 1) * 0.5f - y));
                tiles[y, x] = new Tile();
            }
        }
    }

    void Update()
    {
        if (animationState == AnimationState.Moving)
        {
            var inPosition = true;

            foreach (var tile in tiles)
            {
                if (tile.position != tile.targetPosition)
                {
                    var modifier = spacing * Time.deltaTime * speed;

                    inPosition = false;

                    switch (gameManager.direction)
                    {
                        case Direction.Left:
                            if (tile.position.x - modifier > tile.targetPosition.x)
                            {
                                tile.position -= new Vector2(modifier, 0);
                            }
                            else tile.position = tile.targetPosition;

                            break;

                        case Direction.Up:
                            if (tile.position.y + modifier < tile.targetPosition.y)
                            {
                                tile.position += new Vector2(0, modifier);
                            }
                            else tile.position = tile.targetPosition;

                            break;

                        case Direction.Right:
                            if (tile.position.x + modifier < tile.targetPosition.x)
                            {
                                tile.position += new Vector2(modifier, 0);
                            }
                            else tile.position = tile.targetPosition;

                            break;

                        case Direction.Down:
                            if (tile.position.y - modifier > tile.targetPosition.y)
                            {
                                tile.position -= new Vector2(0, modifier);
                            }
                            else tile.position = tile.targetPosition;

                            break;
                    }
                }
            }

            if (inPosition)
            {
                animationState = AnimationState.Upgrading;
            }
        }
        else if (animationState == AnimationState.Upgrading)
        {
            var newTiles = new Tile[height, width];
            var deletedTiles = new List<Tile>();
            var remainingTiles = new List<Tile>();

            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < width; ++x)
                {
                    var delete = false;

                    foreach (var remainingTile in remainingTiles)
                    {
                        if (tiles[y, x].position == remainingTile.position)
                        {
                            remainingTile.value += tiles[y, x].value;
                            delete = true;
                            break;
                        }
                    }

                    if (delete)
                    {
                        deletedTiles.Add(tiles[y, x]);
                    }
                    else
                    {
                        remainingTiles.Add(tiles[y, x]);
                    }
                }
            }

            foreach (var tile in remainingTiles)
            {
                var slot = GetSlotIndex(tile.position);

                newTiles[(int)slot.y, (int)slot.x] = tile;
            }

            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < width; ++x)
                {
                    if (newTiles[y, x] == null)
                    {
                        newTiles[y, x] = deletedTiles[0];
                        newTiles[y, x].position = newTiles[y, x].targetPosition = slots[y, x];
                        newTiles[y, x].value = GenerateTile();
                        deletedTiles.RemoveAt(0);
                    }
                }
            }

            tiles = newTiles;
            gameManager.UpdateScore();
            animationState = AnimationState.Stopped;
        }
    }
}
