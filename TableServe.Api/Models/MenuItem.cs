using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableServe.Api.Models;

public class MenuItem {

    public int Id { get; set; } = 0;
    [StringLength(30)]
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "decimal(11,2)")]
    public decimal Price { get; set; } = decimal.Zero;

    public int CategoryId { get; set; } = 0;
    public Category? Category { get; set; } = null;

}
