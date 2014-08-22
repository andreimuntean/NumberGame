using UnityEngine;

static class Calculator
{
    static public bool CanMove(Platform platform)
    {
        for (var y = 0; y < platform.height - 1; ++y)
        {
            for (var x = 0; x < platform.width - 1; ++x)
            {
                var current = platform.tiles[y, x].value;
                var right = platform.tiles[y, x + 1].value;
                var down = platform.tiles[y + 1, x].value;
                
                if (CanMerge(current, down) || CanMerge(current, right))
                {
                    return true;
                }
            }
        }

        return false;
    }

    static public bool IsPrime(int x)
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

    static public bool IsEven(int x)
    {
        return x % 2 == 0;
    }

    static public bool IsOdd(int x)
    {
        return x % 2 == 1;
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
                    var start = 0;

                    for (var x = 1; x < width; ++x)
                    {
                        if (CanMerge(tiles[y, x].value, tiles[y, start].value))
                        {
                            for (var k = (int)width - 1; k > start; --k)
                            {
                                tiles[y, k].targetPosition = tiles[y, k - 1].targetPosition;
                            }

                            ++x;
                        }

                        start = x;
                    }
                }
                break;

            case Direction.Up:
                for (var x = 0; x < width; ++x)
                {
                    var start = 0;

                    for (var y = 1; y < height; ++y)
                    {
                        if (CanMerge(tiles[y, x].value, tiles[start, x].value))
                        {
                            for (var k = (int)height - 1; k > start; --k)
                            {
                                tiles[k, x].targetPosition = tiles[k - 1, x].targetPosition;
                            }

                            ++y;
                        }

                        start = y;
                    }
                }
                break;

            case Direction.Right:
                for (var y = 0; y < height; ++y)
                {
                    var start = (int)width - 1;

                    for (var x = (int)width - 2; x >= 0; --x)
                    {
                        if (CanMerge(tiles[y, x].value, tiles[y, start].value))
                        {
                            for (var k = 0; k < start; ++k)
                            {
                                tiles[y, k].targetPosition = tiles[y, k + 1].targetPosition;
                            }

                            --x;
                        }

                        start = x;
                    }
                }
                break;

            case Direction.Down:
                for (var x = 0; x < width; ++x)
                {
                    var start = (int)height - 1;

                    for (var y = (int)height - 2; y >= 0; --y)
                    {
                        if (CanMerge(tiles[y, x].value, tiles[start, x].value))
                        {
                            for (var k = 0; k < start; ++k)
                            {
                                tiles[k, x].targetPosition = tiles[k + 1, x].targetPosition;
                            }

                            --y;
                        }

                        start = y;
                    }
                }
                break;
        }
    }

    static bool EqualParity(int x, int y)
    {
        return x % 2 == y % 2;
    }

    static bool CanMerge(int x, int y)
    {
        if (IsPrime(x))
        {
            if (IsPrime(y)) return true;
            return false;
        }
        else if (IsPrime(y))
        {
            if (IsPrime(x)) return true;
            return false;
        }

        return EqualParity(x, y);
    }
}
