public struct Ease
{
    private readonly float _value;

    private Ease(float value)
    {
        _value = value;
    }

    public static implicit operator float(Ease target)
    {
        return target._value;
    }

    public static implicit operator Ease(float target)
    {
        return new Ease(target);
    }
}
