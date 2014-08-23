using UnityEngine;

public class Tile
{
    private int actualValue;
    public int value
    {
        get { return actualValue; }
        set
        {
            actualValue = value;
            view.GetComponent<TileBehaviour>().value = actualValue;
        }
    }

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

    public Tile()
    {
        view = (GameObject)Object.Instantiate(Resources.Load<GameObject>("Prefabs/Tile"));
    }
}
