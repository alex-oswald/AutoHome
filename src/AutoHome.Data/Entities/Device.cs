﻿namespace AutoHome.Data.Entities;

public class Device : BaseEntity
{
    public Guid DeviceId { get; set; }
    public string Type { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Uri { get; set; } = null!;
}
