using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableServe.Api.Models;

public class Order {

    public int Id { get; set; } = 0;
    public int TableNumber { get; set; } = 0;
    [StringLength(200)]
    public string? Notes { get; set; } = null;
    [StringLength(10)]
    public string Status { get; set; } = OrderStatus.Placed;
    [StringLength(200)]
    public string? CancellationReason { get; set; } = null;
    [Column(TypeName = "decimal(11,2)")]
    public decimal Total { get; set; } = decimal.Zero;
    public DateTime OrderedAt { get; set; } = DateTime.Now;

    public int StaffId { get; set; } = 0;
    public Staff? Staff { get; set; } = null;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}
