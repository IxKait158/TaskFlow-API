namespace TaskFlow.Application.Common.Exception;

public class ForbiddenException : System.Exception {
    public ForbiddenException(string message) 
        : base(message) { }
}