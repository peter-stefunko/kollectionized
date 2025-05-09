using System;
using System.Collections.Generic;

namespace Kollectionized.Models;

public class CardInstanceDetails
{
    public Guid Id { get; set; }
    public string OwnerUsername { get; set; } = string.Empty;
    public decimal? Grade { get; set; }
    public string GradingCompany { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public List<string> TradeHistory { get; set; } = [];
}