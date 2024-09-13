using eSya.ServiceProvider.DL.Repository;
using eSya.ServiceProvider.DO;
using eSya.ServiceProvider.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ServiceProvider.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorLeaveController : ControllerBase
    {
        private readonly IDoctorLeaveRepository _doctorLeaveRepository;
        public DoctorLeaveController(IDoctorLeaveRepository doctorLeaveRepository)
        {
            _doctorLeaveRepository = doctorLeaveRepository;
        }
        #region Doctor Leave
        /// <summary>
        /// Insert into Doctor Leave Table
        /// UI Reffered - Doctor Leave,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoDoctorLeave(DO_DoctorLeave obj)
        {
            var msg = await _doctorLeaveRepository.InsertIntoDoctorLeave(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Update Doctor Leave Table
        /// UI Reffered - Doctor Leave,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateDoctorLeave(DO_DoctorLeave obj)
        {
            var msg = await _doctorLeaveRepository.UpdateDoctorLeave(obj);
            return Ok(msg);
        }
        /// <summary>
        /// Activate Or DeActivate Doctor Leave Table
        /// UI Reffered - Doctor Leave,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ActivateOrDeActivateDoctorLeave(DO_DoctorLeave obj)
        {
            var msg = await _doctorLeaveRepository.ActivateOrDeActivateDoctorLeave(obj);
            return Ok(msg);
        }
        /// <summary>
        /// Get All Doctor Leave List
        /// UI Reffered - Doctor Leave,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorLeaveListAll(int Businesskey, int DoctorID)
        {
            var msg = await _doctorLeaveRepository.GetDoctorLeaveListAll(Businesskey, DoctorID);
            return Ok(msg);
        }

       
        #endregion
    }
}
