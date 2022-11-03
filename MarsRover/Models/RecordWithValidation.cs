namespace MarsRover.Models;

public abstract record RecordWithValidation
{
    protected RecordWithValidation() => Validate();
    protected virtual void Validate() { }
}