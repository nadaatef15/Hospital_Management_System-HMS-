namespace HMSContracts.Model.Permission
{
    public class PermissionModel
    {
        public string PermissionType { get; set; } // view , create , edit , delete
        public string Model {  get; set; }  
        public bool isSelected { get; set; }
    }
}
