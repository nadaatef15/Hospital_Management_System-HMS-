namespace HMSDataAccess.Entity
{
    public class Payment
    {
        public int Id { get; set; } 
        public int PaidAmount {  get; set; }    
        public DateOnly Date {  get; set; }
        public int ReciptNumber {  get; set; }  

        public  int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }
}
