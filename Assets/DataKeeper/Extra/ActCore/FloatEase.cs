public struct FloatEase
{
    private readonly float _value;

    private FloatEase(float value)
    {
        _value = value;
    }

    public static implicit operator float(FloatEase target)
    {
        return target._value;
    }

    public static implicit operator FloatEase(float target)
    {
        return new FloatEase(target);
    }

    public override string ToString()
    {
        return _value.ToString();
    }
}
