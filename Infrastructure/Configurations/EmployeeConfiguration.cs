using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{

    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(x => x.SignalRId).IsRequired(false);
        builder.Property(x => x.UserName).IsRequired(false);

    }
}
