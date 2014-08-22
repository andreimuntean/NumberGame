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
        GUI.skin.button.normal.background = buttonTexture;
        GUI.skin.button.hover.background = buttonTexture;
        GUI.skin.button.active.background = buttonTexture;
        GUI.skin.button.alignment = TextAnchor.MiddleCenter;
        GUI.skin.button.fontSize = 30;

        /*if (GUI.Button(new Rect(406, 537, 192, 75), "Undo"))
        {

        }
        else if (GUI.Button(new Rect(604, 537, 192, 75), "Restart"))
        {

        }*/

        if (GUI.Button(new Rect(505, 537, 192, 75), "Restart"))
        {
            gameManager.Initialize();
        }
    }
}
