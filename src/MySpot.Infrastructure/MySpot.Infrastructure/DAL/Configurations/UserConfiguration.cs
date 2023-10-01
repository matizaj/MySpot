﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
	{
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new UserId(x));
            builder.Property(x => x.Email).HasConversion(x => x.Value, x => new Email(x));
            builder.Property(x => x.FullName).HasConversion(x => x.Value, x => new FullName(x));
            builder.Property(x => x.UserName).HasConversion(x => x.Value, x => new UserName(x));
            builder.Property(x => x.Password).HasConversion(x => x.Value, x => new Password(x));
            builder.Property(x => x.Role).HasConversion(x => x.Value, x => new Role(x));
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.UserName).IsUnique();
        }
    }
}

