using System.Text.Json;

namespace NoNameLib.Domain.Extensions;

public static class ObjectExtensions
{
    public static string ToJson(this object obj, JsonSerializerOptions options = null)
        => JsonSerializer.Serialize(obj, obj.GetType(), options);
}
