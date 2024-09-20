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
    public class DoctorDayScheduleRepository: IDoctorDayScheduleRepository
    {
        private readonly IStringLocalizer<DoctorDayScheduleRepository> _localizer;
        public DoctorDayScheduleRepository(IStringLocalizer<DoctorDayScheduleRepository> localizer)
        {
            _localizer = localizer;
        }
        #region Doctor Day Schedule upload
       

        public async Task<DO_ReturnParameter> ImportedDoctorScheduleList(List<DO_DoctorDaySchedule> obj)
        {
            using (var db = new eSyaEnterprise())
            {

                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        foreach (var time in obj)
                        {
                            if (time.ScheduleFromTime >= time.ScheduleToTime)
                            {
                                return new DO_ReturnParameter() { Status = false, Message = time.ScheduleFromTime + "From Time can't be more than or equal to" + time.ScheduleToTime + "To Time." };
                            }

                            var doctor = db.GtEsdocds.Where(x => x.DoctorName.ToUpper().Trim().Replace(" ", "") == time.DoctorName.ToUpper().Trim().Replace(" ", "")).FirstOrDefault();
                            if (doctor == null)
                            {
                                return new DO_ReturnParameter() { Status = false, Message = "Doctor:" + time.DoctorName + "is not avalabe" };
                            }
                            else
                            {
                                time.DoctorId = doctor.DoctorId;
                            }
                            var clinic = db.GtEcapcds.Where(x => x.CodeDesc.ToUpper().Trim().Replace(" ", "") == time.ClinicDesc.ToUpper().Trim().Replace(" ", "").Trim()).FirstOrDefault();
                            if (clinic == null)
                            {
                                return new DO_ReturnParameter() { Status = false, Message = "Doctor:" + time.ClinicDesc + "is not avalabe" };
                            }
                            else
                            {
                                time.ClinicId = clinic.ApplicationCode;
                            }
                            var consultation = db.GtEcapcds.Where(x => x.CodeDesc.ToUpper().Trim().Replace(" ", "") == time.ConsultationDesc.ToUpper().Trim().Replace(" ", "")).FirstOrDefault();
                            if (consultation == null)
                            {
                                return new DO_ReturnParameter() { Status = false, Message = "Doctor:" + time.ConsultationDesc + "is not avalabe" };
                            }
                            else
                            {
                                time.ConsultationId = consultation.ApplicationCode;
                            }
                            var specialty = db.GtEsspcds.Where(x => x.SpecialtyDesc.ToUpper().Trim().Replace(" ", "") == time.SpecialtyDesc.ToUpper().Trim().Replace(" ", "")).FirstOrDefault();
                            if (specialty == null)
                            {
                                return new DO_ReturnParameter() { Status = false, Message = "Doctor:" + time.SpecialtyDesc + "is not avalabe" };
                            }
                            else
                            {
                                time.SpecialtyId = specialty.SpecialtyId;
                            }


                            var ds_list = db.GtEsdos2s.Where(x => x.BusinessKey == time.BusinessKey && x.ConsultationId == time.ConsultationId
                                      && x.ClinicId == time.ClinicId && x.SpecialtyId == time.SpecialtyId && x.DoctorId == time.DoctorId
                                      && x.ScheduleDate.Date == time.ScheduleDate.Date && x.ActiveStatus).ToList();

                            bool isexists = false;
                            foreach (var _isexists in ds_list)
                            {
                                if ((time.ScheduleFromTime >= _isexists.ScheduleFromTime && time.ScheduleFromTime < _isexists.ScheduleToTime)
                                       || (time.ScheduleToTime > _isexists.ScheduleFromTime && time.ScheduleToTime <= _isexists.ScheduleToTime))
                                {
                                    isexists = true;
                                }
                            }
                            if (isexists == true)
                            {
                                return new DO_ReturnParameter() { Status = false, Message = "Time slot for Date and From Time:" + time.ScheduleDate.Date.Add(time.ScheduleFromTime) +" "+ "Date and To Time:" + time.ScheduleDate.Date.Add(time.ScheduleToTime)+ "is already exists for Doctor:"+" " + time.DoctorName };
                            }

                            var scheduled = await db.GtEsdos2s.Where(x => x.BusinessKey == time.BusinessKey && x.ConsultationId == time.ConsultationId
                                     && x.ClinicId == time.ClinicId && x.SpecialtyId == time.SpecialtyId && x.DoctorId == time.DoctorId
                                     && x.ScheduleDate.Date == time.ScheduleDate.Date && x.SerialNo == time.SerialNo).FirstOrDefaultAsync();

                            if (scheduled == null)
                            {
                                //int serialNumber = db.GtEsdos2s.Where(x => x.BusinessKey == time.BusinessKey && x.ConsultationId == time.ConsultationId && x.ClinicId == time.ClinicId && x.SpecialtyId == time.SpecialtyId && x.DoctorId == time.DoctorId && x.ScheduleDate.Date == time.ScheduleDate.Date).Select(x => x.SerialNo).DefaultIfEmpty().Max() + 1;
                                int serialNumber = db.GtEsdos2s.Where(x => x.BusinessKey == time.BusinessKey && x.ConsultationId == time.ConsultationId && x.ClinicId == time.ClinicId && x.SpecialtyId == time.SpecialtyId && x.DoctorId == time.DoctorId).Select(x => x.SerialNo).DefaultIfEmpty().Max() + 1;
                                var do_sc = new GtEsdos2
                                {
                                    BusinessKey = time.BusinessKey,
                                    ConsultationId = time.ConsultationId,
                                    ClinicId = time.ClinicId,
                                    SpecialtyId = time.SpecialtyId,
                                    DoctorId = time.DoctorId,
                                    ScheduleDate = time.ScheduleDate,
                                    SerialNo = serialNumber,
                                    ScheduleFromTime = time.ScheduleFromTime,
                                    ScheduleToTime = time.ScheduleToTime,
                                    NoOfPatients = time.NoOfPatients,
                                    XlsheetReference = time.XlsheetReference,
                                    ActiveStatus = time.ActiveStatus,
                                    FormId = time.FormID,
                                    CreatedBy = time.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = time.TerminalID,
                                };

                                db.GtEsdos2s.Add(do_sc);
                                await db.SaveChangesAsync();
                            }
                            else
                            {
                                scheduled.ScheduleFromTime = time.ScheduleFromTime;
                                scheduled.ScheduleToTime = time.ScheduleToTime;
                                scheduled.NoOfPatients = time.NoOfPatients;
                                scheduled.XlsheetReference = time.XlsheetReference;
                                scheduled.ActiveStatus = time.ActiveStatus;
                                scheduled.ModifiedBy = time.UserID;
                                scheduled.ModifiedOn = System.DateTime.Now;
                                scheduled.ModifiedTerminal = time.TerminalID;
                                await db.SaveChangesAsync();
                            }

                        }
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0015", Message = string.Format(_localizer[name: "S0015"]) };

                    }
                    catch (DbUpdateException ex)
                    {

                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }

                }
            }

        }
        //public async Task<List<DO_DoctorDaySchedule>> GetUploadedDoctordaySchedulebySearchCriteria(int Businesskey, DateTime? ScheduleFromDate, DateTime? ScheduleToDate)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        try
        //        {
        //            var dc_sc = db.GtEsdos2s
        //                .Join(db.GtEsspcds,
        //                o => new { o.SpecialtyId },
        //                s => new { s.SpecialtyId },
        //                (o, s) => new { o, s })
        //                .Join(db.GtEsdocds,
        //                os => new { os.o.DoctorId },
        //                d => new { d.DoctorId },
        //                (os, d) => new { os, d })

        //                .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.Clinic),
        //                    l => new { l.os.o.ClinicId },
        //                    c => new { ClinicId = c.ApplicationCode },
        //                    (l, c) => new { l, c })
        //                .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.ConsultationType),
        //                    lc => new { lc.l.os.o.ConsultationId },
        //                    ol => new { ConsultationId = ol.ApplicationCode },
        //                    (lc, ol) => new { lc, ol })
        //                .Where(w => w.lc.l.os.o.ScheduleDate.Date >= ScheduleFromDate.Date
        //                 && w.lc.l.os.o.ScheduleDate.Date <= ScheduleToDate.Date)

        //                .AsNoTracking()

        //                .Select(x => new DO_DoctorDaySchedule
        //                {

        //                    BusinessKey = x.lc.l.os.o.BusinessKey,
        //                    ConsultationId = x.lc.l.os.o.ConsultationId,
        //                    ConsultationDesc = x.ol.CodeDesc,
        //                    ClinicId = x.lc.l.os.o.ClinicId,
        //                    ClinicDesc = x.lc.c.CodeDesc,
        //                    SpecialtyId = x.lc.l.os.o.SpecialtyId,
        //                    SpecialtyDesc = x.lc.l.os.s.SpecialtyDesc,
        //                    DoctorId = x.lc.l.os.o.DoctorId,
        //                    DoctorName = x.lc.l.d.DoctorName,
        //                    ScheduleDate = x.lc.l.os.o.ScheduleDate,
        //                    SerialNo = x.lc.l.os.o.SerialNo,
        //                    ScheduleFromTime = x.lc.l.os.o.ScheduleFromTime,
        //                    ScheduleToTime = x.lc.l.os.o.ScheduleToTime,
        //                    NoOfPatients = x.lc.l.os.o.NoOfPatients,
        //                    XlsheetReference = x.lc.l.os.o.XlsheetReference,
        //                    ActiveStatus = x.lc.l.os.o.ActiveStatus
        //                })
        //                .ToListAsync();

        //            return await dc_sc;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
        public async Task<List<DO_DoctorDaySchedule>> GetUploadedDoctordaySchedulebySearchCriteria(int Businesskey, DateTime? ScheduleFromDate, DateTime? ScheduleToDate)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_sc = db.GtEsdos2s
                        .Join(db.GtEsspcds,
                            o => new { o.SpecialtyId },
                            s => new { s.SpecialtyId },
                            (o, s) => new { o, s })
                        .Join(db.GtEsdocds,
                            os => new { os.o.DoctorId },
                            d => new { d.DoctorId },
                            (os, d) => new { os, d })
                        .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.Clinic),
                            l => new { l.os.o.ClinicId },
                            c => new { ClinicId = c.ApplicationCode },
                            (l, c) => new { l, c })
                        .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.ConsultationType),
                            lc => new { lc.l.os.o.ConsultationId },
                            ol => new { ConsultationId = ol.ApplicationCode },
                            (lc, ol) => new { lc, ol })
                        // Date filter only when ScheduleFromDate or ScheduleToDate is not null
                        .Where(w => w.lc.l.os.o.BusinessKey==Businesskey
                                && (!ScheduleFromDate.HasValue || w.lc.l.os.o.ScheduleDate.Date >= ScheduleFromDate.Value.Date)
                                && (!ScheduleToDate.HasValue || w.lc.l.os.o.ScheduleDate.Date <= ScheduleToDate.Value.Date))

                        .AsNoTracking()
                        .Select(x => new DO_DoctorDaySchedule
                        {
                            BusinessKey = x.lc.l.os.o.BusinessKey,
                            ConsultationId = x.lc.l.os.o.ConsultationId,
                            ConsultationDesc = x.ol.CodeDesc,
                            ClinicId = x.lc.l.os.o.ClinicId,
                            ClinicDesc = x.lc.c.CodeDesc,
                            SpecialtyId = x.lc.l.os.o.SpecialtyId,
                            SpecialtyDesc = x.lc.l.os.s.SpecialtyDesc,
                            DoctorId = x.lc.l.os.o.DoctorId,
                            DoctorName = x.lc.l.d.DoctorName,
                            ScheduleDate = x.lc.l.os.o.ScheduleDate,
                            SerialNo = x.lc.l.os.o.SerialNo,
                            ScheduleFromTime = x.lc.l.os.o.ScheduleFromTime,
                            ScheduleToTime = x.lc.l.os.o.ScheduleToTime,
                            NoOfPatients = x.lc.l.os.o.NoOfPatients,
                            XlsheetReference = x.lc.l.os.o.XlsheetReference,
                            ActiveStatus = x.lc.l.os.o.ActiveStatus
                        })
                        .ToListAsync();

                    return await dc_sc;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Schedule Export
        //public async Task<List<DO_DoctorDaySchedule>> GetDoctordaySchedulebySearchCriteria(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID, int ConsultationID, DateTime ScheduleFromDate, DateTime ScheduleToDate)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        try
        //        {
        //            var dc_sc = db.GtEsdos2s
        //                .Join(db.GtEsspcds,
        //                o => new { o.SpecialtyId },
        //                s => new { s.SpecialtyId },
        //                (o, s) => new { o, s })
        //                .Join(db.GtEsdocds,
        //                os => new { os.o.DoctorId },
        //                d => new { d.DoctorId },
        //                (os, d) => new { os, d })

        //                .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.Clinic),
        //                    l => new { l.os.o.ClinicId },
        //                    c => new { ClinicId = c.ApplicationCode },
        //                    (l, c) => new { l, c })
        //                .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.ConsultationType),
        //                    lc => new { lc.l.os.o.ConsultationId },
        //                    ol => new { ConsultationId = ol.ApplicationCode },
        //                    (lc, ol) => new { lc, ol })
        //                .Where(w => w.lc.l.os.o.BusinessKey == Businesskey && w.lc.l.os.o.ClinicId == ClinicID && w.lc.l.os.o.ConsultationId == ConsultationID
        //                 && w.lc.l.os.o.SpecialtyId == SpecialtyID && w.lc.l.os.o.DoctorId == DoctorID && w.lc.l.os.o.ScheduleDate.Date >= ScheduleFromDate.Date
        //                 && w.lc.l.os.o.ScheduleDate.Date <= ScheduleToDate.Date)

        //                .AsNoTracking()

        //                .Select(x => new DO_DoctorDaySchedule
        //                {

        //                    BusinessKey = x.lc.l.os.o.BusinessKey,
        //                    ConsultationId = x.lc.l.os.o.ConsultationId,
        //                    ConsultationDesc = x.ol.CodeDesc,
        //                    ClinicId = x.lc.l.os.o.ClinicId,
        //                    ClinicDesc = x.lc.c.CodeDesc,
        //                    SpecialtyId = x.lc.l.os.o.SpecialtyId,
        //                    SpecialtyDesc = x.lc.l.os.s.SpecialtyDesc,
        //                    DoctorId = x.lc.l.os.o.DoctorId,
        //                    DoctorName = x.lc.l.d.DoctorName,
        //                    ScheduleDate = x.lc.l.os.o.ScheduleDate,
        //                    SerialNo = x.lc.l.os.o.SerialNo,
        //                    ScheduleFromTime = x.lc.l.os.o.ScheduleFromTime,
        //                    ScheduleToTime = x.lc.l.os.o.ScheduleToTime,
        //                    NoOfPatients = x.lc.l.os.o.NoOfPatients,
        //                    XlsheetReference = x.lc.l.os.o.XlsheetReference,
        //                    ActiveStatus = x.lc.l.os.o.ActiveStatus
        //                })
        //                .ToListAsync();

        //            return await dc_sc;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
        public async Task<List<DO_DoctorDaySchedule>> GetDoctordaySchedulebySearchCriteria(int Businesskey, int DoctorID, int SpecialtyID, int ClinicID, int ConsultationID, DateTime? ScheduleFromDate, DateTime? ScheduleToDate)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var query = db.GtEsdos2s
                        .Join(db.GtEsspcds,
                            o => new { o.SpecialtyId },
                            s => new { s.SpecialtyId },
                            (o, s) => new { o, s })
                        .Join(db.GtEsdocds,
                            os => new { os.o.DoctorId },
                            d => new { d.DoctorId },
                            (os, d) => new { os, d })
                        .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.Clinic),
                            l => new { l.os.o.ClinicId },
                            c => new { ClinicId = c.ApplicationCode },
                            (l, c) => new { l, c })
                        .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.ConsultationType),
                            lc => new { lc.l.os.o.ConsultationId },
                            ol => new { ConsultationId = ol.ApplicationCode },
                            (lc, ol) => new { lc, ol })
                        // Always filter by BusinessKey
                        .Where(w => w.lc.l.os.o.BusinessKey == Businesskey);

                    // Conditionally filter by DoctorID, if provided
                    if (DoctorID!=0)
                    {
                        query = query.Where(w => w.lc.l.os.o.DoctorId == DoctorID);
                    }

                    // Conditionally filter by SpecialtyID, if provided
                    if (SpecialtyID!=0)
                    {
                        query = query.Where(w => w.lc.l.os.o.SpecialtyId == SpecialtyID);
                    }

                    // Conditionally filter by ClinicID, if provided
                    if (ClinicID!=0)
                    {
                        query = query.Where(w => w.lc.l.os.o.ClinicId == ClinicID);
                    }

                    // Conditionally filter by ConsultationID, if provided
                    if (ConsultationID!=0)
                    {
                        query = query.Where(w => w.lc.l.os.o.ConsultationId == ConsultationID);
                    }

                    // Conditionally filter by ScheduleFromDate and ScheduleToDate
                    if (ScheduleFromDate.HasValue && ScheduleToDate.HasValue)
                    {
                        query = query.Where(w => w.lc.l.os.o.ScheduleDate.Date >= ScheduleFromDate.Value.Date
                                                  && w.lc.l.os.o.ScheduleDate.Date <= ScheduleToDate.Value.Date);
                    }

                    var dc_sc = await query
                        .AsNoTracking()
                        .Select(x => new DO_DoctorDaySchedule
                        {
                            BusinessKey = x.lc.l.os.o.BusinessKey,
                            ConsultationId = x.lc.l.os.o.ConsultationId,
                            ConsultationDesc = x.ol.CodeDesc,
                            ClinicId = x.lc.l.os.o.ClinicId,
                            ClinicDesc = x.lc.c.CodeDesc,
                            SpecialtyId = x.lc.l.os.o.SpecialtyId,
                            SpecialtyDesc = x.lc.l.os.s.SpecialtyDesc,
                            DoctorId = x.lc.l.os.o.DoctorId,
                            DoctorName = x.lc.l.d.DoctorName,
                            ScheduleDate = x.lc.l.os.o.ScheduleDate,
                            SerialNo = x.lc.l.os.o.SerialNo,
                            ScheduleFromTime = x.lc.l.os.o.ScheduleFromTime,
                            ScheduleToTime = x.lc.l.os.o.ScheduleToTime,
                            NoOfPatients = x.lc.l.os.o.NoOfPatients,
                            XlsheetReference = x.lc.l.os.o.XlsheetReference,
                            ActiveStatus = x.lc.l.os.o.ActiveStatus
                        })
                        .ToListAsync();

                    return dc_sc;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<DO_ReturnParameter> InsertIntoDoctordaySchedule(DO_DoctorDaySchedule obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var ds_list = db.GtEsdos2s.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationId
                                      && x.ClinicId == obj.ClinicId && x.SpecialtyId == obj.SpecialtyId && x.DoctorId == obj.DoctorId
                                      && x.ScheduleDate.Date == obj.ScheduleDate.Date && x.ActiveStatus).ToList();

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
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0148", Message = string.Format(_localizer[name: "W0148"]) };

                        }

                        else
                        {
                            //int serialNumber = db.GtEsdos2s.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationId && x.ClinicId == obj.ClinicId && x.SpecialtyId == obj.SpecialtyId && x.DoctorId == obj.DoctorId && x.ScheduleDate.Date == obj.ScheduleDate.Date).Select(x => x.SerialNo).DefaultIfEmpty().Max() + 1;
                            int serialNumber = db.GtEsdos2s.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationId && x.ClinicId == obj.ClinicId && x.SpecialtyId == obj.SpecialtyId && x.DoctorId == obj.DoctorId).Select(x => x.SerialNo).DefaultIfEmpty().Max() + 1;

                            var do_sc = new GtEsdos2
                            {
                                BusinessKey = obj.BusinessKey,
                                ConsultationId = obj.ConsultationId,
                                ClinicId = obj.ClinicId,
                                SpecialtyId = obj.SpecialtyId,
                                DoctorId = obj.DoctorId,
                                ScheduleDate = obj.ScheduleDate,
                                SerialNo = serialNumber,
                                ScheduleFromTime = obj.ScheduleFromTime,
                                ScheduleToTime = obj.ScheduleToTime,
                                NoOfPatients = obj.NoOfPatients,
                                //XlsheetReference = "#",
                                XlsheetReference = obj.XlsheetReference,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID,
                            };

                            db.GtEsdos2s.Add(do_sc);
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0011", Message = string.Format(_localizer[name: "S0011"]) };
                        }
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

        public async Task<DO_ReturnParameter> UpdateDoctordaySchedule(DO_DoctorDaySchedule obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdos2 _daySchedule = db.GtEsdos2s.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationId && x.ClinicId == obj.ClinicId && x.SpecialtyId == obj.SpecialtyId && x.DoctorId == obj.DoctorId && x.ScheduleDate.Date == obj.ScheduleDate.Date && x.SerialNo == obj.SerialNo).FirstOrDefault();
                        if (_daySchedule == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0150", Message = string.Format(_localizer[name: "W0150"]) };
                        }
                        else
                        {
                            bool isexists = false;
                            var ds_list = db.GtEsdos2s.Where(x => x.BusinessKey == obj.BusinessKey && x.ConsultationId == obj.ConsultationId
                                      && x.ClinicId == obj.ClinicId && x.SpecialtyId == obj.SpecialtyId && x.DoctorId == obj.DoctorId
                                      && x.ScheduleDate.Date == obj.ScheduleDate.Date && x.ActiveStatus && x.SerialNo != obj.SerialNo).ToList();

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
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0148", Message = string.Format(_localizer[name: "W0148"]) };
                            }

                            _daySchedule.ScheduleFromTime = obj.ScheduleFromTime;
                            _daySchedule.ScheduleToTime = obj.ScheduleToTime;
                            _daySchedule.NoOfPatients = obj.NoOfPatients;
                            _daySchedule.XlsheetReference = obj.XlsheetReference;
                            _daySchedule.ActiveStatus = obj.ActiveStatus;
                            _daySchedule.ModifiedBy = obj.UserID;
                            _daySchedule.ModifiedOn = System.DateTime.Now;
                            _daySchedule.ModifiedTerminal = obj.TerminalID;

                        };

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0012", Message = string.Format(_localizer[name: "S0012"]) };

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

        public async Task<DO_ReturnParameter> ActiveOrDeActiveDoctordaySchedule(DO_DoctorDaySchedule objdel)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdos2 _dayschedule = db.GtEsdos2s.Where(x => x.BusinessKey == objdel.BusinessKey && x.ConsultationId == objdel.ConsultationId && x.ClinicId == objdel.ClinicId && x.SpecialtyId == objdel.SpecialtyId && x.DoctorId == objdel.DoctorId && x.SerialNo == objdel.SerialNo && x.ScheduleDate.Date == objdel.ScheduleDate.Date).FirstOrDefault();
                        if (_dayschedule == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0150", Message = string.Format(_localizer[name: "W0150"]) };
                        }

                        _dayschedule.ActiveStatus = objdel.status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (objdel.status == true)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0013", Message = string.Format(_localizer[name: "S0013"]) };

                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0014", Message = string.Format(_localizer[name: "S0014"]) };

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
