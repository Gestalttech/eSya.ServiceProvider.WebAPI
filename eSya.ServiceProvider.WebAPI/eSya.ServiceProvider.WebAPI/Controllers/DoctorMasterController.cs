using eSya.ServiceProvider.DL.Repository;
using eSya.ServiceProvider.DO;
using eSya.ServiceProvider.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.ServiceProvider.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorMasterController : ControllerBase
    {
        private readonly IDoctorMasterRepository _doctorMasterRepository;

        public DoctorMasterController(IDoctorMasterRepository doctorMasterRepository)
        {
            _doctorMasterRepository = doctorMasterRepository;
        }

        #region Doctor Master
        /// <summary>
        /// Insert into Doctor Master Table
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoDoctorMaster(DO_DoctorMaster obj)
        {
            var msg = await _doctorMasterRepository.InsertIntoDoctorMaster(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Update into Doctor Master Table
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateDoctorMaster(DO_DoctorMaster obj)
        {
            var msg = await _doctorMasterRepository.UpdateDoctorMaster(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Get All Doctor Master List
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorMasterList()
        {
            var msg = await _doctorMasterRepository.GetDoctorMasterList();
            return Ok(msg);
        }

        /// <summary>
        /// Get All Doctor Master List
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorMasterListForPrefix(string doctorNamePrefix)
        {
            var msg = await _doctorMasterRepository.GetDoctorMasterListForPrefix(doctorNamePrefix);
            return Ok(msg);
        }

        /// <summary>
        /// Get Specific Doctor Master Data
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorMaster(int doctorId)
        {
            var msg = await _doctorMasterRepository.GetDoctorMaster(doctorId);
            return Ok(msg);
        }

        /// <summary>
        /// Get Doctor Parameter List
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorParameterList(int doctorId)
        {
            var dparams = await _doctorMasterRepository.GetDoctorParameterList(doctorId);
            return Ok(dparams);
        }

        /// <summary>
        /// Active Or DeActive Doctor Master
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ActiveOrDeActiveDoctor(bool status, int doctorId)
        {
            var msg = await _doctorMasterRepository.ActiveOrDeActiveDoctor(status, doctorId);
            return Ok(msg);
        }

        #endregion

        #region About Doctor Details
        /// <summary>
        /// Insert Or Update Doctor Doctor Details
        /// UI Reffered - Doctor Details,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateIntoDoctordetails(Do_DoctorDetails obj)
        {
            var msg = await _doctorMasterRepository.InsertOrUpdateIntoDoctordetails(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Get Business Doctor Details by Doctor Id
        /// UI Reffered - Doctor Details,
        /// UI-Param-doctorId
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctordetailsbydoctorId(int doctorId)
        {
            var do_details = await _doctorMasterRepository.GetDoctordetailsbydoctorId(doctorId);
            return Ok(do_details);
        }
        #endregion 

        #region Doctor Profile Image
        /// <summary>
        /// Insert Or Update into Doctor Doctor Profile Image
        /// UI Reffered - Doctor Profile Master,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoDoctorProfileImage(DO_DoctorImage obj)
        {
            var msg = await _doctorMasterRepository.InsertIntoDoctorProfileImage(obj);
            return Ok(msg);
        }

        /// <summary>
        ///Get Image & Signature by doctor Id
        /// UI Reffered - Doctor Profile Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorProfileImagebyDoctorId(int doctorId)
        {
            var _imgs = await _doctorMasterRepository.GetDoctorProfileImagebyDoctorId(doctorId);
            return Ok(_imgs);
        }
        #endregion

        #region Doctor Profile Address
        /// <summary>
        ///Get STATES bY ISD Codes 
        /// UI Reffered - Doctor Profile Address,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetStatesbyIsdCode(int Isdcode)
        {
            var states = await _doctorMasterRepository.GetStatesbyIsdCode(Isdcode);
            return Ok(states);
        }

        /// <summary>
        ///Get CITIES bY STATE Code
        /// UI Reffered - Doctor Profile Address,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCitiesbyStateCode(int Isdcode, int statecode)
        {
            var cities = await _doctorMasterRepository.GetCitiesbyStateCode(Isdcode, statecode);
            return Ok(cities);
        }

        /// <summary>
        ///Get Zip Code bY City Code 
        /// UI Reffered - Doctor Profile Address,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetZipDescriptionbyCityCode(int Isdcode, int statecode, int citycode)
        {
            var zipcodes = await _doctorMasterRepository.GetZipDescriptionbyCityCode(Isdcode, statecode, citycode);
            return Ok(zipcodes);
        }

        /// <summary>
        ///Get Zip Code bY serial number 
        /// UI Reffered - Doctor Profile Address,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetZipCodeAndArea(int Isdcode, string zipcode)
        {
            var zipcodes = await _doctorMasterRepository.GetZipCodeAndArea(Isdcode, zipcode);
            return Ok(zipcodes);
        }
        /// <summary>
        ///Get Zip Code bY Area
        /// UI Reffered - Doctor Profile Address,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> FillCoumbosbyZipCode(int Isdcode, string zipcode)
        {
            var area = await _doctorMasterRepository.FillCoumbosbyZipCode(Isdcode, zipcode);
            return Ok(area);
        }


        /// <summary>
        ///Get Doctor Address  bY doctorId 
        /// UI Reffered - Doctor Profile Address,
        /// </summary>
        //[HttpGet]
        //public async Task<IActionResult> GetDoctorAddressDoctorId(int Isdcode, int doctorId, int businesskey)
        //{
        //    var zipcodes = await _DoctorMasterRepository.GetDoctorAddressDoctorId(Isdcode, doctorId, businesskey);
        //    return Ok(zipcodes);
        //}
        [HttpGet]
        public async Task<IActionResult> GetDoctorAddressDoctorId(int doctorId)
        {
            var zipcodes = await _doctorMasterRepository.GetDoctorAddressDoctorId(doctorId);
            return Ok(zipcodes);
        }
        /// <summary>
        /// Insert Or Update into Doctor  Address
        /// UI Reffered - Doctor Profile Master,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateIntoDoctorProfileAddress(DO_DoctorProfileAddress obj)
        {
            var msg = await _doctorMasterRepository.InsertOrUpdateIntoDoctorProfileAddress(obj);
            return Ok(msg);
        }
        #endregion

        #region Doctor Statutory Details
        /// <summary>
        ///Get Doctor Statutory Details by ISD Code
        /// UI Reffered - Doctor Profile Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorStatutoryDetails(int doctorId, int isdCode)
        {
            var _stdetails = await _doctorMasterRepository.GetDoctorStatutoryDetails(doctorId, isdCode);
            return Ok(_stdetails);
        }

        /// <summary>
        /// Insert Or Update into Doctor Statutory Details
        /// UI Reffered - Doctor Profile Master,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateDoctorStatutoryDetails(List<DO_DoctorStatutoryDetails> obj)
        {
            var msg = await _doctorMasterRepository.InsertOrUpdateDoctorStatutoryDetails(obj);
            return Ok(msg);
        }

        ///Get ISD by Business Key
        /// UI Reffered - Doctor Profile Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetISDCodesbyBusinessKey(int businessKey)
        {
            var Isd = await _doctorMasterRepository.GetISDCodesbyBusinessKey(businessKey);
            return Ok(Isd);
        }

        ///Get ISD by Doctor Id
        /// UI Reffered - Doctor Profile Master,
        /// </summary>
        [HttpGet]
        public IActionResult GetISDCodesbyDoctorId(int doctorId)
        {
            var Isd = _doctorMasterRepository.GetISDCodesbyDoctorId(doctorId);
            return Ok(Isd);
        }
        #endregion

        #region Doctor Business Link need to remove after freeze
        /// <summary>
        /// Get Doctor Business Link Data
        /// UI Reffered - Doctor Business Link,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorMasterBusinessList(int businessKey)
        {
            var msg = await _doctorMasterRepository.GetDoctorMasterBusinessList(businessKey);
            return Ok(msg);
        }

        /// <summary>
        /// Insert/ Update into Doctor Business Link Table
        /// UI Reffered - Doctor Business Link,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoDoctorBusinessLink(List<DO_DoctorMaster> obj)
        {
            var msg = await _doctorMasterRepository.InsertIntoDoctorBusinessLink(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Get Business Doctor Link Data
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetBusinessLocationDoctorList(int doctorId)
        {
            var msg = await _doctorMasterRepository.GetBusinessLocationDoctorList(doctorId);
            return Ok(msg);
        }

        /// <summary>
        /// Insert/ Update into Doctor Business Link Table
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoBusinessDoctorLink(List<DO_DoctorMaster> obj)
        {
            var msg = await _doctorMasterRepository.InsertIntoBusinessDoctorLink(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Get Business Specialty Link Data
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorBusinessKey(int doctorId)
        {
            var msg = await _doctorMasterRepository.GetDoctorBusinessKey(doctorId);
            return Ok(msg);
        }
        /// <summary>
        /// Get Business Keys by doctor Id for drop down
        /// UI Reffered - Doctor Speciality,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorLocationbyDoctorId(int doctorId)
        {
            var msg = await _doctorMasterRepository.GetDoctorLocationbyDoctorId(doctorId);
            return Ok(msg);
        }

      
        #endregion

        #region Doctor Profilr Business Link
        /// <summary>
        /// Get Doctor Business Link Data
        /// UI Reffered - Doctor Business Link,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorBusinessLinkList(int doctorId)
        {
            var msg = await _doctorMasterRepository.GetDoctorBusinessLinkList(doctorId);
            return Ok(msg);
        }

        /// <summary>
        /// Insert/ Update into Doctor Business Link Table
        /// UI Reffered - Doctor Business Link,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateDoctorBusinessLink(List<DO_DoctorBusinessLink> obj)
        {
            var msg = await _doctorMasterRepository.InsertOrUpdateDoctorBusinessLink(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Get Doctor Business Link Data
        /// UI Reffered - Doctor Business Link,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorLinkWithBusinessLocation(int doctorId)
        {
            var msg = await _doctorMasterRepository.GetDoctorLinkWithBusinessLocation(doctorId);
            return Ok(msg);
        }
        #endregion

        #region Specialty Doctor Link
        /// <summary>
        /// Get Business Specialty Link Data
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetSpecialtyListByDoctorId(int doctorId)
        {
            var msg = await _doctorMasterRepository.GetSpecialtyListByDoctorId(doctorId);
            return Ok(msg);
        }

        /// <summary>
        /// Insert into Doctor Specialty Link Table
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertDoctorSpecialtyLink(DO_SpecialtyDoctorLink obj)
        {
            var msg = await _doctorMasterRepository.InsertDoctorSpecialtyLink(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Update into Doctor Specialty Link Table
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateDoctorSpecialtyLink(DO_SpecialtyDoctorLink obj)
        {
            var msg = await _doctorMasterRepository.UpdateDoctorSpecialtyLink(obj);
            return Ok(msg);
        }

        /// <summary>
        /// Get Business Specialty Link Data
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetSpecialtyListByBKeyDoctorId(int businessKey, int doctorId)
        {
            var msg = await _doctorMasterRepository.GetSpecialtyListByBKeyDoctorId(businessKey, doctorId);
            return Ok(msg);
        }
        #endregion

        #region Doctor Clinic Link
        /// <summary>
        /// Get Doctor Clinic Link Data
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorClinicLinkList(int businessKey, int specialtyId, int doctorId)
        {
            var msg = await _doctorMasterRepository.GetDoctorClinicLinkList(businessKey, specialtyId, doctorId);
            return Ok(msg);
        }

        /// <summary>
        /// Get Doctor Clinic Link Data by clinic & Consultation Id 
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorClinicLinkListbyClinicConsultation(int businessKey, int clinicId, int consultationId)
        {
            var msg = await _doctorMasterRepository.GetDoctorClinicLinkListbyClinicConsultation(businessKey, clinicId, consultationId);
            return Ok(msg);
        }

        /// <summary>
        /// Insert Or Update into Doctor Clinic Link Table
        /// UI Reffered - Doctor Master,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertUpdateDoctorClinicLink(List<DO_DoctorClinic> obj)
        {
            var msg = await _doctorMasterRepository.InsertUpdateDoctorClinicLink(obj);
            return Ok(msg);
        }
        #endregion

        #region Doctor Profile Consultation Rates
        /// <summary>
        ///Get Doctor  Consultation Rates
        /// UI Reffered - Doctor Profile Master,
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDoctorProfileConsultationRatebyDoctorId(int businessKey, int clinictype, string currencycode, int ratetype, int doctorId)
        {
            var _stdetails = await _doctorMasterRepository.GetDoctorProfileConsultationRatebyDoctorId(businessKey, clinictype, currencycode, ratetype, doctorId);
            return Ok(_stdetails);
        }

        /// <summary>
        /// Insert Or Update into Doctor  Consultation Rates
        /// UI Reffered - Doctor Profile Master,
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateDoctorProfileConsultationRate(List<DO_DoctorProfileConsultationRate> obj)
        {
            var msg = await _doctorMasterRepository.AddOrUpdateDoctorProfileConsultationRate(obj);
            return Ok(msg);
        }
        #endregion

        
    }
}
