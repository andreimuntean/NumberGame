using UnityEngine;

public class Score : MonoBehaviour
{
    public int value { get; set; }

    public void Initialize()
    {
        value = 0;
    }

    void Update()
    {
        guiText.text = value.ToString();
    }
}
