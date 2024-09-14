using eSya.ServiceProvider.DL.Repository;
using eSya.ServiceProvider.DO;
using eSya.ServiceProvider.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ServiceProvider.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleChangeController : ControllerBase
    {
        private readonly IScheduleChangeRepository _scheduleChangeRepository;

        public ScheduleChangeController(IScheduleChangeRepository scheduleChangeRepository)
        {
            _scheduleChangeRepository = scheduleChangeRepository;
        }
        #region Doctor Schedule change
        /// <summary>
        ///Get Schedule change Grid 
        /// UI Reffered - Doctor Schedule change,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorScheduleChangeList(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID, int ConsultationID, DateTime ScheduleChangeDate)
        {
            var ds = await _scheduleChangeRepository.GetDoctorScheduleChangeList(Businesskey, DoctorID, SpecialtyID, ClinicID, ConsultationID, ScheduleChangeDate);
            return Ok(ds);
        }
        /// <summary>
        ///Insert inot Doctor Schedule change
        /// UI Reffered - Doctor Schedule change,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoDoctorScheduleChange(DO_DoctorScheduler obj)
        {
            var msg = await _scheduleChangeRepository.InsertIntoDoctorScheduleChange(obj);
            return Ok(msg);
        }
        /// <summary>
        ///Update inot Doctor Schedule change
        /// UI Reffered - Doctor Schedule change,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateDoctorScheduleChange(DO_DoctorScheduler obj)
        {
            var msg = await _scheduleChangeRepository.UpdateDoctorScheduleChange(obj);
            return Ok(msg);
        }
        /// <summary>
        ///Activate or De Activate Doctor Schedule change
        /// UI Reffered - Doctor Schedule change,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ActivateOrDeActivateDoctorScheduleChange(DO_DoctorScheduler obj)
        {
            var msg = await _scheduleChangeRepository.ActivateOrDeActivateDoctorScheduleChange(obj);
            return Ok(msg);
        }
        #endregion
    }
}
