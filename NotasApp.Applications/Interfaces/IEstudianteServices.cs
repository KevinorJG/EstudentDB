using NotasApp.Domain.Entities;
using NotasApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasApp.Applications.Interfaces
{
    public interface IEstudianteServices : IServices<Estudiante>
    {
        Estudiante FindById(int id);
        Estudiante FindByEmail(string email);
        List<Estudiante> FindByName(string name);
        Estudiante FindByCarnet(string carnet);
    }
}
