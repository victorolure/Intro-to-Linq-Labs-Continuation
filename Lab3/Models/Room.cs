namespace Lab3.Models
{
    public class Room
    {
        public int Number { get; set; }
        public int Capacity { get; set; }
        public bool Occupied { get; set; }
        public List<Reservation> Reservations { get; set; }

        public Room()
        {
            Reservations = new List<Reservation>();
        }

        public Room(int capacity)
        {
            Capacity = capacity;
            Occupied = false;
            Reservations = new List<Reservation>();
        }
    }
}
