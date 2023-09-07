using System;

[Serializable]
public class IntReference
{
    public bool UseConstant = true;
    public float ConstantValue;
    public IntVariable Variable;

    public IntReference()
    { }

    public IntReference(float value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public float Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator float(IntReference reference)
    {
        return reference.Value;
    }
}
