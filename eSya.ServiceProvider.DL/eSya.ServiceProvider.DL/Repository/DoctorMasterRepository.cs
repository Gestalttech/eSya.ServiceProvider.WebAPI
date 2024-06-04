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
using System.Xml.Linq;

namespace eSya.ServiceProvider.DL.Repository
{
    public class DoctorMasterRepository: IDoctorMasterRepository
    {
        private readonly IStringLocalizer<DoctorMasterRepository> _localizer;
        public DoctorMasterRepository(IStringLocalizer<DoctorMasterRepository> localizer)
        {
            _localizer = localizer;
        }
        #region Doctor Master
        public async Task<DO_ReturnParameter> InsertIntoDoctorMaster(DO_DoctorMaster obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        var isDoctorExist = db.GtEsdocds.Where(x => x.DoctorShortName.ToUpper().Trim().Replace(" ", "") == obj.DoctorShortName.ToUpper().Trim().Replace(" ", "")).Count();

                        if (isDoctorExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0132", Message = string.Format(_localizer[name: "W0132"]) };
                        }
                        var isregtnoExist = db.GtEsdocds.Where(x => x.DoctorRegnNo.ToUpper().Trim().Replace(" ", "") == obj.DoctorRegnNo.ToUpper().Trim().Replace(" ", "")).Count();
                        if (isregtnoExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0133", Message = string.Format(_localizer[name: "W0133"]) };

                        }
                        if (!string.IsNullOrEmpty(obj.MobileNumber) && obj.MobileNumber != "0")
                        {
                            var isMobileNoExist = db.GtEsdocds.Where(x => x.MobileNumber.ToUpper().Trim().Replace(" ", "") == obj.MobileNumber.ToUpper().Trim().Replace(" ", "") && x.ActiveStatus).Count();
                            if (isMobileNoExist > 0)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0134", Message = string.Format(_localizer[name: "W0134"]) };

                            }
                        }
                        if (!string.IsNullOrEmpty(obj.EMailId))
                        {
                            var isEmailIdExist = db.GtEsdocds.Where(x => x.EmailId.ToUpper().Trim().Replace(" ", "") == obj.EMailId.ToUpper().Trim().Replace(" ", "") && x.ActiveStatus).Count();
                            if (isEmailIdExist > 0)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0135", Message = string.Format(_localizer[name: "W0135"]) };

                            }
                        }
                        int maxDoctorId = db.GtEsdocds.Select(d => d.DoctorId).DefaultIfEmpty().Max();
                        int DocId = maxDoctorId + 1;
                        var dMaster = new GtEsdocd
                        {
                            DoctorId = DocId,
                            DoctorName = obj.DoctorName,
                            DoctorShortName = obj.DoctorShortName,
                            Gender = obj.Gender,
                            DoctorRegnNo = obj.DoctorRegnNo,
                            Isdcode = obj.ISDCode,
                            MobileNumber = obj.MobileNumber,
                            EmailId = obj.EMailId,
                            DoctorClass = obj.DoctorClass,
                            DoctorCategory = obj.DoctorCategory,
                            TraiffFrom = obj.TraiffFrom,
                            //Password = obj.Password,
                            SeniorityLevel = obj.SeniorityLevel,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID,

                        };
                        db.GtEsdocds.Add(dMaster);
                        //await db.SaveChangesAsync();

