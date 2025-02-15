using System;
using System.Collections.Generic;

namespace AvaloniaApplication1.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateTime Dateofbirtdh { get; set; }

    public int Gender { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public virtual Gender GenderNavigation { get; set; } = null!;

    public virtual Role RoleNavigation { get; set; } = null!;
}
