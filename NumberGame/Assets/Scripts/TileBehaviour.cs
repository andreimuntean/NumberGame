using UnityEngine;

public enum TileType
{
    Even = 0,
    Odd = 1,
    Prime = 2
}

public class TileBehaviour : MonoBehaviour
{
    public TextMesh textMesh;
    public int value;

    void Update()
    {
        textMesh.text = value.ToString();
    }
}

public class Tile
{
    private int tileValue;
    public int value
    {
        get { return tileValue; }
        set
        {
            tileValue = value;
            Update();
        }
    }

    public Vector2 position
    {
        get { return view.transform.position; }
        set
        {
            view.transform.position = new Vector3(value.x, value.y, -5);
        }
    }

    public Vector2 targetPosition { get; set; }
    public GameObject view { get; set; }
    public TileType type { get; set; }

    public Tile()
    {
        view = (GameObject)Object.Instantiate(Resources.Load<GameObject>("Prefabs/Tile"));
    }

    void Update()
    {
        var tileBehaviour = view.GetComponent<TileBehaviour>();

        tileBehaviour.value = value;
        type = Calculator.TypeOf(this);

        switch (type)
        {
            case TileType.Even:
                tileBehaviour.textMesh.color = new Color(0, 0, 1);
                break;

            case TileType.Odd:
                tileBehaviour.textMesh.color = new Color(0, 0, 0);
                break;

            case TileType.Prime:
                tileBehaviour.textMesh.color = new Color(1, 0, 0);
                break;
        }
    }
}
