using System;

namespace Kollectionized.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
}