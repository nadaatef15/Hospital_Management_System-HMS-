namespace HMSDataAccess.Entity
{
    public class InvoiceItem
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public int UnitPrice {  get; set; }
        public int TotalPrice { get; set; } 
        public int Quentity {  get; set; }

        public int InvoiceId {  get; set; } 
        public Invoice Invoice { get; set; }    
        
    }
}
