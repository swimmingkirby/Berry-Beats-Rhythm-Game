using UnityEngine;

public static class InputManager
{
    public enum BtnPhase : byte { None, Down, Hold, Up }
    private static BtnPhase GetPhase(KeyCode k)
    {
        if (Input.GetKeyDown(k)) return BtnPhase.Down;
        if (Input.GetKeyUp(k)) return BtnPhase.Up;
        if (Input.GetKey(k)) return BtnPhase.Hold;
        return BtnPhase.None;
    }
    public static (BtnPhase l, BtnPhase r, BtnPhase u, BtnPhase d) GetInput()
    {
        return (
            GetPhase(KeyCode.LeftArrow),
            GetPhase(KeyCode.RightArrow),
            GetPhase(KeyCode.UpArrow),
            GetPhase(KeyCode.DownArrow)
        );
    }
}