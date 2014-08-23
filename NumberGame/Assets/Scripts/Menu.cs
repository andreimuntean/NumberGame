using UnityEngine;

public class Menu : MonoBehaviour
{
    GameManager gameManager;
    Texture2D buttonTexture;

    void Awake()
    {
        gameManager = GameObject.Find("Tile Panel").GetComponent<GameManager>();
        buttonTexture = Resources.Load<Texture2D>("Sprites/ButtonBackground");
    }

    void OnGUI()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        GUI.skin.button.normal.background = buttonTexture;
        GUI.skin.button.hover.background = buttonTexture;
        GUI.skin.button.active.background = buttonTexture;
        GUI.skin.button.alignment = TextAnchor.MiddleCenter;
        GUI.skin.button.fontSize = 30;

        if (GUI.Button(new Rect((screenWidth - 192) / 2, screenHeight - 100, 192, 75), "Restart"))
        {
            gameManager.Initialize();
        }
    }
}
