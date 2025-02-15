using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class Gender
{
    public int IdGender { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Smeshariki> Smesharikis { get; set; } = new List<Smeshariki>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
