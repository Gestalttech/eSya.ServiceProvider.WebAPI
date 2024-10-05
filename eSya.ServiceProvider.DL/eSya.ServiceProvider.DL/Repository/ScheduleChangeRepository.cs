using eSya.ServiceProvider.DL.Entities;
using eSya.ServiceProvider.DO;
using eSya.ServiceProvider.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ServiceProvider.DL.Repository
{
    public class ScheduleChangeRepository: IScheduleChangeRepository
    {
        private readonly IStringLocalizer<ScheduleChangeRepository> _localizer;
        public ScheduleChangeRepository(IStringLocalizer<ScheduleChangeRepository> localizer)
        {
            _localizer = localizer;
        }
        #region Doctor Schedule Change
        public async Task<List<DO_DoctorScheduler>> GetExistingDoctorScheduledList(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID, int ConsultationID, DateTime ScheduleChangeDate)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = await db.GtEsdos1s

                        .Where(w => w.BusinessKey == Businesskey && w.DoctorId == DoctorID && w.SpecialtyId == SpecialtyID
                            && w.ClinicId == ClinicID && w.ConsultationId == ConsultationID && w.DayOfWeek == ScheduleChangeDate.DayOfWeek.ToString())
                        .AsNoTracking()
                        .Select(x => new DO_DoctorScheduler
                        {
                            DayOfWeek = x.DayOfWeek,
                            ScheduleFromTime = x.ScheduleFromTime,
                            ScheduleToTime = x.ScheduleToTime,
                            PatientCountPerHour = x.PatientCountPerHour,
                            TimeSlotInMins = x.TimeSlotInMins,
                            ActiveStatus = x.ActiveStatus

                        }).ToListAsync();


                    return dc_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<DO_DoctorScheduler>> GetDoctorScheduleChangeList(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID, int ConsultationID, DateTime ScheduleChangeDate)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = await db.GtEsdoscs

                        .Where(w => w.BusinessKey == Businesskey && w.DoctorId == DoctorID && w.SpecialtyId == SpecialtyID
                            && w.ClinicId == ClinicID && w.ConsultationId == ConsultationID && w.ScheduleChangeDate.Date == ScheduleChangeDate.Date)
                        .AsNoTracking()
                        .Select(x => new DO_DoctorScheduler
                        {
                            SerialNo=x.SerialNo,
                            ScheduleFromTime = x.ScheduleFromTime,
                            ScheduleToTime = x.ScheduleToTime,
                            PatientCountPerHour = x.PatientCountPerHour,
                            TimeSlotInMins = x.TimeSlotInMins,
                            ActiveStatus = x.ActiveStatus

                        }).ToListAsync();

                    return dc_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<DO_ReturnParameter> InsertIntoDoctorScheduleChange(DO_DoctorScheduler obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var ds_list = db.GtEsdoscs.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationID
                                     && x.ClinicId == obj.ClinicID && x.SpecialtyId == obj.SpecialtyID && x.DoctorId == obj.DoctorId
                                     && x.ScheduleChangeDate.Date == obj.ScheduleChangeDate.Date && x.ActiveStatus).ToList();
                        bool isexists = false;

                        foreach (var item in ds_list)
                        {
                            if ((obj.ScheduleFromTime >= item.ScheduleFromTime && obj.ScheduleFromTime < item.ScheduleToTime)
                                   || (obj.ScheduleToTime > item.ScheduleFromTime && obj.ScheduleToTime <= item.ScheduleToTime))
                            {
                                isexists = true;
                            }
                        }
                        if (isexists == true)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0146", Message = string.Format(_localizer[name: "W0146"]) };

                        }

                        int serialNumber = db.GtEsdoscs.Where(x => x.BusinessKey == obj.BusinessKey && x.ClinicId == obj.ClinicID && x.SpecialtyId == obj.SpecialtyID && x.DoctorId == obj.DoctorId && x.ConsultationId == obj.ConsultationID).Select(x => x.SerialNo).DefaultIfEmpty().Max() + 1;

                        var dMasterSchedule = new GtEsdosc
                        {
                            BusinessKey = obj.BusinessKey,
                            ConsultationId = obj.ConsultationID,
                            ClinicId = obj.ClinicID,
                            SpecialtyId = obj.SpecialtyID,
                            DoctorId = obj.DoctorId,
                            ScheduleChangeDate = obj.ScheduleChangeDate,
                            SerialNo= serialNumber,
                            ScheduleFromTime = obj.ScheduleFromTime,
                            ScheduleToTime = obj.ScheduleToTime,
                            PatientCountPerHour=obj.PatientCountPerHour,
                            TimeSlotInMins=obj.TimeSlotInMins,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID,

                        };
                        db.GtEsdoscs.Add(dMasterSchedule);

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0009", Message = string.Format(_localizer[name: "S0009"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> UpdateDoctorScheduleChange(DO_DoctorScheduler obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdosc doctorSchedule = db.GtEsdoscs.Where(x => x.BusinessKey == obj.BusinessKey && x.ClinicId == obj.ClinicID && x.SpecialtyId == obj.SpecialtyID && x.DoctorId == obj.DoctorId && x.ConsultationId == obj.ConsultationID && x.ScheduleChangeDate.Date == obj.ScheduleChangeDate.Date
                        && x.SerialNo==obj.SerialNo ).FirstOrDefault();
                        if (doctorSchedule == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0145", Message = string.Format(_localizer[name: "W0145"]) };

                        }
                        else
                        {
                            var ds_list = db.GtEsdoscs.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationID
                                      && x.ClinicId == obj.ClinicID && x.SpecialtyId == obj.SpecialtyID && x.DoctorId == obj.DoctorId
                                      && x.ScheduleChangeDate.Date == obj.ScheduleChangeDate.Date && x.ActiveStatus && x.SerialNo != obj.SerialNo).ToList();
                            bool isexists = false;

                            foreach (var item in ds_list)
                            {
                                if ((obj.ScheduleFromTime >= item.ScheduleFromTime && obj.ScheduleFromTime < item.ScheduleToTime)
                                       || (obj.ScheduleToTime > item.ScheduleFromTime && obj.ScheduleToTime <= item.ScheduleToTime))
                                {
                                    isexists = true;
                                }
                            }
                            if (isexists == true)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0146", Message = string.Format(_localizer[name: "W0146"]) };

                            }

                            doctorSchedule.ScheduleFromTime = obj.ScheduleFromTime;
                            doctorSchedule.ScheduleToTime = obj.ScheduleToTime;
                            doctorSchedule.PatientCountPerHour = obj.PatientCountPerHour;
                            doctorSchedule.TimeSlotInMins = obj.TimeSlotInMins;
                            doctorSchedule.ActiveStatus = obj.ActiveStatus;
                            doctorSchedule.ModifiedBy = obj.UserID;
                            doctorSchedule.ModifiedOn = System.DateTime.Now;
                            doctorSchedule.ModifiedTerminal = obj.TerminalID;

                        };

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0010", Message = string.Format(_localizer[name: "S0010"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> ActivateOrDeActivateDoctorScheduleChange(DO_DoctorScheduler obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdosc doctorSchedule = db.GtEsdoscs.Where(x => x.BusinessKey == obj.BusinessKey && x.ClinicId == obj.ClinicID && x.SpecialtyId == obj.SpecialtyID && x.DoctorId == obj.DoctorId && x.ConsultationId == obj.ConsultationID && x.ScheduleChangeDate.Date == obj.ScheduleChangeDate.Date &&x.SerialNo==obj.SerialNo).FirstOrDefault();
                        if (doctorSchedule == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0147", Message = string.Format(_localizer[name: "W0147"]) };

                        }
                        else
                        {

                            doctorSchedule.ActiveStatus = obj._status;
                            doctorSchedule.ModifiedBy = obj.UserID;
                            doctorSchedule.ModifiedOn = System.DateTime.Now;
                            doctorSchedule.ModifiedTerminal = obj.TerminalID;

                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (obj._status)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };

                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        #endregion
    }
}
