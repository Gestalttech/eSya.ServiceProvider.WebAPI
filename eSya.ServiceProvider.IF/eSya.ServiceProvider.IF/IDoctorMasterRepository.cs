using eSya.ServiceProvider.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ServiceProvider.IF
{
    public interface IDoctorMasterRepository
    {
        #region Doctor Master
        Task<DO_ReturnParameter> InsertIntoDoctorMaster(DO_DoctorMaster obj);
        Task<DO_ReturnParameter> UpdateDoctorMaster(DO_DoctorMaster obj);
        Task<List<DO_DoctorMaster>> GetDoctorMasterList();
        Task<List<DO_DoctorParameter>> GetDoctorParameterList(int doctorId);
        Task<List<DO_DoctorMaster>> GetDoctorMasterListForPrefix(string doctorNamePrefix);
        Task<DO_DoctorMaster> GetDoctorMaster(int doctorId);
        Task<DO_ReturnParameter> ActiveOrDeActiveDoctor(bool status, int doctorId);
        #endregion

        #region About Doctor Details
        Task<Do_DoctorDetails> GetDoctordetailsbydoctorId(int doctorId);
        Task<DO_ReturnParameter> InsertOrUpdateIntoDoctordetails(Do_DoctorDetails obj);
        #endregion

        #region Doctor Profile Image
        Task<DO_ReturnParameter> InsertIntoDoctorProfileImage(DO_DoctorImage obj);
        Task<DO_DoctorImage> GetDoctorProfileImagebyDoctorId(int doctorId);
        #endregion

        #region Doctor Profile Address
        Task<List<DO_DoctorProfileAddress>> GetStatesbyIsdCode(int Isdcode);
        Task<List<DO_DoctorProfileAddress>> GetCitiesbyStateCode(int Isdcode, int statecode);
        Task<List<DO_DoctorProfileAddress>> GetZipDescriptionbyCityCode(int Isdcode, int statecode, int citycode);
        Task<List<DO_DoctorProfileAddress>> GetZipCodeAndArea(int Isdcode, string zipcode);
        Task<DO_DoctorProfileAddress> FillCoumbosbyZipCode(int Isdcode, string zipcode);
        Task<DO_DoctorProfileAddress> GetDoctorAddressDoctorId(int doctorId);
        Task<DO_ReturnParameter> InsertOrUpdateIntoDoctorProfileAddress(DO_DoctorProfileAddress obj);
        #endregion

        #region Doctor Statutory Details
        Task<List<DO_DoctorStatutoryDetails>> GetDoctorStatutoryDetails(int doctorId, int isdCode);
        Task<DO_ReturnParameter> InsertOrUpdateDoctorStatutoryDetails(List<DO_DoctorStatutoryDetails> obj);
        Task<List<DO_ISDCodes>> GetISDCodesbyBusinessKey(int businessKey);
        List<DO_ISDCodes> GetISDCodesbyDoctorId(int doctorId);
        #endregion

        #region Doctor Business Link need to remove after freeze
        Task<List<DO_DoctorMaster>> GetDoctorMasterBusinessList(int businessKey);
        Task<DO_ReturnParameter> InsertIntoDoctorBusinessLink(List<DO_DoctorMaster> obj);
        Task<List<DO_BusinessLocation>> GetBusinessLocationDoctorList(int doctorId);
        Task<DO_ReturnParameter> InsertIntoBusinessDoctorLink(List<DO_DoctorMaster> obj);
        Task<List<DO_BusinessLocation>> GetDoctorBusinessKey(int doctorId);
        Task<List<DO_BusinessLocation>> GetDoctorLocationbyDoctorId(int doctorId);
        #endregion

        #region Doctor Profile Business Link 
        Task<List<DO_DoctorBusinessLink>> GetDoctorBusinessLinkList(int doctorId);
        Task<DO_ReturnParameter> InsertOrUpdateDoctorBusinessLink(List<DO_DoctorBusinessLink> obj);
        Task<List<DO_DoctorBusinessLink>> GetDoctorLinkWithBusinessLocation(int doctorId);
        #endregion

        #region Specialty Doctor Link
        Task<List<DO_SpecialtyDoctorLink>> GetSpecialtyListByDoctorId(int doctorId);
        Task<DO_ReturnParameter> InsertDoctorSpecialtyLink(DO_SpecialtyDoctorLink obj);
        Task<DO_ReturnParameter> UpdateDoctorSpecialtyLink(DO_SpecialtyDoctorLink obj);
        Task<List<DO_SpecialtyDoctorLink>> GetSpecialtyListByBKeyDoctorId(int businessKey, int doctorId);
        #endregion

        #region Doctor Clinic Link
        Task<List<DO_DoctorClinic>> GetDoctorClinicLinkList(int businessKey, int specialtyId, int doctorId);
        Task<DO_ReturnParameter> InsertUpdateDoctorClinicLink(List<DO_DoctorClinic> obj);
        Task<List<DO_DoctorClinic>> GetDoctorClinicLinkListbyClinicConsultation(int businessKey, int clinicId, int consultationId);
        #endregion

        #region Doctor Profile Consultation Rates
        Task<List<DO_DoctorProfileConsultationRate>> GetDoctorProfileConsultationRatebyDoctorId(int businessKey, int clinictype, string currencycode, int ratetype, int doctorId);
        Task<DO_ReturnParameter> AddOrUpdateDoctorProfileConsultationRate(List<DO_DoctorProfileConsultationRate> obj);
        #endregion

        

        
    }
}
