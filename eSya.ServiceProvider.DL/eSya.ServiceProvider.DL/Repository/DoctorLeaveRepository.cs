﻿using eSya.ServiceProvider.DL.Entities;
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
    public class DoctorLeaveRepository: IDoctorLeaveRepository
    {
        private readonly IStringLocalizer<DoctorLeaveRepository> _localizer;
        public DoctorLeaveRepository(IStringLocalizer<DoctorLeaveRepository> localizer)
        {
            _localizer = localizer;
        }
        #region Doctor Leave
        public async Task<DO_ReturnParameter> InsertIntoDoctorLeave(DO_DoctorLeave obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var isDoctorLeaveExist = db.GtEsdolds.Where(x =>x.BusinessKey==obj.BusinessKey && x.DoctorId == obj.DoctorId && ((x.OnLeaveFrom.Date >= obj.OnLeaveFrom.Date && x.OnLeaveFrom.Date <= obj.OnLeaveTill.Date) || (x.OnLeaveTill.Date >= obj.OnLeaveFrom.Date && x.OnLeaveTill.Date <= obj.OnLeaveTill.Date)) && x.ActiveStatus == true).Count();
                        if (isDoctorLeaveExist > 0)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0130", Message = string.Format(_localizer[name: "W0130"]) };
                        }

                        var dMasterLeave = new GtEsdold
                        {
                            BusinessKey=obj.BusinessKey,
                            DoctorId = obj.DoctorId,
                            OnLeaveFrom = obj.OnLeaveFrom.Date,
                            OnLeaveTill = obj.OnLeaveTill.Date,
                            NoOfDays = obj.NoOfDays,
                            Comments=obj.Comments,
                            ActiveStatus = obj.ActiveStatus,
                            FormId = obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = System.DateTime.Now,
                            CreatedTerminal = obj.TerminalID,

                        };
                        db.GtEsdolds.Add(dMasterLeave);

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

        public async Task<DO_ReturnParameter> UpdateDoctorLeave(DO_DoctorLeave obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdold doctorLeave = db.GtEsdolds.Where(x =>x.BusinessKey==obj.BusinessKey && x.DoctorId == obj.DoctorId && x.OnLeaveFrom.Date == obj.OnLeaveFrom.Date && x.OnLeaveTill==obj.OnLeaveTill).FirstOrDefault();
                        if (doctorLeave == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0131", Message = string.Format(_localizer[name: "W0131"]) };

                        }
                        else
                        {
                            doctorLeave.Comments = obj.Comments;
                            doctorLeave.ActiveStatus = obj.ActiveStatus;
                            doctorLeave.ModifiedBy = obj.UserID;
                            doctorLeave.ModifiedOn = System.DateTime.Now;
                            doctorLeave.ModifiedTerminal = obj.TerminalID;

                        };

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

        public async Task<List<DO_DoctorLeave>> GetDoctorLeaveListAll(int Businesskey,int DoctorID)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var dc_ms = db.GtEsdolds
                        .Where(w => w.BusinessKey == Businesskey && w.DoctorId == DoctorID)
                        //.Where(w =>w.BusinessKey==Businesskey && w.DoctorId == DoctorID && w.OnLeaveTill >= System.DateTime.Now.Date)
                        .AsNoTracking()
                        .Select(x => new DO_DoctorLeave
                        {

                            OnLeaveFrom = x.OnLeaveFrom,
                            OnLeaveTill = x.OnLeaveTill,
                            NoOfDays = x.NoOfDays,
                            Comments=x.Comments,
                            ActiveStatus = x.ActiveStatus

                        }).OrderByDescending(x => x.OnLeaveFrom).ToListAsync();

                    return await dc_ms;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<DO_ReturnParameter> ActivateOrDeActivateDoctorLeave(DO_DoctorLeave obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEsdold doctorLeave = db.GtEsdolds.Where(x => x.BusinessKey == obj.BusinessKey && x.DoctorId == obj.DoctorId && x.OnLeaveFrom.Date == obj.OnLeaveFrom.Date && x.OnLeaveTill == obj.OnLeaveTill).FirstOrDefault();
                        if (doctorLeave == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0131", Message = string.Format(_localizer[name: "W0131"]) };

                        }
                        else
                        {
                           
                            doctorLeave.ActiveStatus = obj._status;
                            doctorLeave.ModifiedBy = obj.UserID;
                            doctorLeave.ModifiedOn = System.DateTime.Now;
                            doctorLeave.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                        }
                      
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
