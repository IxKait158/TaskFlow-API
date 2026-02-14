namespace TaskFlow.Application.Common.Exception;

public class ConflictException : System.Exception {
    public ConflictException(string message)
        : base(message) { }
}