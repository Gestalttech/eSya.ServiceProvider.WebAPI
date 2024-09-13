using eSya.ServiceProvider.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ServiceProvider.IF
{
    public interface IDoctorLeaveRepository
    {
        #region Doctor Leave
        Task<DO_ReturnParameter> InsertIntoDoctorLeave(DO_DoctorLeave obj);
        Task<DO_ReturnParameter> UpdateDoctorLeave(DO_DoctorLeave obj);
        Task<List<DO_DoctorLeave>> GetDoctorLeaveListAll(int Businesskey, int DoctorID);
        Task<DO_ReturnParameter> ActivateOrDeActivateDoctorLeave(DO_DoctorLeave obj);
        #endregion
    }
}
