using System.Text.Json;
using System.Text.Json.Serialization;

namespace LMSBackend.Application.Common.JsonConverters
{
    public class DateTimeWithTimeConverter : JsonConverter<DateTime?>
    {
        private const string Format = "MM/dd/yyyy hh:mm:ss tt";

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            return string.IsNullOrEmpty(s) ? null : DateTime.ParseExact(s, Format, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString(Format));
        }
    }
}