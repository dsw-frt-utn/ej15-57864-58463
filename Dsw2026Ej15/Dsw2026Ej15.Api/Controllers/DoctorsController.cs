using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Api.DTOs;
using System;
using System.Linq;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence) => _persistence = persistence;

        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ValidationException("Error: el campo Name es requerido");
            }

            if (string.IsNullOrWhiteSpace(dto.LicenseNumber))
            {
                throw new ValidationException("Error: el campo License Number es requerido");
            }

            var speciality = _persistence.GetSpecialityById(dto.SpecialityId);
            if (speciality == null)
            {
                throw new ValidationException("Error: SpecialityId debe existir");
            }

            var doctor = new Doctor
            {
                Name = dto.Name,
                LicenseNumber = dto.LicenseNumber,
                Speciality = speciality,
                IsActive = true
            };

            _persistence.AddDoctor(doctor);
            return StatusCode(201, doctor);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = _persistence.GetActiveDoctors();
            var result = doctors.Select(d => new
            {
                d.Id,
                d.Name,
                d.LicenseNumber,
                SpecialityName = d.Speciality.Name
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
                return NotFound();

            return Ok(new
            {
                doctor.Name,
                doctor.LicenseNumber,
                SpecialityName = doctor.Speciality.Name
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
                return NotFound();

            doctor.IsActive = false;
            return NoContent();
        }
    }
}