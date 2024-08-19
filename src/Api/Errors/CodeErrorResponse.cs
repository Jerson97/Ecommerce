using Newtonsoft.Json;

namespace Ecommerce.Api.Errors
{
    public class CodeErrorResponse
    {
        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string[]? Message { get; set; }

        public CodeErrorResponse(int statusCode, string[]? message = null)
        {
            StatusCode = statusCode;

            if (message != null)
            {

                Message = new string[0];
                var text = getDefaultMessageStatusCode(statusCode);
                Message[0] = text;
            }
            else {

                Message = message;
            }

        }

        private string getDefaultMessageStatusCode(int statusCode) {

            return statusCode switch
            {
                400 => "El Request enviado tiene errores",
                401 => "No tienes authorization para este recurso",
                404 => "No se encontro el recurso solicitado",
                500 => "Se produjieron errores en el servidor",
                _ => string.Empty,
            };
        }
    }

    
}