                        foreach (DO_DoctorParameter dp in obj.l_DoctorParameter)
                        {
                            GtEcpadr doc_Par = db.GtEcpadrs.Where(x => x.DoctorCode == DocId && x.ParameterId == dp.ParameterID).FirstOrDefault();
                            if (doc_Par != null)
                            {
                                doc_Par.ParmAction = dp.ParmAction;
                                doc_Par.ParmDesc = dp.ParmDesc;
                                doc_Par.ParmPerc = dp.ParmPerc;
                                doc_Par.ParmValue = dp.ParmValue;
                                doc_Par.ModifiedBy = obj.UserID;
                                doc_Par.ModifiedOn = System.DateTime.Now;
                                doc_Par.ModifiedTerminal = obj.TerminalID;
                            }
                            else
                            {
                                var d_param = new GtEcpadr
                                {
                                    DoctorCode = DocId,
                                    ParameterId = dp.ParameterID,
                                    ParmPerc = dp.ParmPerc,
                                    ParmAction = dp.ParmAction,
                                    ParmDesc = dp.ParmDesc,
                                    ParmValue = dp.ParmValue,
                                    ActiveStatus = dp.ActiveStatus,
                                    FormId = obj.FormID,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = obj.TerminalID,

                                };
                                db.GtEcpadrs.Add(d_param);
                            }
                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]), ID = DocId };

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

        public async Task<DO_ReturnParameter> UpdateDoctorMaster(DO_DoctorMaster obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdocd dc_ms = db.GtEsdocds.Where(w => w.DoctorId == obj.DoctorId).FirstOrDefault();
                        if (dc_ms == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0136", Message = string.Format(_localizer[name: "W0136"]) };

                        }
                        var _shortnameExist = db.GtEsdocds.Where(x => x.DoctorShortName.ToUpper().Trim().Replace(" ", "") == obj.DoctorShortName.ToUpper().Trim().Replace(" ", "") && x.DoctorId != obj.DoctorId).Count();
                        if (_shortnameExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0132", Message = string.Format(_localizer[name: "W0132"]) };
                        }
                        var _isregtnoExist = db.GtEsdocds.Where(x => x.DoctorRegnNo.ToUpper().Trim().Replace(" ", "") == obj.DoctorRegnNo.ToUpper().Trim().Replace(" ", "") && x.DoctorId != obj.DoctorId).Count();
                        if (_isregtnoExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0133", Message = string.Format(_localizer[name: "W0133"]) };
                        }
                        if (!string.IsNullOrEmpty(obj.MobileNumber) && obj.MobileNumber != "0")
                        {
                            var isMobileNoExist = db.GtEsdocds.Where(x => x.MobileNumber.ToUpper().Trim().Replace(" ", "") == obj.MobileNumber.ToUpper().Trim().Replace(" ", "") && x.DoctorId != obj.DoctorId && x.ActiveStatus).Count();
                            if (isMobileNoExist > 0)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0134", Message = string.Format(_localizer[name: "W0134"]) };
                            }
                        }
                        if (!string.IsNullOrEmpty(obj.EMailId))
                        {
                            var isEmailIdExist = db.GtEsdocds.Where(x => x.EmailId.ToUpper().Trim().Replace(" ", "") == obj.EMailId.ToUpper().Trim().Replace(" ", "") && x.DoctorId != obj.DoctorId && x.ActiveStatus).Count();
                            if (isEmailIdExist > 0)
                            {
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0135", Message = string.Format(_localizer[name: "W0135"]) };
                            }
                        }

                        dc_ms.DoctorName = obj.DoctorName.Trim();
                        dc_ms.DoctorShortName = obj.DoctorShortName;
                        dc_ms.Gender = obj.Gender;
                        dc_ms.DoctorRegnNo = obj.DoctorRegnNo;
                        dc_ms.Isdcode = obj.ISDCode;
                        dc_ms.MobileNumber = obj.MobileNumber;
                        dc_ms.EmailId = obj.EMailId;
                        dc_ms.DoctorClass = obj.DoctorClass;
                        dc_ms.DoctorCategory = obj.DoctorCategory;
                        //dc_ms.AllowConsultation = obj.AllowConsultation;
                        //dc_ms.PayoutType = obj.PayoutType;
                        //dc_ms.AllowSms = obj.AllowSMS;
                        dc_ms.TraiffFrom = obj.TraiffFrom;
                        dc_ms.Password = obj.Password;
                        dc_ms.SeniorityLevel = obj.SeniorityLevel;
                        dc_ms.ActiveStatus = obj.ActiveStatus;
                        dc_ms.ModifiedBy = obj.UserID;
                        dc_ms.ModifiedOn = System.DateTime.Now;
                        dc_ms.ModifiedTerminal = obj.TerminalID;

                        await db.SaveChangesAsync();

                        foreach (DO_DoctorParameter dp in obj.l_DoctorParameter)
                        {
                            GtEcpadr doc_Par = db.GtEcpadrs.Where(x => x.DoctorCode == obj.DoctorId && x.ParameterId == dp.ParameterID).FirstOrDefault();
                            if (doc_Par != null)
                            {
                                doc_Par.ParmAction = dp.ParmAction;
                                doc_Par.ParmDesc = dp.ParmDesc;
                                doc_Par.ParmPerc = dp.ParmPerc;
                                doc_Par.ParmValue = dp.ParmValue;
                                doc_Par.ModifiedBy = obj.UserID;
                                doc_Par.ModifiedOn = System.DateTime.Now;
                                doc_Par.ModifiedTerminal = obj.TerminalID;
                            }
                            else
                            {
                                var d_param = new GtEcpadr
                                {
                                    DoctorCode = obj.DoctorId,
                                    ParameterId = dp.ParameterID,
                                    ParmPerc = dp.ParmPerc,
                                    ParmAction = dp.ParmAction,
                                    ParmDesc = dp.ParmDesc,
                                    ParmValue = dp.ParmValue,
                                    ActiveStatus = dp.ActiveStatus,
                                    FormId = obj.FormID,
                                    CreatedBy = obj.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = obj.TerminalID,

                                };
                                db.GtEcpadrs.Add(d_param);
                            }
                        }

                        await db.SaveChangesAsync();

                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]), ID = obj.DoctorId };

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

        public async Task<List<DO_DoctorParameter>> GetDoctorParameterList(int doctorId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var sp_ms = db.GtEcpadrs
                        .Where(w => w.DoctorCode == doctorId && w.ActiveStatus)
                        .AsNoTracking()
                        .Select(x => new DO_DoctorParameter
                        {
                            ParameterID = x.ParameterId,
                            ParmAction = x.ParmAction,
                            ParmDesc = x.ParmDesc,
                            ParmValue = x.ParmValue,
                            ParmPerc = x.ParmPerc

                        }).ToListAsync();

                    return await sp_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<DO_DoctorMaster>> GetDoctorMasterListForPrefix(string doctorNamePrefix)
        {
            if (doctorNamePrefix == "All")
                doctorNamePrefix = "";

            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = db.GtEsdocds
                       .Join(db.GtEcapcds.Where(x => x.CodeType == CodeTypeValue.DoctorClass),
                       d => new { d.DoctorClass },
                       a => new { DoctorClass = a.ApplicationCode },
                       (d, a) => new { d, a })
                       .Join(db.GtEcapcds.Where(x => x.CodeType == CodeTypeValue.DoctorCategory),
                       dd => new { dd.d.DoctorCategory },
                       aa => new { DoctorCategory = aa.ApplicationCode },
                       (dd, aa) => new { dd, aa  })
                        .Join(db.GtEcapcds.Where(x => x.CodeType == CodeTypeValue.Gender),
                        ddd => new { ddd.dd.d.Gender },
                        aaa => new { Gender = aaa.ApplicationCode },
                        (ddd, aaa) => new { ddd, aaa  })
                        .Join(db.GtEcapcds.Where(x => x.CodeType == CodeTypeValue.SeniorityLevel),
                       dddd => new { dddd.ddd.dd.d.SeniorityLevel },
                       aaaa => new { SeniorityLevel = aaaa.ApplicationCode },
                       (dddd, aaaa) => new { dddd, aaaa  })
                        .Join(db.GtEcapcds.Where(x => x.CodeType == CodeTypeValue.TraiffFrom),
                       ddddd => new { ddddd.dddd.ddd.dd.d.TraiffFrom },
                       aaaaa => new { TraiffFrom = aaaaa.ApplicationCode },
                       (ddddd, aaaaa) => new { ddddd, aaaaa })
                       .Where(w => w.ddddd.dddd.ddd.dd.d.DoctorName.ToUpper().StartsWith(doctorNamePrefix.ToUpper()) || doctorNamePrefix == "")
                       .AsNoTracking()
                       .Select(x => new DO_DoctorMaster
                       {
                           DoctorId = x.ddddd.dddd.ddd.dd.d.DoctorId,
                           DoctorName = x.ddddd.dddd.ddd.dd.d.DoctorName,
                           DoctorShortName = x.ddddd.dddd.ddd.dd.d.DoctorShortName,
                           Gender = x.ddddd.dddd.ddd.dd.d.Gender,
                           DoctorRegnNo = x.ddddd.dddd.ddd.dd.d.DoctorRegnNo,
                           ISDCode = x.ddddd.dddd.ddd.dd.d.Isdcode,
                           MobileNumber = x.ddddd.dddd.ddd.dd.d.MobileNumber,
                           EMailId = x.ddddd.dddd.ddd.dd.d.EmailId,
                           DoctorClass = x.ddddd.dddd.ddd.dd.d.DoctorClass,
                           DoctorClassDesc = x.ddddd.dddd.ddd.dd.a != null ? x.ddddd.dddd.ddd.dd.a.CodeDesc : string.Empty,
                           DoctorCategory = x.ddddd.dddd.ddd.dd.d.DoctorCategory,
                           DoctorCategoryDesc = x.ddddd.dddd.ddd.aa != null ? x.ddddd.dddd.ddd.aa.CodeDesc : string.Empty,
                           GenderDesc = x.ddddd.dddd.aaa != null ? x.ddddd.dddd.aaa.CodeDesc : string.Empty,
                           SeniorityLevel = x.ddddd.dddd.ddd.dd.d.SeniorityLevel,
                           SeniorityLevelDesc = x.ddddd.aaaa != null ? x.ddddd.aaaa.CodeDesc : string.Empty,
                           TraiffFrom = x.ddddd.dddd.ddd.dd.d.TraiffFrom,
                           TraiffFromDesc = x.aaaaa != null ? x.aaaaa.CodeDesc : string.Empty,
                           Password = x.ddddd.dddd.ddd.dd.d.Password,
                           ActiveStatus = x.ddddd.dddd.ddd.dd.d.ActiveStatus

                       }).OrderBy(x => x.DoctorName).ToListAsync();

                    return await dc_ms;

                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<DO_DoctorMaster> GetDoctorMaster(int doctorId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = db.GtEsdocds
                       .Join(db.GtEcapcds.Where(x => x.CodeType == CodeTypeValue.DoctorClass),
                       d => new { d.DoctorClass },
                       a => new { DoctorClass = a.ApplicationCode },
                       (d, a) => new { d, a })
                       .Join(db.GtEcapcds.Where(x => x.CodeType == CodeTypeValue.DoctorCategory),
                       dd => new { dd.d.DoctorCategory },
                       aa => new { DoctorCategory = aa.ApplicationCode },
                       (dd, aa) => new { dd, aa })
                        .Join(db.GtEcapcds.Where(x => x.CodeType == CodeTypeValue.Gender),
                        ddd => new { ddd.dd.d.Gender },
                        aaa => new { Gender = aaa.ApplicationCode },
                        (ddd, aaa) => new { ddd, aaa})
                        .Join(db.GtEcapcds.Where(x => x.CodeType == CodeTypeValue.SeniorityLevel),
                       dddd => new { dddd.ddd.dd.d.SeniorityLevel },
                       aaaa => new { SeniorityLevel = aaaa.ApplicationCode },
                       (dddd, aaaa) => new { dddd, aaaa  })
                        .Join(db.GtEcapcds.Where(x => x.CodeType == CodeTypeValue.TraiffFrom),
                       ddddd => new { ddddd.dddd.ddd.dd.d.TraiffFrom },
                       aaaaa => new { TraiffFrom = aaaaa.ApplicationCode },
                       (ddddd, aaaaa) => new { ddddd, aaaaa })
                       .Where(w => w.ddddd.dddd.ddd.dd.d.DoctorId == doctorId)
                       .AsNoTracking()
                       .Select(x => new DO_DoctorMaster
                       {
                           DoctorId = x.ddddd.dddd.ddd.dd.d.DoctorId,
                           DoctorName = x.ddddd.dddd.ddd.dd.d.DoctorName,
                           DoctorShortName = x.ddddd.dddd.ddd.dd.d.DoctorShortName,
                           Gender = x.ddddd.dddd.ddd.dd.d.Gender,
                           DoctorRegnNo = x.ddddd.dddd.ddd.dd.d.DoctorRegnNo,
                           ISDCode = x.ddddd.dddd.ddd.dd.d.Isdcode,
                           MobileNumber = x.ddddd.dddd.ddd.dd.d.MobileNumber,
                           EMailId = x.ddddd.dddd.ddd.dd.d.EmailId,
                           DoctorClass = x.ddddd.dddd.ddd.dd.d.DoctorClass,
                           DoctorClassDesc = x.ddddd.dddd.ddd.dd.a != null ? x.ddddd.dddd.ddd.dd.a.CodeDesc : string.Empty,
                           DoctorCategory = x.ddddd.dddd.ddd.dd.d.DoctorCategory,
                           DoctorCategoryDesc = x.ddddd.dddd.ddd.aa != null ? x.ddddd.dddd.ddd.aa.CodeDesc : string.Empty,
                           GenderDesc = x.ddddd.dddd.aaa != null ? x.ddddd.dddd.aaa.CodeDesc : string.Empty,
                           SeniorityLevel = x.ddddd.dddd.ddd.dd.d.SeniorityLevel,
                           SeniorityLevelDesc = x.ddddd.aaaa != null ? x.ddddd.aaaa.CodeDesc : string.Empty,
                           TraiffFrom = x.ddddd.dddd.ddd.dd.d.TraiffFrom,
                           TraiffFromDesc = x.aaaaa != null ? x.aaaaa.CodeDesc : string.Empty,
                           Password = x.ddddd.dddd.ddd.dd.d.Password,
                           ActiveStatus = x.ddddd.dddd.ddd.dd.d.ActiveStatus

                       }).FirstOrDefaultAsync();

                    return await dc_ms;
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<DO_ReturnParameter> ActiveOrDeActiveDoctor(bool status, int doctorId)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdocd doctor = db.GtEsdocds.Where(x => x.DoctorId == doctorId).FirstOrDefault();
                        if (doctor == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0136", Message = string.Format(_localizer[name: "W0136"]) };
                        }

                        doctor.ActiveStatus = status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (status == true)
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

        #region About Doctor Details
        public async Task<Do_AboutDoctor> GetAboutDoctorbydoctorId(int doctorId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_details = db.GtEsdoabs.Where(x => x.DoctorId == doctorId)
                        .Select(x => new Do_AboutDoctor
                        {
                            DoctorId = x.DoctorId,
                            LanguageKnown = x.LanguageKnown,
                            Experience = x.Experience,
                            DoctorRemarks = x.DoctorRemarks,
                            CertificationCourse = x.CertificationCourse,
                            AboutDoctor = x.AboutDoctor,
                            ActiveStatus = x.ActiveStatus
                        })
                        .FirstOrDefaultAsync();

                    return await dc_details;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public async Task<DO_ReturnParameter> InsertOrUpdateIntoAboutDoctor(Do_AboutDoctor obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var dc_details = db.GtEsdoabs.Where(x => x.DoctorId == obj.DoctorId).FirstOrDefault();
                        if (dc_details == null)
                        {
                            var details = new GtEsdoab
                            {
                                DoctorId = obj.DoctorId,
                                LanguageKnown = obj.LanguageKnown,
                                Experience = obj.Experience,
                                DoctorRemarks = obj.DoctorRemarks,
                                CertificationCourse = obj.CertificationCourse,
                                AboutDoctor = obj.AboutDoctor,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormId,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID,

                            };
                            db.GtEsdoabs.Add(details);
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]), ID = obj.DoctorId };
                        }

                        else
                        {
                            dc_details.DoctorId = obj.DoctorId;
                            dc_details.LanguageKnown = obj.LanguageKnown;
                            dc_details.Experience = obj.Experience;
                            dc_details.DoctorRemarks = obj.DoctorRemarks;
                            dc_details.CertificationCourse = obj.CertificationCourse;
                            dc_details.AboutDoctor = obj.AboutDoctor;
                            dc_details.ActiveStatus = obj.ActiveStatus;
                            dc_details.ModifiedBy = obj.UserID;
                            dc_details.ModifiedOn = System.DateTime.Now;
                            dc_details.ModifiedTerminal = obj.TerminalID;
                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]), ID = obj.DoctorId };
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
        #endregion Doctor Details

        #region Doctor Profile Image
        public async Task<DO_ReturnParameter> InsertIntoDoctorProfileImage(DO_DoctorImage obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (obj.DoctorProfileImage == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0137", Message = string.Format(_localizer[name: "W0137"]) };

                        }
                        if (obj.DoctorSignatureImage == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0138", Message = string.Format(_localizer[name: "W0138"]) };

                        }
                        var doc_image = db.GtEsdoims.Where(x => x.DoctorId == obj.DoctorId).FirstOrDefault();
                        if (doc_image != null)
                        {
                            doc_image.DoctorImage = obj.DoctorProfileImage;
                            doc_image.DoctorSignature = obj.DoctorSignatureImage;
                            doc_image.ActiveStatus = true;
                            doc_image.ModifiedBy = obj.UserID;
                            doc_image.ModifiedOn = System.DateTime.Now;
                            doc_image.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();

                        }
                        else
                        {
                            var dimage = new GtEsdoim
                            {
                                DoctorId = obj.DoctorId,
                                DoctorImage = obj.DoctorProfileImage,
                                DoctorSignature = obj.DoctorSignatureImage,
                                ActiveStatus = true,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtEsdoims.Add(dimage);
                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0006", Message = string.Format(_localizer[name: "S0006"]), ID = obj.DoctorId };

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
        public async Task<DO_DoctorImage> GetDoctorProfileImagebyDoctorId(int doctorId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = await db.GtEsdoims.Where(x => x.DoctorId == doctorId)
                          .Select(i => new DO_DoctorImage
                          {
                              DoctorId = i.DoctorId,
                              DoctorProfileImage = i.DoctorImage,
                              DoctorSignatureImage = i.DoctorSignature,
                              ActiveStatus = i.ActiveStatus
                          }).FirstOrDefaultAsync();

                    return dc_ms;
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Doctor Profile Address
        public async Task<List<DO_DoctorBusinessLink>> GetDoctorLinkWithBusinessLocation(int doctorId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    try
                    {
                        var sp_ms =await db.GtEsdobls
                            .Join(db.GtEcbslns,
                            s => new { s.BusinessKey },
                            b => new { b.BusinessKey },
                            (s, b) => new { s, b })
                            .Where(w => w.s.DoctorId == doctorId && w.s.ActiveStatus)
                            .AsNoTracking()
                            .Select(x => new DO_DoctorBusinessLink
                            {
                                BusinessKey = x.s.BusinessKey,
                                BusinessLocation = x.b.BusinessName + "-" + x.b.BusinessName,
                                IsdCode = x.b.Isdcode
                            }).OrderBy(x => x.BusinessLocation).ToListAsync();
                          
                        return  sp_ms;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<DO_DoctorProfileAddress>> GetStatesbyIsdCode(int Isdcode)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = db.GtAddrsts
                        .Where(w => w.Isdcode == Isdcode && w.ActiveStatus)
                        .AsNoTracking()
                        .Select(x => new DO_DoctorProfileAddress
                        {
                            StateCode = x.StateCode,
                            StateDesc = x.StateDesc

                        }).OrderBy(x => x.StateDesc).ToListAsync();

                    return await dc_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<DO_DoctorProfileAddress>> GetCitiesbyStateCode(int Isdcode, int statecode)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = db.GtAddrcts
                        .Where(w => w.Isdcode == Isdcode && w.StateCode == statecode && w.ActiveStatus)
                        .AsNoTracking()
                        .Select(x => new DO_DoctorProfileAddress
                        {
                            CityCode = x.CityCode,
                            CityDesc = x.CityDesc

                        }).OrderBy(x => x.CityDesc).ToListAsync();

                    return await dc_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<DO_DoctorProfileAddress>> GetZipDescriptionbyCityCode(int Isdcode, int statecode, int citycode)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = db.GtAddrhds
                        .Where(w => w.Isdcode == Isdcode && w.StateCode == statecode && w.CityCode == citycode && w.ActiveStatus)
                        .AsNoTracking()
                        .Select(x => new DO_DoctorProfileAddress
                        {
                            Zipcode = x.Zipcode,
                            ZipDesc = x.Zipdesc,

                        }).OrderBy(x => x.ZipDesc).ToListAsync();

                    return await dc_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<DO_DoctorProfileAddress>> GetZipCodeAndArea(int Isdcode, string zipcode)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = db.GtAddrdts
                        .Where(w => w.Isdcode == Isdcode && w.Zipcode.ToUpper().Trim().Replace(" ", "") == zipcode.ToUpper().Trim().Replace(" ", "") && w.ActiveStatus)
                        .AsNoTracking()
                        .Select(x => new DO_DoctorProfileAddress
                        {
                            ZipserialNumber = x.ZipserialNumber,
                            Area = x.Area,

                        }).OrderBy(x => x.Area).ToListAsync();

                    return await dc_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<DO_DoctorProfileAddress> FillCoumbosbyZipCode(int Isdcode, string zipcode)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = db.GtAddrhds
                        .Where(w => w.Isdcode == Isdcode && w.Zipcode.ToUpper().Trim().Replace(" ", "") == zipcode.ToUpper().Trim().Replace(" ", "") && w.ActiveStatus)
                        .AsNoTracking()
                        .Select(x => new DO_DoctorProfileAddress
                        {
                            Isdcode = x.Isdcode,
                            StateCode = x.StateCode,
                            CityCode = x.CityCode,
                            Zipcode = x.Zipcode,
                            ZipDesc = x.Zipdesc

                            //ZipserialNumber = x.ZipserialNumber,
                            //Area = x.Area
                        }).FirstOrDefaultAsync();

                    return await dc_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<DO_DoctorProfileAddress> GetDoctorAddressDoctorId(int doctorId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    //var dc_ms = db.GtEsdoad
                    //    .Where(w => w.DoctorId == doctorId && w.Isdcode==Isdcode && w.BusinessKey==businesskey)
                    //    .AsNoTracking()
                    var dc_ms = db.GtEsdoads
                        .Where(w => w.DoctorId == doctorId)
                        .AsNoTracking()
                        .Select(x => new DO_DoctorProfileAddress
                        {
                            BusinessKey = x.BusinessKey,
                            DoctorId = x.DoctorId,
                            Isdcode = x.Isdcode,
                            StateCode = x.StateCode,
                            CityCode = x.CityCode,
                            Zipcode = x.Zipcode,
                            ZipserialNumber = x.ZipserialNumber,
                            ZipDesc = x.Zipcode,
                            //Area=x.Area,
                            Address = x.Address,
                            //Pobox=x.Pobox,
                            ActiveStatus = x.ActiveStatus
                        }).FirstOrDefaultAsync();

                    return await dc_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<DO_ReturnParameter> InsertOrUpdateIntoDoctorProfileAddress(DO_DoctorProfileAddress obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var dc_address = db.GtEsdoads.Where(x => x.DoctorId == obj.DoctorId && x.Isdcode == obj.Isdcode && x.BusinessKey == obj.BusinessKey).FirstOrDefault();
                        if (dc_address == null)
                        {
                            var _address = new GtEsdoad
                            {
                                BusinessKey = obj.BusinessKey,
                                DoctorId = obj.DoctorId,
                                Isdcode = obj.Isdcode,
                                StateCode = obj.StateCode,
                                CityCode = obj.CityCode,
                                Zipcode = obj.Zipcode,
                                ZipserialNumber = obj.ZipserialNumber,
                                //Area = obj.Area,
                                Address = obj.Address,
                                //Pobox = obj.Pobox,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormId,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID,

                            };
                            db.GtEsdoads.Add(_address);
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                        }

                        else
                        {
                            dc_address.StateCode = obj.StateCode;
                            dc_address.CityCode = obj.CityCode;
                            dc_address.Zipcode = obj.Zipcode;
                            dc_address.ZipserialNumber = obj.ZipserialNumber;
                            //dc_address.Area = obj.Area;
                            dc_address.Address = obj.Address;
                            //dc_address.Pobox = obj.Pobox;
                            dc_address.ActiveStatus = obj.ActiveStatus;
                            dc_address.ModifiedBy = obj.UserID;
                            dc_address.ModifiedOn = System.DateTime.Now;
                            dc_address.ModifiedTerminal = obj.TerminalID;
                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
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

        #region Doctor Statutory Details
        public async Task<List<DO_DoctorStatutoryDetails>> GetDoctorStatutoryDetails(int doctorId, int isdCode)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    
                    return await db.GtEccnsds.Where(x => x.Isdcode == isdCode && x.ActiveStatus).
                        Join(db.GtEcsupas.Where(x=>x.Isdcode==isdCode && x.Action && x.ParameterId==6),
                        x=> new { x.StatutoryCode },
                        y=> new { y.StatutoryCode },
                        (x,y)=>new {x,y})
                       .GroupJoin(db.GtEsdosds.Where(x => x.DoctorId == doctorId),
                       m => m.x.StatutoryCode,
                       l => l.StatutoryCode,
                       (m, l) => new
                       { m, l }).SelectMany(z => z.l.DefaultIfEmpty(),
                       (a, b) => new DO_DoctorStatutoryDetails
                       {
                           Isdcode = a.m.x.Isdcode,
                           StatutoryCode = a.m.x.StatutoryCode,
                           StatutoryDescription = a.m.x.StatutoryDescription,
                           StatutoryValue = b != null ? b.StatutoryDescription : "",
                           TaxPerc = b != null ? b.TaxPerc : 0,
                           EffectiveFrom = b != null ? b.EffectiveFrom : DateTime.Now,
                           EffectiveTill = b != null ? b.EffectiveTill : null,
                           ActiveStatus = b != null ? b.ActiveStatus : false
                       }).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertOrUpdateDoctorStatutoryDetails(List<DO_DoctorStatutoryDetails> obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var is_StatutoryDetailEnter = obj.Where(w => !String.IsNullOrEmpty(w.StatutoryValue)).Count();
                        if (is_StatutoryDetailEnter <= 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0139", Message = string.Format(_localizer[name: "W0139"]) };
                        }

                        foreach (var sd in obj.Where(w => !String.IsNullOrEmpty(w.StatutoryValue)))
                        {
                            GtEsdosd cs_sd = db.GtEsdosds.Where(x => x.Isdcode == sd.Isdcode
                                            && x.StatutoryCode == sd.StatutoryCode && x.DoctorId == sd.DoctorId && x.EffectiveFrom.Date==sd.EffectiveFrom.Date).FirstOrDefault();
                            if (cs_sd == null)
                            {
                                var o_cssd = new GtEsdosd
                                {
                                    DoctorId = sd.DoctorId,
                                    Isdcode = sd.Isdcode,
                                    StatutoryCode = sd.StatutoryCode,
                                    StatutoryDescription = sd.StatutoryValue,
                                    TaxPerc = sd.TaxPerc,
                                    EffectiveFrom = sd.EffectiveFrom,
                                    EffectiveTill = sd.EffectiveTill,
                                    ActiveStatus = sd.ActiveStatus,
                                    FormId = sd.FormID,
                                    CreatedBy = sd.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = sd.TerminalID
                                };
                                db.GtEsdosds.Add(o_cssd);
                            }
                            else
                            {
                                cs_sd.StatutoryDescription = sd.StatutoryValue;
                                cs_sd.TaxPerc = sd.TaxPerc;
                                cs_sd.EffectiveTill = sd.EffectiveTill;
                                cs_sd.ActiveStatus = sd.ActiveStatus;
                                cs_sd.ModifiedBy = sd.UserID;
                                cs_sd.ModifiedOn = System.DateTime.Now;
                                cs_sd.ModifiedTerminal = sd.TerminalID;
                            }
                            await db.SaveChangesAsync();
                        }

                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
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

        public async Task<List<DO_CountryISDCodes>> GetISDCodesbyBusinessKey(int businessKey)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var do_cl = db.GtEcbslns
                        .Join(db.GtEccncds.Where(w => w.ActiveStatus),
                        lc => new { lc.Isdcode },
                        o => new { o.Isdcode },
                        (lc, o) => new { lc, o })
                        .Where(w => w.lc.BusinessKey == businessKey && w.lc.ActiveStatus)
                       .AsNoTracking()
                       .Select(r => new DO_CountryISDCodes
                       {
                           Isdcode = r.lc.Isdcode,
                           CountryName = r.o.CountryName,
                           CountryFlag = r.o.CountryFlag,
                           MobileNumberPattern = r.o.MobileNumberPattern,
                           CountryCode = r.o.CountryCode,
                       }).ToListAsync();

                    return await do_cl;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<DO_CountryISDCodes> GetISDCodesbyDoctorId(int doctorId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = db.GtEsdobls
                        .Join(db.GtEcbslns,
                        d => new { d.BusinessKey },
                        a => new { a.BusinessKey },
                        (d, a) => new { d, a })
                        .Join(db.GtEccncds,
                        dd => new { dd.a.Isdcode },
                        aa => new { aa.Isdcode },
                        (dd, aa) => new { dd, aa })
                        .Where(w => w.dd.d.DoctorId == doctorId && w.dd.d.ActiveStatus == true && w.aa.ActiveStatus == true
                            && w.dd.a.ActiveStatus == true)
                        .AsNoTracking()
                        .Select(x => new DO_CountryISDCodes
                        {
                            Isdcode = x.dd.a.Isdcode,
                            CountryFlag = x.aa.CountryFlag,
                            CountryCode = x.aa.CountryCode,
                            CountryName = x.aa.CountryName,
                            MobileNumberPattern = x.aa.MobileNumberPattern,

                        }).ToList();
                    if (dc_ms.Count > 0)
                    {
                        var res = dc_ms.GroupBy(x => x.Isdcode).Select(y => y.First()).Distinct();
                        return res.ToList();
                    }
                    else
                    {
                        return dc_ms.ToList();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        #endregion

        #region Doctor Profile Business Link 
        public async Task<List<DO_DoctorBusinessLink>> GetDoctorBusinessLinkList(int doctorId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = db.GtEcbslns.Where(x=>x.ActiveStatus)
                        .GroupJoin(db.GtEsdobls.Where(x => x.DoctorId == doctorId),
                        d => new { d.BusinessKey },
                        l => new { l.BusinessKey },
                        (d, l) => new { d, l })
                        .SelectMany(z => z.l.DefaultIfEmpty(),
                        (a, b) => new DO_DoctorBusinessLink
                        {
                            BusinessKey = a.d.BusinessKey,
                            BusinessLocation = a.d.BusinessName + "-" + a.d.LocationDescription,
                            TimeSlotInMins = b!= null ? b.TimeSlotInMins : 0,
                            PatientCountPerHour = b != null ? b.PatientCountPerHour : 0,
                            ActiveStatus = b != null ? b.ActiveStatus : false

                        }).OrderBy(x => x.BusinessLocation).ToListAsync();

                    return await dc_ms;

                      
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<DO_ReturnParameter> InsertOrUpdateDoctorBusinessLink(List<DO_DoctorBusinessLink> obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        bool dataSaved = false;
                        foreach (DO_DoctorBusinessLink objDM in obj)
                        {
                            GtEsdobl dMaster = db.GtEsdobls.Where(x => x.BusinessKey == objDM.BusinessKey && x.DoctorId == objDM.DoctorId).FirstOrDefault();
                            if (dMaster == null)
                            {

                                dMaster = new GtEsdobl
                                {
                                    BusinessKey = objDM.BusinessKey,
                                    DoctorId = objDM.DoctorId,
                                    TimeSlotInMins = objDM.TimeSlotInMins,
                                    PatientCountPerHour = objDM.PatientCountPerHour,
                                    ActiveStatus = objDM.ActiveStatus,
                                    FormId = objDM.FormID,
                                    CreatedBy = objDM.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = objDM.TerminalID,

                                };
                                db.GtEsdobls.Add(dMaster);
                                dataSaved = true;

                            }
                            else
                            {
                                dMaster.TimeSlotInMins = objDM.TimeSlotInMins;
                                dMaster.PatientCountPerHour = objDM.PatientCountPerHour;
                                dMaster.ActiveStatus = objDM.ActiveStatus;
                                dMaster.ModifiedBy = objDM.UserID;
                                dMaster.ModifiedOn = System.DateTime.Now;
                                dMaster.ModifiedTerminal = objDM.TerminalID;
                                dataSaved = true;
                            }
                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
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

        #region Specialty Doctor Link
        public async Task<List<DO_SpecialtyDoctorLink>> GetSpecialtyListForBusinessKey(int businessKey)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var sp_ms = db.GtEsspbls
                        .Join(db.GtEsspcds,
                        b => new { b.SpecialtyId },
                        s => new { s.SpecialtyId },
                        (b, s) => new { b, s })
                        .Where(w => w.b.BusinessKey == businessKey && w.b.ActiveStatus && w.s.ActiveStatus)
                        .AsNoTracking()
                        .Select(x => new DO_SpecialtyDoctorLink
                        {
                            SpecialtyID = x.b.SpecialtyId,
                            SpecialtyDesc = x.s.SpecialtyDesc,

                        }).OrderBy(x => x.SpecialtyDesc).ToList();
                    var dist = sp_ms.GroupBy(y => y.SpecialtyID, (key, grp) => grp.FirstOrDefault());
                        //.OrderBy(x => x.SpecialtyDesc).ToListAsync();

                    return  sp_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<DO_SpecialtyDoctorLink>> GetSpecialtyListByDoctorId(int doctorId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var do_ms = db.GtEsdosps
                        .Join(db.GtEsspcds,
                        d => new { d.SpecialtyId },
                        s => new { s.SpecialtyId },
                        (d, s) => new { d, s })
                        .Join(db.GtEcbslns,
                        dd => new { dd.d.BusinessKey },
                        b => new { b.BusinessKey },
                        (dd, b) => new { dd, b })
                        .Where(w => w.dd.d.DoctorId == doctorId && w.b.ActiveStatus && w.dd.s.ActiveStatus)
                        .Select(x => new DO_SpecialtyDoctorLink
                        {
                            DoctorID = x.dd.d.DoctorId,
                            SpecialtyID = x.dd.s.SpecialtyId,
                            SpecialtyDesc = x.dd.s.SpecialtyDesc,
                            LocationDesc = x.b.BusinessName,
                            BusinessKey = x.dd.d.BusinessKey,
                            ActiveStatus = x.dd.d.ActiveStatus

                        }).OrderBy(x => x.SpecialtyDesc).ToListAsync();

                    return await do_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<DO_ReturnParameter> InsertDoctorSpecialtyLink(DO_SpecialtyDoctorLink obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdosp spDl = db.GtEsdosps.Where(x => x.BusinessKey == obj.BusinessKey && x.SpecialtyId == obj.SpecialtyID && x.DoctorId == obj.DoctorID).FirstOrDefault();
                        if (spDl != null)
                        {
                            spDl.ActiveStatus = obj.ActiveStatus;
                            spDl.ModifiedBy = obj.UserID;
                            spDl.ModifiedOn = System.DateTime.Now;
                            spDl.ModifiedTerminal = obj.TerminalID;
                        }
                        else if (obj.ActiveStatus)
                        {
                            var sMaster = new GtEsdosp
                            {
                                BusinessKey = obj.BusinessKey,
                                SpecialtyId = obj.SpecialtyID,
                                DoctorId = obj.DoctorID,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormId,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID,

                            };
                            db.GtEsdosps.Add(sMaster);
                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
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

        public async Task<DO_ReturnParameter> UpdateDoctorSpecialtyLink(DO_SpecialtyDoctorLink obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdosp sp_ms = db.GtEsdosps.Where(w => w.SpecialtyId == obj.SpecialtyID && w.BusinessKey == obj.BusinessKey && w.DoctorId == obj.DoctorID).FirstOrDefault();
                        if (sp_ms == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0141", Message = string.Format(_localizer[name: "W0141"]) };
                        }

                        sp_ms.ActiveStatus = obj.ActiveStatus;
                        sp_ms.ModifiedBy = obj.UserID;
                        sp_ms.ModifiedOn = System.DateTime.Now;
                        sp_ms.ModifiedTerminal = obj.TerminalID;

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };

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

        #region Doctor Clinic Link
        public async Task<List<DO_SpecialtyDoctorLink>> GetSpecialtyListByBKeyDoctorId(int businessKey, int doctorId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var do_ms = db.GtEsdosps
                        .Join(db.GtEsspcds,
                        d => new { d.SpecialtyId },
                        c => new { c.SpecialtyId },
                        (d, c) => new { d, c }
                        )
                        .Where(w => w.d.BusinessKey == businessKey && w.d.DoctorId == doctorId && w.d.ActiveStatus && w.c.ActiveStatus)
                        .AsNoTracking()
                        .Select(x => new DO_SpecialtyDoctorLink
                        {
                            DoctorID = x.d.DoctorId,
                            SpecialtyID = x.d.SpecialtyId,
                            SpecialtyDesc = x.c.SpecialtyDesc

                        }).OrderBy(x => x.SpecialtyDesc).ToList();
                    var dist = do_ms.GroupBy(y => y.SpecialtyID, (key, grp) => grp.FirstOrDefault());
                    return dist.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<DO_DoctorClinic>> GetDoctorClinicLinkList(int businessKey, int specialtyId, int doctorId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {

                    // var do_cl = db.GtEsopcls.Where(x=>x.BusinessKey==businessKey && x.ActiveStatus)
                    // .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.Clinic && w.ActiveStatus),
                    //       l => new { l.ClinicId },
                    //       c => new { ClinicId = c.ApplicationCode },
                    //       (l, c) => new { l, c })
                    //    .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.ConsultationType && w.ActiveStatus),
                    //        lc => new { lc.l.ConsultationId },
                    //        o => new { ConsultationId = o.ApplicationCode },
                    //        (lc, o) => new { lc, o })
                    //.GroupJoin(db.GtEsdocls.Where(w => w.BusinessKey == businessKey && w.SpecialtyId == specialtyId && w.DoctorId == doctorId),
                    //          lco => new { lco.lc.l.BusinessKey, lco.lc.l.ClinicId, lco.lc.l.ConsultationId },
                    //          d => new { d.BusinessKey, d.ClinicId, d.ConsultationId },
                    //          (lco, d) => new { lco, d })
                    //.SelectMany(z => z.d.DefaultIfEmpty(),
                    // (a, b) => new DO_DoctorClinic
                    // {
                    //     BusinessKey = b == null ? 0 : b.BusinessKey,
                    //     ClinicId = a.lco.lc.l.ClinicId,
                    //     ClinicDesc = a.lco.lc.c.CodeDesc,
                    //     ConsultationId = a.lco.lc.l.ConsultationId,
                    //     ConsultationDesc = a.lco.o.CodeDesc,
                    //     ActiveStatus = b == null ? false : b.ActiveStatus
                    // }).ToList();
                    // var Distinctclinics = do_cl .GroupBy(x => new { x.ClinicId, x.ConsultationId }).Select(g => g.First()).ToList();
                    // return Distinctclinics.ToList();

                    var do_cl = db.GtEsspcls.Where(x => x.BusinessKey == businessKey && x.SpecialtyId==specialtyId && x.ActiveStatus)
                    .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.Clinic && w.ActiveStatus),
                          l => new { l.ClinicId },
                          c => new { ClinicId = c.ApplicationCode },
                          (l, c) => new { l, c })
                       .Join(db.GtEcapcds.Where(w => w.CodeType == CodeTypeValue.ConsultationType && w.ActiveStatus),
                           lc => new { lc.l.ConsultationId },
                           o => new { ConsultationId = o.ApplicationCode },
                           (lc, o) => new { lc, o })
                   .GroupJoin(db.GtEsdocls.Where(w => w.BusinessKey == businessKey && w.SpecialtyId == specialtyId && w.DoctorId == doctorId),
                             lco => new { lco.lc.l.BusinessKey, lco.lc.l.ClinicId, lco.lc.l.ConsultationId, lco.lc.l.SpecialtyId },
                             d => new { d.BusinessKey, d.ClinicId, d.ConsultationId,d.SpecialtyId },
                             (lco, d) => new { lco, d })
                   .SelectMany(z => z.d.DefaultIfEmpty(),
                    (a, b) => new DO_DoctorClinic
                    {
                        BusinessKey = b == null ? 0 : b.BusinessKey,
                        ClinicId = a.lco.lc.l.ClinicId,
                        ClinicDesc = a.lco.lc.c.CodeDesc,
                        ConsultationId = a.lco.lc.l.ConsultationId,
                        ConsultationDesc = a.lco.o.CodeDesc,
                        ActiveStatus = b == null ? false : b.ActiveStatus
                    }).ToList();
                    var Distinctclinics = do_cl.GroupBy(x => new { x.ClinicId, x.ConsultationId }).Select(g => g.First()).ToList();
                    return Distinctclinics.ToList();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<DO_ReturnParameter> InsertUpdateDoctorClinicLink(List<DO_DoctorClinic> obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (DO_DoctorClinic dc in obj)
                        {
                            var lst = db.GtEsdocls.Where(x => x.BusinessKey == dc.BusinessKey && x.SpecialtyId == dc.SpecialtyId && x.DoctorId == dc.DoctorId
                                                         && x.ClinicId == dc.ClinicId && x.ConsultationId == dc.ConsultationId).ToList();
                            if (lst.Count > 0)
                            {
                                foreach (var i in lst)
                                {
                                    db.GtEsdocls.Remove(i);
                                }
                            }

                            if (dc.ActiveStatus)
                            {
                                var do_cl = new GtEsdocl
                                {
                                    BusinessKey = dc.BusinessKey,
                                    SpecialtyId = dc.SpecialtyId,
                                    DoctorId = dc.DoctorId,
                                    ClinicId = dc.ClinicId,
                                    ConsultationId = dc.ConsultationId,
                                    ActiveStatus = dc.ActiveStatus,
                                    FormId = dc.FormId,
                                    CreatedBy = dc.UserID,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = dc.TerminalID,
                                };
                                db.GtEsdocls.Add(do_cl);
                            }
                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
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


        #endregion Doctor Clinic Link

        #region Doctor Profile Consultation Rates
        //public async Task<List<DO_DoctorClinic>> GetDoctorClinicLinkListbyClinicConsultation(int businessKey, int clinicId, int consultationId)
        //{
        //    using (var db = new eSyaEnterprise())
        //    {
        //        try
        //        {
        //            //var do_cl = db.GtEsdocl
        //            //    .Join(db.GtEcapcd.Where(w => w.CodeType == CodeTypeValue.Clinic),
        //            //    l => new { l.ClinicId },
        //            //    c => new { ClinicId = c.ApplicationCode },
        //            //    (l, c) => new { l, c })
        //            //    .Join(db.GtEcapcd.Where(w => w.CodeType == CodeTypeValue.ConsultationType),
        //            //    lc => new { lc.l.ConsultationId },
        //            //    o => new { ConsultationId = o.ApplicationCode },
        //            //    (lc, o) => new { lc, o })
        //            //    .Join(db.GtEsdocd.Where(w => w.ActiveStatus),
        //            //    lco => new { lco.lc.l.DoctorId },
        //            //    d => new { d.DoctorId },
        //            //    (lco, d) => new { lco, d })
        //            //    .Join(db.GtEsspcd.Where(w => w.ActiveStatus),
        //            //    lcod => new { lcod.lco.lc.l.SpecialtyId },
        //            //    s => new { s.SpecialtyId },
        //            //    (lcod, s) => new { lcod, s })
        //            //    .Where(w=> w.lcod.lco.lc.l.BusinessKey == businessKey && w.lcod.lco.lc.l.ClinicId == clinicId && w.lcod.lco.lc.l.ConsultationId == consultationId && w.lcod.lco.lc.l.ActiveStatus)
        //            //   .AsNoTracking()
        //            //   .Select(r => new DO_DoctorClinic
        //            //   {
        //            //       ClinicId = r.lcod.lco.lc.l.ClinicId,
        //            //       ClinicDesc = r.lcod.lco.lc.c.CodeDesc,
        //            //       ConsultationId = r.lcod.lco.lc.l.ConsultationId,
        //            //       ConsultationDesc = r.lcod.lco.o.CodeDesc,
        //            //       DoctorId=r.lcod.lco.lc.l.DoctorId,
        //            //       DoctorName = r.lcod.d.DoctorName,
        //            //       SpecialtyId = r.lcod.lco.lc.l.SpecialtyId,
        //            //       SpecialtyDesc = r.s.SpecialtyDesc

        //            //   }).ToListAsync();
        //            var do_cl = db.GtEsdocls

        //              .Select(r => new DO_DoctorClinic
        //              {

        //                  ActiveStatus = r.ActiveStatus
        //              }).ToListAsync();
        //            return await do_cl;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}


        //public async Task<List<DO_DoctorProfileConsultationRate>> GetDoctorProfileConsultationRatebyDoctorId(int businessKey, int clinictype, string currencycode, int ratetype, int doctorId)
        //{
        //    try
        //    {
        //        using (eSyaEnterprise db = new eSyaEnterprise())
        //        {
        //            var defaultDate = DateTime.Now.Date;
        //            var result = db.GtEsclsls.Where(w => w.BusinessKey == businessKey && w.ActiveStatus && (clinictype == 0 ? true : w.ClinicId == clinictype))
        //                .Join(db.GtEssrms,
        //                l => l.ServiceId,
        //                s => s.ServiceId,
        //                (l, s) => new { l, s })
        //                .Join(db.GtEcapcds,
        //                ls => ls.l.ClinicId,
        //                c => c.ApplicationCode,
        //                (ls, c) => new { ls, c })
        //                .Join(db.GtEcapcds,
        //                lsc => lsc.ls.l.ConsultationId,
        //                n => n.ApplicationCode,
        //                (lsc, n) => new { lsc, n })
        //                .GroupJoin(db.GtEsdoros.Where(w => w.DoctorId == doctorId && w.BusinessKey == businessKey && (clinictype == 0 ? true : w.ClinicId == clinictype) && w.CurrencyCode == currencycode && w.RateType == ratetype).OrderByDescending(o => o.ActiveStatus),
        //                lscn => lscn.lsc.ls.l.ClinicId,
        //                r => r.ClinicId,
        //                (lscn, r) => new { lscn, r = r.Where(w => w.ConsultationId == lscn.lsc.ls.l.ConsultationId && w.ServiceId == lscn.lsc.ls.l.ServiceId).FirstOrDefault() })
        //                         .Select(x => new DO_DoctorProfileConsultationRate
        //                         {
        //                             ServiceId = x.lscn.lsc.ls.s.ServiceId,
        //                             ClinicId = x.lscn.lsc.c.ApplicationCode,
        //                             ConsultationId = x.lscn.n.ApplicationCode,
        //                             ServiceDesc = x.lscn.lsc.ls.s.ServiceDesc,
        //                             ClinicDesc = x.lscn.lsc.c.CodeDesc,
        //                             ConsultationDesc = x.lscn.n.CodeDesc,
        //                             Tariff = x.r != null ? x.r.Tariff : 0,
        //                             EffectiveDate = x.r != null ? x.r.EffectiveDate : defaultDate,
        //                             EffectiveTill = x.r != null ? x.r.EffectiveTill : null,
        //                             ActiveStatus = x.r != null ? x.r.ActiveStatus : true,
        //                         }
        //                ).ToListAsync();
        //            return await result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<DO_ReturnParameter> AddOrUpdateDoctorProfileConsultationRate(List<DO_DoctorProfileConsultationRate> obj)
        //{
        //    using (eSyaEnterprise db = new eSyaEnterprise())
        //    {
        //        using (var dbContext = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                foreach (var c_visitrate in obj)
        //                {
        //                    var ServiceExist = db.GtEsdoros.Where(w => w.DoctorId == c_visitrate.DoctorId && w.ServiceId == c_visitrate.ServiceId && w.BusinessKey == c_visitrate.BusinessKey && w.ClinicId == c_visitrate.ClinicId && w.ConsultationId == c_visitrate.ConsultationId && w.CurrencyCode == c_visitrate.CurrencyCode && w.RateType == c_visitrate.RateType && w.EffectiveTill == null).FirstOrDefault();
        //                    if (ServiceExist != null)
        //                    {
        //                        if (c_visitrate.EffectiveDate != ServiceExist.EffectiveDate)
        //                        {
        //                            if (c_visitrate.EffectiveDate < ServiceExist.EffectiveDate)
        //                            {
        //                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0142", Message = string.Format(_localizer[name: "W0142"]) };

        //                            }
        //                            ServiceExist.EffectiveTill = c_visitrate.EffectiveDate.AddDays(-1);
        //                            ServiceExist.ModifiedBy = c_visitrate.UserID;
        //                            ServiceExist.ModifiedOn = System.DateTime.Now;
        //                            ServiceExist.ModifiedTerminal = c_visitrate.TerminalID;
        //                            ServiceExist.ActiveStatus = false;

        //                            var clinicvisitrate = new GtEsdoro
        //                            {
        //                                DoctorId = c_visitrate.DoctorId,
        //                                BusinessKey = c_visitrate.BusinessKey,
        //                                ServiceId = c_visitrate.ServiceId,
        //                                ClinicId = c_visitrate.ClinicId,
        //                                ConsultationId = c_visitrate.ConsultationId,
        //                                RateType = c_visitrate.RateType,
        //                                CurrencyCode = c_visitrate.CurrencyCode,
        //                                EffectiveDate = c_visitrate.EffectiveDate,
        //                                Tariff = c_visitrate.Tariff,
        //                                ActiveStatus = c_visitrate.ActiveStatus,
        //                                FormId = c_visitrate.FormId,
        //                                CreatedBy = c_visitrate.UserID,
        //                                CreatedOn = System.DateTime.Now,
        //                                CreatedTerminal = c_visitrate.TerminalID
        //                            };
        //                            db.GtEsdoros.Add(clinicvisitrate);


        //                        }
        //                        else
        //                        {
        //                            ServiceExist.Tariff = c_visitrate.Tariff;
        //                            ServiceExist.ActiveStatus = c_visitrate.ActiveStatus;

        //                            ServiceExist.ModifiedBy = c_visitrate.UserID;
        //                            ServiceExist.ModifiedOn = System.DateTime.Now;
        //                            ServiceExist.ModifiedTerminal = c_visitrate.TerminalID;
        //                        }

        //                    }
        //                    else
        //                    {
        //                        if (c_visitrate.Tariff != 0)
        //                        {
        //                            var clinicvisitrate = new GtEsdoro
        //                            {
        //                                DoctorId = c_visitrate.DoctorId,
        //                                BusinessKey = c_visitrate.BusinessKey,
        //                                ServiceId = c_visitrate.ServiceId,
        //                                ClinicId = c_visitrate.ClinicId,
        //                                ConsultationId = c_visitrate.ConsultationId,
        //                                RateType = c_visitrate.RateType,
        //                                CurrencyCode = c_visitrate.CurrencyCode,
        //                                EffectiveDate = c_visitrate.EffectiveDate,
        //                                Tariff = c_visitrate.Tariff,
        //                                ActiveStatus = c_visitrate.ActiveStatus,
        //                                FormId = c_visitrate.FormId,
        //                                CreatedBy = c_visitrate.UserID,
        //                                CreatedOn = System.DateTime.Now,
        //                                CreatedTerminal = c_visitrate.TerminalID
        //                            };
        //                            db.GtEsdoros.Add(clinicvisitrate);
        //                        }

        //                    }
        //                }
        //                await db.SaveChangesAsync();
        //                dbContext.Commit();
        //                return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };


        //            }
        //            catch (Exception ex)
        //            {
        //                dbContext.Rollback();
        //                return new DO_ReturnParameter() { Status = false, Message = ex.Message };
        //            }
        //        }
        //    }
        //}
        #endregion


    }
}








