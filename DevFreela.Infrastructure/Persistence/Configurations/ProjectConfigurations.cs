﻿using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
           
            builder
            .HasKey(p => p.IdClient);

            builder
                 .HasOne(p => p.Freelancer)
                 .WithMany(f => f.FreelanceProjects)
                 .HasForeignKey(p => p.IdFreelancer)
                 .OnDelete(DeleteBehavior.Restrict);

            builder
              .HasOne(p => p.Client)
              .WithMany(f => f.OwnedProjects)
              .HasForeignKey(p => p.IdClient)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
