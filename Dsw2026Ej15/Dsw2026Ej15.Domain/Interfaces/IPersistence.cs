using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        IEnumerable<Doctor> GetActiveDoctors();
        Doctor? GetDoctorById(Guid id);
        void AddDoctor(Doctor doctor);
        Speciality? GetSpecialityById(Guid id);
    }
}
