﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemindersDomain.models;

namespace RemindersDomain.infrastructure
{
    class ReminderEntityTypeConfiguration : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.ToTable("Reminder", null);
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Text);
            builder.Property(r => r.DateTime);
            builder.Property(r => r.DateTimeString);
        }
    }
}
