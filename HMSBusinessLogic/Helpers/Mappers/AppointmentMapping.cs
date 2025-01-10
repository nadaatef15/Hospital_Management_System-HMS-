using HMSBusinessLogic.Resource;
using HMSContracts.Model.Appointment;
using HMSDataAccess.Entity;
using static HMSContracts.Constants.SysEnums;

namespace HMSBusinessLogic.Helpers.Mappers
{
    public static class AppointmentMapping
    {
        public static AppointmentEntity ToEntity(this AppointmentModel model) => new()
        {
            Date = model.Date,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            ReasonOfVisit = model.ReasonOfVisit,
            Status = model.Status,
            DoctorId = model.DoctorId,
            PatientId = model.PatientId,
        };

        public static AppointmentResource ToResource(this AppointmentEntity model) => new()
        {
            Date = model.Date,
            SartTime = model.StartTime,
            EndTime = model.EndTime,
            ReasonOfVisit = model.ReasonOfVisit,
            DoctorId = model.DoctorId,
            PatientId = model.PatientId,
            Status = model.Status == 0 ? Status.complete : Status.incomplete,
        };

    }
}
