using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum BuyStatusEnum
{
    [Display(Name = "Compra criada")]
    CompraCriada = 1,

    [Display(Name = "Compra alterada")]
    CompraAlterada = 2,

    [Display(Name = "Compra cancelada")]
    CompraCancelada = 3
}

public enum BuyItemStatusEnum
{
    [Display(Name = "Item criado")]
    ItemCriado = 1,

    [Display(Name = "Item Cancelado")]
    ItemCancelado = 2
}
