using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class Smeshariki
{
    public int IdSmesharik { get; set; }

    public string Name { get; set; } = null!;

    public int Gender { get; set; }

    public int Age { get; set; }

    public string Animal { get; set; } = null!;

    public byte[]? Image { get; set; }

    public virtual Gender GenderNavigation { get; set; } = null!;

    public virtual ICollection<Property> Idproperties { get; set; } = new List<Property>();
}
