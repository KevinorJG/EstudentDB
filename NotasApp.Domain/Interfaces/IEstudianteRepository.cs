using NotasApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasApp.Domain.Interfaces
{
    public interface IEstudianteRepository : IRepository<Estudiante>
    {
        Estudiante FindById(int id);
        Estudiante FindByEmail(string email);
        List<Estudiante> FindByName(string name);
        Estudiante FindByCarnet(string carnet);
    }
}
