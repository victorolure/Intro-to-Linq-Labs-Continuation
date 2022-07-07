namespace Lab3.Models
{
    public class Reservation
    {
        public DateTime Created { get; set; }
        public DateTime StartDate { get; set; }
        public int Id { get; set; }
        public int Occupants { get; set; }
        public bool Current { get; set; }
        public Client Client { get; set; }
        public Room Room { get; set; }


        // CONSTRUCTORS
        public Reservation() { }
        public Reservation(int occupants, Client client, Room room, DateTime startdate)
        {
            Created = DateTime.Now;
            StartDate = startdate;
            Occupants = occupants;
            Current = false;
            Client = client;
            Room = room;
        }
    }
}
