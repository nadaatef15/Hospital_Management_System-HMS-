using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Specialty
{
    public class SpecialtyModel
    {
        [Required]
        public string Name { get; set; }
        public int? Id {  get; set; }
    }
}
