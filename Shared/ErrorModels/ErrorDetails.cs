
using System.Text.Json;

namespace Shared.ErrorModels
{
    public class ErrorDetails
    {
        public int statusCode { get; set; }
        public string errorMessage { get; set; }
        public IEnumerable<string> errors { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);   //convert from c# into json
    }
}
