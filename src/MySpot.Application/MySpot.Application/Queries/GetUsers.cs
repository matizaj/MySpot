using MySpot.Application.Abstractions;
using MySpot.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Application.Queries
{
    public class GetUsers:IQuery<IEnumerable<UserDto>>
    {
    }
}
