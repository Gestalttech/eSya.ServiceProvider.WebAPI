using eSya.ServiceProvider.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ServiceProvider.IF
{
    public interface IScheduleChangeRepository
    {
        #region Doctor Schedule Change
        Task<List<DO_DoctorScheduler>> GetDoctorScheduleChangeList(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID, int ConsultationID, DateTime ScheduleChangeDate);
        Task<DO_ReturnParameter> InsertIntoDoctorScheduleChange(DO_DoctorScheduler obj);
        Task<DO_ReturnParameter> UpdateDoctorScheduleChange(DO_DoctorScheduler obj);
        Task<DO_ReturnParameter> ActivateOrDeActivateDoctorScheduleChange(DO_DoctorScheduler obj);
        #endregion
    }
}
