using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Data.Dtos;


namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> doctors = new();
        private readonly List<Speciality> specialities = new();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        private void LoadSpecialities()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");

            if (File.Exists(path))
            {
                var json = File.ReadAllText(path); // [cite: 112]
                var dtos = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }); // [cite: 113]

                if (dtos != null)
                {
                    foreach (var dto in dtos)
                    {
                        specialities.Add(new Speciality
                        {
                            Id = dto.Id,
                            Name = dto.Name,
                            Description = dto.Description
                        });
                    }
                }
            }
        }

        public IEnumerable<Doctor> GetActiveDoctors() => doctors.Where(d => d.IsActive); 

        public Doctor? GetDoctorById(Guid id) =>  doctors.FirstOrDefault(d => d.Id == id);

        public void AddDoctor(Doctor doctor) => doctors.Add(doctor);

        public Speciality? GetSpecialityById(Guid id) => specialities.FirstOrDefault(s => s.Id == id);
    }

}

