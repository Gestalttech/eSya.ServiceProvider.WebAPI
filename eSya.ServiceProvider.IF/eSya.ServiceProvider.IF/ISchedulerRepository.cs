using eSya.ServiceProvider.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ServiceProvider.IF
{
    public interface ISchedulerRepository
    {
        #region Doctor Schedule
        Task<List<DO_DoctorMaster>> GetDoctorsbyBusinessKey(int Businesskey);
        Task<List<DO_SpecialtyDoctorLink>> GetSpecialtiesbyDoctorID(int Businesskey, int DoctorID);
        Task<List<DO_DoctorClinic>> GetClinicsbySpecialtyID(int Businesskey, int DoctorID, int SpecialtyID);
        Task<List<DO_DoctorClinic>> GetConsultationsbyClinicID(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID);
        Task<List<DO_DoctorScheduler>> GetDoctorScheduleList(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID, int ConsultationID);
        Task<DO_ReturnParameter> InsertIntoDoctorSchedule(DO_DoctorScheduler obj);
        Task<DO_ReturnParameter> UpdateDoctorSchedule(DO_DoctorScheduler obj);
        Task<DO_ReturnParameter> ActivateOrDeActivateDoctorSchedule(DO_DoctorScheduler obj);
        #endregion
    }
}
