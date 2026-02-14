using System.Text.Json.Serialization;

namespace TaskFlow.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ETaskPriority {
    Low = 1,
    Normal = 2,
    High = 3
}