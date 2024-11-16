using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomContinuousMoveProvider : ActionBasedContinuousMoveProvider
{
    public Vector2 input;
    // Start is called before the first frame update
    void Start()
    {
        input = Vector2.zero;
    }


    protected override Vector2 ReadInput()
    {
        return input;
    }
}
