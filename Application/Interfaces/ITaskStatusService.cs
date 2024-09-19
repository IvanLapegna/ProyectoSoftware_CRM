using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITaskStatusService
    {

        Task<ICollection<GenericResponse>> GetAll();
        Task<bool> existe(int id);

    }
}
