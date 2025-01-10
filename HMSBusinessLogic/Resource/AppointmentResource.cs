﻿using static HMSContracts.Constants.SysEnums;

namespace HMSBusinessLogic.Resource
{
    public class AppointmentResource
    {
        public DateOnly Date { get; set; }

        public TimeOnly SartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string ReasonOfVisit { get; set; }

        public Status Status { get; set; }

        public string DoctorId { get; set; }

        public string PatientId { get; set; }
    }
}
