using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectCommentConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder
            .HasKey(P => P.Id);

            builder
                .HasOne(P => P.Project)
                .WithMany(f => f.Comments)
                .HasForeignKey(p => p.IdProject);
            builder
               .HasOne(P => P.User)
               .WithMany(f => f.Comments)
               .HasForeignKey(p => p.IdUser);
        }
    }
}
