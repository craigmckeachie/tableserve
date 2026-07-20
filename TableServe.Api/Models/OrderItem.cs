using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TableServe.Api.Models;

public class OrderItem {

    public int Id { get; set; } = 0;
    public int Quantity { get; set; } = 1;
    [StringLength(200)]
    public string? Notes { get; set; } = null;

    public int OrderId { get; set; } = 0;
    [JsonIgnore]
    public Order? Order { get; set; } = null;

    public int MenuItemId { get; set; } = 0;
    public MenuItem? MenuItem { get; set; } = null;

}
