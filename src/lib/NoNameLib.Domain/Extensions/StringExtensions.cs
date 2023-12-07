using System.Text.Json;

namespace NoNameLib.Domain.Extensions;

public static class StringExtensions
{
    public static T FromJson<T>(
        this string json,
        JsonSerializerOptions options = null)
        where T : class
            => json.FromJson(typeof(T), options) as T;

    public static object FromJson(
        this string json,
        Type type,
        JsonSerializerOptions options = null) => JsonSerializer.Deserialize(json, type, options);
}
