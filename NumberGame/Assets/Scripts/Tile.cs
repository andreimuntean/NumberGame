using UnityEngine;

public class Tile
{
    public Vector2 position
    {
        get
        {
            return view.transform.position;
        }
        set
        {
            view.transform.position = new Vector3(value.x, value.y, -5);
        }
    }

    public Vector2 targetPosition { get; set; }
    public GameObject view { get; set; }

    private int rawValue;
    public int value
    {
        get { return rawValue; }
        set
        {
            rawValue = value;
            view.GetComponent<TileBehaviour>().value = rawValue;
        }
    }

    public Tile()
    {
        view = (GameObject)Object.Instantiate(Resources.Load<GameObject>("Prefabs/Tile"));
    }
}
