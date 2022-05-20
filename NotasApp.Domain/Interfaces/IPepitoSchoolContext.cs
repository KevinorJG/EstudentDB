using Microsoft.EntityFrameworkCore;
using NotasApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasApp.Domain.Interfaces
{
    public interface IPepitoSchoolContext
    {

        public DbSet<Estudiante> Estudent { get; set; }
        public int SaveChanges();
    }
}
