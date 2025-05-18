using System.ComponentModel.DataAnnotations;

namespace AMAPP.API.DTOs.DeliveryDate;

public class CreateDeliveryDateDto
{
    [Required]
    public DateTime Date { get; set; }
    public ResourceStatus ResourceStatus { get; set; } = ResourceStatus.Ativo; //default value
}