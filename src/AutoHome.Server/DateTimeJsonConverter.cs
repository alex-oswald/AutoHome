using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// This converter lets us serialize datetime the same way as Newtonsoft.Json. This lets the the rest client create DateTimeOffset
/// objects with the correct time zone info.
/// </summary>
public sealed class DateTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dt = reader.GetDateTimeOffset();
        return new DateTime(dt.UtcTicks, DateTimeKind.Utc);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var isoDate = new DateTimeOffset(value).ToString("O");
        writer.WriteStringValue(isoDate);
    }
}