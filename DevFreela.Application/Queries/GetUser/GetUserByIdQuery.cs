using DevFreela.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetUserByIdQuery : IRequest<UserViewModel>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            this.Id = id;
        }

        
    }
}
