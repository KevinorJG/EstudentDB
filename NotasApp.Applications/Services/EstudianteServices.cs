using NotasApp.Applications.Interfaces;
using NotasApp.Domain.Entities;
using NotasApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasApp.Applications.Services
{
    public class EstudianteServices : IEstudianteServices
    {
        private IEstudianteRepository estudianteRepository;

        public EstudianteServices(IEstudianteRepository estudianteRepository)
        {
            this.estudianteRepository = estudianteRepository;
        }

        public void Create(Estudiante t)
        {
            estudianteRepository.Create(t);
        }

        public bool Delete(Estudiante t)
        {
            return estudianteRepository.Delete(t);
        }

        public Estudiante FindByCarnet(string carnet)
        {
            return estudianteRepository.FindByCarnet(carnet);
        }

        public Estudiante FindByEmail(string email)
        {
           return estudianteRepository.FindByEmail(email);
        }

        public Estudiante FindById(int id)
        {
            return estudianteRepository.FindById(id);
        }

        public List<Estudiante> FindByName(string name)
        {
            return estudianteRepository.FindByName(name);
        }

        public List<Estudiante> GetAll()
        {
           return estudianteRepository.GetAll();
        }

        public int Update(Estudiante t)
        {
            return estudianteRepository.Update(t);
        }
    }
}
