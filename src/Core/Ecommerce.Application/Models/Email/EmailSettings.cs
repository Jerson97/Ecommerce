namespace Ecommerce.Application.Models.Email
{
    public class EmailSettings
    {
        public string? Email { get; set; }       // Dirección de correo del remitente
        public string? Key { get; set; }         // Clave API (si usas una API como SendGrid)
        public string? BaseUrlClient { get; set; } // URL base del cliente (para construir enlaces en correos)
        public string? DisplayName { get; set; } // Nombre del remitente
        public string? SmtpHost { get; set; }    // Servidor SMTP
        public int SmtpPort { get; set; }        // Puerto SMTP
        public string? SmtpUser { get; set; }    // Usuario SMTP
        public string? SmtpPass { get; set; }    // Contraseña SMTP
    }

}
