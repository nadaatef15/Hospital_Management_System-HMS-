using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSDataAccess.Entity
{
    public class Diagnoses
    {
        public int Id {  get; set; }    

        public string Name { get; set; }    

       public List<MedicalRecordDiagnoses> medicalRecordDiagnoses = new List<MedicalRecordDiagnoses>();
    }
}
