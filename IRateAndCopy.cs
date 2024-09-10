namespace Lab05_CSharp
{
    internal interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
}
