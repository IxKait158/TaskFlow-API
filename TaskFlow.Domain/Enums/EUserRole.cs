using System.Text.Json.Serialization;

namespace TaskFlow.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EUserRole {
    Admin = 1,
    User = 2
}