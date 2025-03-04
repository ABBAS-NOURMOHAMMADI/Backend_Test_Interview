﻿namespace Domain.Entities;

public class TaskAssignment : BaseEntity<int>
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public int UserId { get; set; }
    public Task Task { get; set; }
    public User User { get; set; }
}
