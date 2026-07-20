using System.ComponentModel.DataAnnotations;

namespace TableServe.Api.Models;

public class Category {

    public int Id { get; set; } = 0;
    [StringLength(30)]
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; } = 0;

}
