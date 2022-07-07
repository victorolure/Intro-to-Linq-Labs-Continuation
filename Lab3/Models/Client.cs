namespace Lab3.Models
{
    public class Client
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public long CreditCard { get; set; }
        public List<Reservation> Reservations { get; set; }

        public Client(string name, long creditCard)
        {
            Name = name;
            CreditCard = creditCard;
            Reservations = new List<Reservation>();
        }
    }
}
