namespace Playground;

public class Calculator
{
    public MathResult Add(MathInput input, MathInput input2)
    {
        return new MathResult { Result = input.Input + input2.Input };
    }
}

public class MathInput
{
    public int Input { get; set; }
}

public class MathResult
{
    public int Result { get; set; }
}