using UnityEngine;

static class Calculator
{
    static public bool isFlowing = true;

    static public TileType TypeOf(Tile tile)
    {
        if (IsPrime(tile.value)) return TileType.Prime;
        else if (tile.value % 2 == 0) return TileType.Even;
        else return TileType.Odd;
    }

    static public void GenerateValue(Tile tile)
    {
        var primeNumbers = new int[] { 2, 3, 5, 7 };
        var compositeNumbers = new int[] { 1, 4, 6, 8, 9 };

        if (Random.value > 0.8f)
        {
            tile.value = primeNumbers[Random.Range(0, 4)];
        }
        else
        {
            tile.value = compositeNumbers[Random.Range(0, 5)];
        }
    }

    static public void UpgradeValue(Tile remainingTile, Tile deletedTile)
    {
        remainingTile.value += deletedTile.value;
    }

    static public bool CanMove(Platform platform)
    {
        for (var y = 0; y < platform.height; ++y)
        {
            for (var x = 0; x < platform.width; ++x)
            {
                var current = platform.tiles[y, x];

                if (x + 1 < platform.width)
                {
                    var right = platform.tiles[y, x + 1];

                    if (CanMerge(current, right))
                    {
                        return true;
                    }
                }

                if (y + 1 < platform.height)
                {
                    var down = platform.tiles[y + 1, x];

                    if (CanMerge(current, down))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    static public void Compute(Platform platform, Direction direction)
    {
        var height = platform.height;
        var width = platform.width;
        var tiles = platform.tiles;

        switch (direction)
        {
            case Direction.Left:
                for (var y = 0; y < height; ++y)
                {
                    for (var x = 1; x < width; ++x)
                    {
                        if (CanMerge(tiles[y, x], tiles[y, x - 1]))
                        {
                            for (var k = (int)width - 1; k >= x; --k)
                            {
                                tiles[y, k].targetPosition = tiles[y, k - 1].targetPosition;
                            }

                            if (isFlowing) ++x;
                            else break;
                        }
                    }
                }
                break;

            case Direction.Up:
                for (var x = 0; x < width; ++x)
                {
                    for (var y = 1; y < height; ++y)
                    {
                        if (CanMerge(tiles[y, x], tiles[y - 1, x]))
                        {
                            for (var k = (int)height - 1; k >= y; --k)
                            {
                                tiles[k, x].targetPosition = tiles[k - 1, x].targetPosition;
                            }

                            if (isFlowing) ++y;
                            else break;
                        }
                    }
                }
                break;

            case Direction.Right:
                for (var y = 0; y < height; ++y)
                {
                    for (var x = (int)width - 2; x >= 0; --x)
                    {
                        if (CanMerge(tiles[y, x], tiles[y, x + 1]))
                        {
                            for (var k = 0; k <= x; ++k)
                            {
                                tiles[y, k].targetPosition = tiles[y, k + 1].targetPosition;
                            }

                            if (isFlowing) --x;
                            else break;
                        }
                    }
                }
                break;

            case Direction.Down:
                for (var x = 0; x < width; ++x)
                {
                    for (var y = (int)height - 2; y >= 0; --y)
                    {
                        if (CanMerge(tiles[y, x], tiles[y + 1, x]))
                        {
                            for (var k = 0; k <= y; ++k)
                            {
                                tiles[k, x].targetPosition = tiles[k + 1, x].targetPosition;
                            }

                            if (isFlowing) --y;
                            else break;
                        }
                    }
                }
                break;
        }
    }

    static bool IsPrime(int x)
    {
        if (x <= 3)
        {
            return x > 1;
        }

        if (x % 2 == 0 || x % 3 == 0)
        {
            return false;
        }

        var sqrt = Mathf.Sqrt(x);

        for (var i = 5; i <= sqrt; i += 6)
        {
            if (x % i == 0 || x % (i + 2) == 0)
            {
                return false;
            }
        }

        return true;
    }

    static bool CanMerge(Tile x, Tile y)
    {
        return x.type == y.type;
    }
}
