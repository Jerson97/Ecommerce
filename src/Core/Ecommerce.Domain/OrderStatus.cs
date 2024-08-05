using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Ecommerce.Domain.Common;

public enum OrderStatus {
    [EnumMember(Value = "Pendiente")]
    Pending,
    [EnumMember(Value = "El pago fue recibido")]
    Completed,
    [EnumMember(Value = "El producto fue enviado")]
    Sent,
    [EnumMember(Value = "El pago tuvo errores")]
    Error
}