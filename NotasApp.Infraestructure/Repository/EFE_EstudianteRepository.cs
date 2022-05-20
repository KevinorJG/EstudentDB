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
            try
            {
                if (t == null)
                {
                    throw new Exception("Este objeto no puede ser nulo");
                }
                schoolContext.Estudent.Add(t);
                schoolContext.SaveChanges();
            }
            catch
            {
                throw;
            }
            
        }

        public bool Delete(Estudiante t)
        {
            try
            {
                if (t == null)
                {
                    throw new Exception("Este objeto no puede ser nulo");
                }
                Estudiante estudiante = FindById(t.Id);
                if (estudiante == null)
                {
                    throw new Exception("este objeto no existe");
                }
                schoolContext.Estudent.Remove(t);
                int result = schoolContext.SaveChanges();
                return result > 0;
            }
            catch
            {
                throw;
            }
            
        }

        public Estudiante FindByCarnet(string carnet)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(carnet))
                {
                    throw new ArgumentNullException("dato inválido");
                }
                return schoolContext.Estudent.FirstOrDefault(x => x.Carnet == carnet);
            }
            catch
            {
                throw;
            }
            
        }

        public Estudiante FindByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentNullException("email inválido");
                }
                return schoolContext.Estudent.FirstOrDefault(x => x.Correo == email);
            }
            catch
            {
                throw;
            }
            
        }

        public Estudiante FindById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentNullException("este dato no es válido");
                }
                return schoolContext.Estudent.FirstOrDefault(x => x.Id == id);
            }
            catch
            {
                throw;
            }
           
        }

        public List<Estudiante> FindByName(string name)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentNullException("este dato no es válido");
                }
                return schoolContext.Estudent.Where(e => e.Nombres.Equals(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            catch
            {
                throw;
            }
           
        }

        public List<Estudiante> GetAll()
        {
            try
            {
                return schoolContext.Estudent.ToList();
            }
            catch
            {
                throw;
            }
          
        }

        public int Update(Estudiante t)
        {
            try
            {
                if(t == null)
                {
                    throw new Exception("Este objeto no puede ser nulo");
                }
                schoolContext.Estudent.Update(t);
                return schoolContext.SaveChanges();
            }
            catch
            {
                throw;
            }
            
        }
    }
}
