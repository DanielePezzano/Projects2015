namespace Models.Base.Interfaces
{
    public interface IBaseLog
    {
        string MethodCaller { get; set; }
        string Description { get; set; }
        string ParametersValue { get; set; } //json string
    }
}
