using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class Property
{
    public int IdProperty { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Smeshariki> Idsmeshes { get; set; } = new List<Smeshariki>();
}
