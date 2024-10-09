
namespace Shared.ErrorModels
{
    public class ValidationErrorResponse
    {
        public int statusCode { get; set; }
        public string errorMessage { get; set; }
        public IEnumerable<ValidationError> Errors { get; set; }


    }
    public class ValidationError
    {
        public string field { get; set; }
        public IEnumerable<string> Errors { get; set; }

    }
}
