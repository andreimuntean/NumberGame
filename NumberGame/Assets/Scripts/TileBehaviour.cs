using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public TextMesh textMesh;

    public int value
    {
        get { return int.Parse(textMesh.text); }
        set
        {
            textMesh.text = value.ToString();

            if (Calculator.IsPrime(value))
            {
                this.textMesh.color = new Color(0, 0, 0);
            }
            else if (Calculator.IsEven(value))
            {
                this.textMesh.color = new Color(0.75f, 0.75f, 0);
            }
            else if (Calculator.IsOdd(value))
            {
                this.textMesh.color = new Color(0, 0.75f, 0.75f);
            }
        }
    }
}
