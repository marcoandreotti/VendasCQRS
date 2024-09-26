using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum SaleStatusEnum
{
    [Display(Name = "Compra criada")]
    CompraCriada = 1,

    [Display(Name = "Compra alterada")]
    CompraAlterada = 2,

    [Display(Name = "Compra cancelada")]
    CompraCancelada = 3
}

public enum SaleItemStatusEnum
{
    [Display(Name = "Item criado")]
    ItemCriado = 1,

    [Display(Name = "Item Cancelado")] 
    ItemCancelado = 2
}
