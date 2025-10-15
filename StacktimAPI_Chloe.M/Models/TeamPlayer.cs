using System;
using System.Collections.Generic;

namespace StacktimAPI_Chloe.M.Models;

public partial class TeamPlayer
{
    public int TeamId { get; set; }

    public int PlayerId { get; set; }

    public int Role { get; set; }

    public DateTime JoinDate { get; set; }

    public virtual Player Player { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
