using System.Text.Json.Serialization;

namespace TaskFlow.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ETaskStatus {
    ToDo = 1,
    InProgress = 2,
    Done = 3,
    Canceled = 99
}