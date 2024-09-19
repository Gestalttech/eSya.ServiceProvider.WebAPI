﻿using eSya.ServiceProvider.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ServiceProvider.IF
{
    public interface IDoctorDayScheduleRepository
    {
        #region Doctor Day Schedule
        Task<List<DO_DoctorDaySchedule>> GetDoctordaySchedulebySearchCriteria(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID, int ConsultationID, DateTime ScheduleFromDate, DateTime ScheduleToDate);
        Task<DO_ReturnParameter> InsertIntoDoctordaySchedule(DO_DoctorDaySchedule obj);
        Task<DO_ReturnParameter> UpdateDoctordaySchedule(DO_DoctorDaySchedule obj);
        Task<DO_ReturnParameter> ActiveOrDeActiveDoctordaySchedule(DO_DoctorDaySchedule objdel);
        Task<DO_ReturnParameter> ImportedDoctorScheduleList(List<DO_DoctorDaySchedule> obj);
        #endregion
    }
}
