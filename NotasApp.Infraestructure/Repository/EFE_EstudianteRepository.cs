using NotasApp.Domain.Entities;
using NotasApp.Domain.EstudianteDB;
using NotasApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasApp.Infraestructure.Repository
{
    public class EFE_EstudianteRepository : IEstudianteRepository
    {
        private IPepitoSchoolContext schoolContext;

        public EFE_EstudianteRepository(IPepitoSchoolContext schoolContext)
        {
            this.schoolContext = schoolContext;
        }

        public void Create(Estudiante t)
        {
            schoolContext.Estudent.Add(t);
            schoolContext.SaveChanges();
        }

        public bool Delete(Estudiante t)
        {
            schoolContext.Estudent.Remove(t);
            int result = schoolContext.SaveChanges();
            return result > 0;
        }

        public Estudiante FindByCarnet(string carnet)
        {
            return schoolContext.Estudent.FirstOrDefault(x => x.Carnet == carnet);
        }

        public Estudiante FindByEmail(string email)
        {
            return schoolContext.Estudent.FirstOrDefault(x => x.Correo == email);
        }

        public Estudiante FindById(int id)
        {
            return schoolContext.Estudent.FirstOrDefault(x => x.Id == id);
        }

        public List<Estudiante> FindByName(string name)
        {
            return schoolContext.Estudent.Where(e => e.Nombres.Equals(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public List<Estudiante> GetAll()
        {
           return schoolContext.Estudent.ToList();
        }

        public int Update(Estudiante t)
        {
            schoolContext.Estudent.Update(t);
            return schoolContext.SaveChanges();
        }
    }
}
