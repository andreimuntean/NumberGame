using UnityEngine;

public class Score : MonoBehaviour
{
    public int value = 0;

    public void Initialize()
    {
        value = 0;
    }

    void Update()
    {
        guiText.text = value.ToString();
    }
}
