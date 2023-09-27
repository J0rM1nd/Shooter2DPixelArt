using UnityEngine;

public static class Number
{
    private static Sprite _n0;
    private static Sprite _n1;
    private static Sprite _n2;
    private static Sprite _n3;
    private static Sprite _n4;
    private static Sprite _n5;
    private static Sprite _n6;
    private static Sprite _n7;
    private static Sprite _n8;
    private static Sprite _n9;

    public static Sprite Get(int n)
    {
        return n switch
        {
            0 => _n0,
            1 => _n1,
            2 => _n2,
            3 => _n3,
            4 => _n4,
            5 => _n5,
            6 => _n6,
            7 => _n7,
            8 => _n8,
            9 => _n9,
            _ => throw new System.Exception("Unknown nubmer" + n)
        };
    }
    static Number()
    {
        _n0 = Resources.Load<Sprite>("Numbers/Number0");
        _n1 = Resources.Load<Sprite>("Numbers/Number1");
        _n2 = Resources.Load<Sprite>("Numbers/Number2");
        _n3 = Resources.Load<Sprite>("Numbers/Number3");
        _n4 = Resources.Load<Sprite>("Numbers/Number4");
        _n5 = Resources.Load<Sprite>("Numbers/Number5");
        _n6 = Resources.Load<Sprite>("Numbers/Number6");
        _n7 = Resources.Load<Sprite>("Numbers/Number7");
        _n8 = Resources.Load<Sprite>("Numbers/Number8");
        _n9 = Resources.Load<Sprite>("Numbers/Number9");

    }
}
