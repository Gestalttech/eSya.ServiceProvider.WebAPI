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
    public class SchedulerRepository: ISchedulerRepository
    {
        private readonly IStringLocalizer<SchedulerRepository> _localizer;
        public SchedulerRepository(IStringLocalizer<SchedulerRepository> localizer)
        {
            _localizer = localizer;
        }
        #region Doctor Schedule
        public async Task<List<DO_DoctorMaster>> GetDoctorsbyBusinessKey(int Businesskey)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds =await db.GtEsdobls.Join
                        (db.GtEsdocds,
                        ld => new {ld.DoctorId},
                        dm=>new {dm.DoctorId},
                        (ld, dm) => new {ld,dm})
                        .Where(w =>w.ld.BusinessKey==Businesskey && w.ld.ActiveStatus && w.dm.ActiveStatus)
                        .Select(c => new DO_DoctorMaster
                        {
                            DoctorId = c.ld.DoctorId,
                            DoctorName = c.dm.DoctorName
                        }).ToListAsync();

                    var Distusers = ds.GroupBy(x => x.DoctorId).Select(y => y.First());
                    return Distusers.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_SpecialtyDoctorLink>> GetSpecialtiesbyDoctorID(int Businesskey,int DoctorID)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = await db.GtEsdosps.Join
                        (db.GtEsspcds,
                        ls => new { ls.SpecialtyId },
                        sm => new { sm.SpecialtyId },
                        (ls, sm) => new { ls, sm })
                        .Where(w => w.ls.BusinessKey == Businesskey && w.ls.ActiveStatus && w.ls.DoctorId== DoctorID && w.sm.ActiveStatus)
                        .Select(c => new DO_SpecialtyDoctorLink
                        {
                            SpecialtyID = c.ls.SpecialtyId,
                            SpecialtyDesc = c.sm.SpecialtyDesc
                        }).ToListAsync();

                    var Distusers = ds.GroupBy(x => x.SpecialtyID).Select(y => y.First());
                    return Distusers.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_DoctorClinic>> GetClinicsbySpecialtyID(int Businesskey, int DoctorID, int SpecialtyID)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = await db.GtEsdocls.Join
                        (db.GtEcapcds,
                        ls => new { ls.ClinicId },
                        sm => new { ClinicId = sm.ApplicationCode },
                        (ls, sm) => new { ls, sm })
                        .Where(w => w.ls.BusinessKey == Businesskey && w.ls.ActiveStatus && w.ls.DoctorId == DoctorID && w.ls.SpecialtyId == SpecialtyID
                        &&  w.sm.CodeType== CodeTypeValue.Clinic && w.sm.ActiveStatus)
                        .Select(c => new DO_DoctorClinic
                        {
                            ClinicId = c.ls.ClinicId,
                            ClinicDesc = c.sm.CodeDesc
                        }).ToListAsync();

                    var Distusers = ds.GroupBy(x => x.ClinicId).Select(y => y.First());
                    return Distusers.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_DoctorClinic>> GetConsultationsbyClinicID(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = await db.GtEsdocls.Join
                        (db.GtEcapcds,
                        ls => new { ls.ConsultationId },
                        sm => new { ConsultationId = sm.ApplicationCode },
                        (ls, sm) => new { ls, sm })
                        .Where(w => w.ls.BusinessKey == Businesskey && w.ls.ActiveStatus && w.ls.DoctorId == DoctorID && w.ls.SpecialtyId == SpecialtyID
                        && w.ls.ClinicId== ClinicID && w.sm.CodeType == CodeTypeValue.ConsultationType && w.sm.ActiveStatus)
                        .Select(c => new DO_DoctorClinic
                        {
                            ConsultationId = c.ls.ConsultationId,
                            ConsultationDesc = c.sm.CodeDesc
                        }).ToListAsync();

                    var Distusers = ds.GroupBy(x => x.ConsultationId).Select(y => y.First());
                    return Distusers.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_DoctorScheduler>> GetDoctorScheduleList(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID, int ConsultationID)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = db.GtEsdos1s.Where(x=>x.BusinessKey==Businesskey && x.DoctorId==DoctorID && x.SpecialtyId==SpecialtyID &&
                    x.ClinicId==ClinicID && x.ConsultationId==ConsultationID)
                        .AsNoTracking()
                        .Select(d => new DO_DoctorScheduler
                        {
                            BusinessKey = d.BusinessKey,
                            ConsultationID = d.ConsultationId,
                            ClinicID = d.ClinicId,
                            SpecialtyID =d.SpecialtyId,
                            DoctorId = d.DoctorId,
                            DayOfWeek = d.DayOfWeek,
                            SerialNo = d.SerialNo,
                            ScheduleFromTime =d.ScheduleFromTime,
                            ScheduleToTime = d.ScheduleToTime,
                            PatientCountPerHour = d.PatientCountPerHour,
                            TimeSlotInMins = d.TimeSlotInMins,
                            Week1 = d.Week1,
                            Week2 = d.Week2,
                            Week3 = d.Week3,
                            Week4 = d.Week4,
                            Week5 = d.Week5,
                            RoomNo = d.RoomNo,
                            ActiveStatus = d.ActiveStatus
                        })//.OrderBy(x => new { x.SpecialtyDesc, x.ClinicDesc, x.ConsultationType, x.SerialNo })
                        .ToListAsync();

                    return await dc_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<DO_ReturnParameter> InsertIntoDoctorSchedule(DO_DoctorScheduler obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                       
                        var dc_list = db.GtEsdos1s.Where(x => x.DoctorId == obj.DoctorId && x.ActiveStatus
                                        && x.DayOfWeek.ToUpper().Trim() == obj.DayOfWeek.ToUpper().Trim()
                                        && ((obj.Week1 && x.Week1 == obj.Week1) || (obj.Week2 && x.Week2 == obj.Week2)
                                            || (obj.Week3 && x.Week3 == obj.Week3) || (obj.Week4 && x.Week4 == obj.Week4)
                                            || (obj.Week5 && x.Week5 == obj.Week5))).ToList();
                        bool isexists = false;
                        foreach (var item in dc_list)
                        {
                            if ((obj.ScheduleFromTime >= item.ScheduleFromTime && obj.ScheduleFromTime < item.ScheduleToTime)
                                   || (obj.ScheduleToTime > item.ScheduleFromTime && obj.ScheduleToTime <= item.ScheduleToTime))

                            {
                                isexists = true;
                            }
                        }
                       
                        int serialNumber = db.GtEsdos1s.Where(x => x.BusinessKey == obj.BusinessKey && x.ClinicId == obj.ClinicID && x.SpecialtyId == obj.SpecialtyID && x.DoctorId == obj.DoctorId && x.ConsultationId == obj.ConsultationID).Select(x => x.SerialNo).DefaultIfEmpty().Max() + 1;

                        var dschedule = new GtEsdos1
                        {
                            BusinessKey = obj.BusinessKey,
                            ConsultationId = obj.ConsultationID,
                            ClinicId = obj.ClinicID,
                            SpecialtyId = obj.SpecialtyID,
                            DoctorId = obj.DoctorId,
                            DayOfWeek = obj.DayOfWeek,
                            SerialNo = serialNumber,
                            ScheduleFromTime = obj.ScheduleFromTime,
                            ScheduleToTime = obj.ScheduleToTime,
                            PatientCountPerHour = obj.PatientCountPerHour,
                            TimeSlotInMins=obj.TimeSlotInMins,
                            Week1 = obj.Week1,
                            Week2 = obj.Week2,
                            Week3 = obj.Week3,
                            Week4 = obj.Week4,
                            Week5 = obj.Week5,
                            RoomNo = obj.RoomNo,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID,

                        };
                        db.GtEsdos1s.Add(dschedule);

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0008", Message = string.Format(_localizer[name: "S0008"]) };
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
        public async Task<DO_ReturnParameter> UpdateDoctorSchedule(DO_DoctorScheduler obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdos1 doctorSchedule = db.GtEsdos1s.Where(x => x.BusinessKey == obj.BusinessKey && x.ClinicId == obj.ClinicID && x.SpecialtyId == obj.SpecialtyID && x.DoctorId == obj.DoctorId && x.ConsultationId == obj.ConsultationID && x.DayOfWeek == obj.DayOfWeek && x.SerialNo == obj.SerialNo).FirstOrDefault();
                        if (doctorSchedule == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0143", Message = string.Format(_localizer[name: "W0143"]) };

                        }
                        else
                        {
                            bool isexists = false;
                            var dc_list = db.GtEsdos1s.Where(x => x.DoctorId == obj.DoctorId && x.ActiveStatus
                                            && x.DayOfWeek.ToUpper().Trim() == obj.DayOfWeek.ToUpper().Trim()
                                            && ((obj.Week1 && x.Week1 == obj.Week1) || (obj.Week2 && x.Week2 == obj.Week2)
                                                || (obj.Week3 && x.Week3 == obj.Week3) || (obj.Week4 && x.Week4 == obj.Week4)
                                                || (obj.Week5 && x.Week5 == obj.Week5))
                                            && x.SerialNo != obj.SerialNo).ToList();
                            foreach (var item in dc_list)
                            {
                                if ((obj.ScheduleFromTime >= item.ScheduleFromTime && obj.ScheduleFromTime < item.ScheduleToTime)
                                   || (obj.ScheduleToTime > item.ScheduleFromTime && obj.ScheduleToTime <= item.ScheduleToTime))

                                {
                                    isexists = true;
                                }
                            }
                           
                            doctorSchedule.DayOfWeek = obj.DayOfWeek;
                            doctorSchedule.ScheduleFromTime = obj.ScheduleFromTime;
                            doctorSchedule.ScheduleToTime = obj.ScheduleToTime;
                            doctorSchedule.PatientCountPerHour = obj.PatientCountPerHour;
                            doctorSchedule.TimeSlotInMins= obj.TimeSlotInMins;
                            doctorSchedule.Week1 = obj.Week1;
                            doctorSchedule.Week2 = obj.Week2;
                            doctorSchedule.Week3 = obj.Week3;
                            doctorSchedule.Week4 = obj.Week4;
                            doctorSchedule.Week5 = obj.Week5;
                            doctorSchedule.RoomNo = obj.RoomNo;
                            doctorSchedule.ActiveStatus = obj.ActiveStatus;
                            doctorSchedule.ModifiedBy = obj.UserID;
                            doctorSchedule.ModifiedOn = System.DateTime.Now;
                            doctorSchedule.ModifiedTerminal = obj.TerminalID;

                        };

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0007", Message = string.Format(_localizer[name: "S0007"]) };
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
        public async Task<DO_ReturnParameter> ActivateOrDeActivateDoctorSchedule(DO_DoctorScheduler obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdos1 doctorSchedule = db.GtEsdos1s.Where(x => x.BusinessKey == obj.BusinessKey && x.ClinicId == obj.ClinicID && x.SpecialtyId == obj.SpecialtyID && x.DoctorId == obj.DoctorId && x.ConsultationId == obj.ConsultationID && x.DayOfWeek == obj.DayOfWeek && x.SerialNo == obj.SerialNo).FirstOrDefault();
                        if (doctorSchedule == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0143", Message = string.Format(_localizer[name: "W0143"]) };

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
