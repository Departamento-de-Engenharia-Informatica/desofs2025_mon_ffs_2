using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.DTOs.DeliveryDate;

public class CreateDeliveryDto
{
    [Required]
    public DateTime Date { get; set; }
    public ResourceStatus ResourceStatus { get; set; } = ResourceStatus.Ativo; //default value
}