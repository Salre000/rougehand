using UnityEngine;
public static class test : JokerBase
{
    public override int Trun() { return 16; }
    public override void RoundStart()
    {
        if ((Random.Range(0, 10000) % 6) < 1)
        {
            JokerUtility.Remove(this);
        }
    }

}