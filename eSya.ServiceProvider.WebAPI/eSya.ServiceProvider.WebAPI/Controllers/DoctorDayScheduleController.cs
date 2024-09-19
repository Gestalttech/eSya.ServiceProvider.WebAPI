using eSya.ServiceProvider.DL.Repository;
using eSya.ServiceProvider.DO;
using eSya.ServiceProvider.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ServiceProvider.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorDayScheduleController : ControllerBase
    {
        private readonly IDoctorDayScheduleRepository _doctorDayScheduleRepository;
        public DoctorDayScheduleController(IDoctorDayScheduleRepository doctorDayScheduleRepository)
        {
            _doctorDayScheduleRepository = doctorDayScheduleRepository;
        }
        #region Doctor Day Schedule
        /// <summary>
        ///Get Doctor Day Schedule Grid 
        /// UI Reffered - Doctor Day Schedule,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctordaySchedulebySearchCriteria(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID, int ConsultationID, DateTime ScheduleFromDate, DateTime ScheduleToDate)
        {
            var ds = await _doctorDayScheduleRepository.GetDoctordaySchedulebySearchCriteria(Businesskey, DoctorID, SpecialtyID, ClinicID, ConsultationID, ScheduleFromDate, ScheduleToDate);
            return Ok(ds);
        }
        /// <summary>
        ///Insert inot Doctor Day Schedule
        /// UI Reffered - Doctor Day Schedule,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoDoctordaySchedule(DO_DoctorDaySchedule obj)
        {
            var msg = await _doctorDayScheduleRepository.InsertIntoDoctordaySchedule(obj);
            return Ok(msg);
        }
        /// <summary>
        ///Update inot Doctor Day Schedule
        /// UI Reffered - Doctor Day Schedule,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateDoctordaySchedule(DO_DoctorDaySchedule obj)
        {
            var msg = await _doctorDayScheduleRepository.UpdateDoctordaySchedule(obj);
            return Ok(msg);
        }
        /// <summary>
        ///Activate or De Activate Doctor Day Schedule
        /// UI Reffered - Doctor Doctor Day Schedule,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ActiveOrDeActiveDoctordaySchedule(DO_DoctorDaySchedule objdel)
        {
            var msg = await _doctorDayScheduleRepository.ActiveOrDeActiveDoctordaySchedule(objdel);
            return Ok(msg);
        }
        /// <summary>
        ///Insert Excel uploaded Bulk Day Schedule
        /// UI Reffered - Doctor Day Schedule,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ImportedDoctorScheduleList(List<DO_DoctorDaySchedule> obj)
        {
            var msg = await _doctorDayScheduleRepository.ImportedDoctorScheduleList(obj);
            return Ok(msg);
        }
        #endregion
    }
}
