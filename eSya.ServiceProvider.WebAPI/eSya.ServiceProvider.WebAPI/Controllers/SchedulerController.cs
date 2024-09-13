using eSya.ServiceProvider.DL.Repository;
using eSya.ServiceProvider.DO;
using eSya.ServiceProvider.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ServiceProvider.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private readonly ISchedulerRepository _schedulerRepository;

        public SchedulerController(ISchedulerRepository schedulerRepository)
        {
            _schedulerRepository = schedulerRepository;
        }
        #region Doctor Schedule
        /// <summary>
        ///Get Doctors by Business Key dropdown 
        /// UI Reffered - Doctor Schedule,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorsbyBusinessKey(int Businesskey)
        {
            var ds = await _schedulerRepository.GetDoctorsbyBusinessKey(Businesskey);
            return Ok(ds);
        }
        /// <summary>
        ///Get Specialties Doctor ID dropdown 
        /// UI Reffered - Doctor Schedule,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetSpecialtiesbyDoctorID(int Businesskey, int DoctorID)
        {
            var ds = await _schedulerRepository.GetSpecialtiesbyDoctorID(Businesskey, DoctorID);
            return Ok(ds);
        }
        /// <summary>
        ///Get Clinics by Speciality ID dropdown 
        /// UI Reffered - Doctor Schedule,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetClinicsbySpecialtyID(int Businesskey, int DoctorID, int SpecialtyID)
        {
            var ds = await _schedulerRepository.GetClinicsbySpecialtyID(Businesskey, DoctorID, SpecialtyID);
            return Ok(ds);
        }
        /// <summary>
        ///Get Consultation Type by Clinic Type dropdown 
        /// UI Reffered - Doctor Schedule,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetConsultationsbyClinicID(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID)
        {
            var ds = await _schedulerRepository.GetConsultationsbyClinicID(Businesskey, DoctorID, SpecialtyID, ClinicID);
            return Ok(ds);
        }
        /// <summary>
        ///Get Schedule Grid 
        /// UI Reffered - Doctor Schedule,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorScheduleList(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID, int ConsultationID)
        {
            var ds = await _schedulerRepository.GetDoctorScheduleList(Businesskey, DoctorID, SpecialtyID, ClinicID, ConsultationID);
            return Ok(ds);
        }
        /// <summary>
        ///Insert inot Doctor Schedule
        /// UI Reffered - Doctor Schedule,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoDoctorSchedule(DO_DoctorScheduler obj)
        {
            var msg = await _schedulerRepository.InsertIntoDoctorSchedule(obj);
            return Ok(msg);
        }
        /// <summary>
        ///Update inot Doctor Schedule
        /// UI Reffered - Doctor Schedule,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateDoctorSchedule(DO_DoctorScheduler obj)
        {
            var msg = await _schedulerRepository.UpdateDoctorSchedule(obj);
            return Ok(msg);
        }
        /// <summary>
        ///Activate or De Activate Doctor Schedule
        /// UI Reffered - Doctor Schedule,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ActivateOrDeActivateDoctorSchedule(DO_DoctorScheduler obj)
        {
            var msg = await _schedulerRepository.ActivateOrDeActivateDoctorSchedule(obj);
            return Ok(msg);
        }
        #endregion
    }
}
