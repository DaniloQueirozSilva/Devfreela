using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{

    public class ProjectRepository : IProjectRepository
    {
        public readonly DevFreelaDbContext _dbContext;
       
        public ProjectRepository(DevFreelaDbContext dbContext )
        {
            _dbContext = dbContext;
            
        }


        public async Task<List<Project>> GetAll()
        {
            return await _dbContext.Projects.ToListAsync();
        }

            }
}
