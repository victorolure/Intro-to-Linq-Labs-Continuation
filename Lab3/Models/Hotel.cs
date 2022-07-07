
namespace Lab3.Models
{
    
    public static class Hotel
    {
        public static ICollection<Client> Clients { get; set; } = new List<Client>();
        public static ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public static ICollection<Room> Rooms { get; set; } = new List<Room>();

        public static Room CreateRoom(int capacity)
        {
            Room newroom = new Room(capacity);
            newroom.Number = Rooms.Count + 1;
            Rooms.Add(newroom);
            return newroom;
        }

        public static Client RegisterClient(string name, long creditcard)
        {
            Client newClient = new Client(name, creditcard);
            newClient.Id = Clients.Count + 1;
            Clients.Add(newClient);
            return newClient;

        }

        public static Client GetClient(int clientId)
        {
            Client searchedClient = Hotel.Clients.FirstOrDefault(client => client.Id == clientId);
            return searchedClient;
        }

        public static Reservation ReserveRoom(int occupants, int clientId, Room room, DateTime startdate)
        {
            Client client = Hotel.GetClient(clientId);
            if (client != null)
            {
                if (client.Reservations.FirstOrDefault(r => r.StartDate.Date == startdate.Date) == null)
                {
                    if (occupants <= room.Capacity && room.Occupied == false)
                    {
                        Reservation newReservation = new Reservation(occupants, client, room, startdate);
                        newReservation.Id = Reservations.Count + 1;
                        Reservations.Add(newReservation);
                        room.Reservations.Add(newReservation);
                        client.Reservations.Add(newReservation);
                        return newReservation;
                    }
                    else
                    {
                        throw new Exception("Error: No room available with this capacity");
                    }
                }
                else
                {
                    throw new Exception($"Error: Sorry {client.Name}, you cannot make 2 reservations for same start date");
                }


            }
            else
            {
                throw new Exception("Error: No Registered Client with this Id, Please get registered");
            }
        }

        public static void Checkin(string clientName)
        {
            Client client = Clients.FirstOrDefault(c => c.Name.ToUpper() == clientName.ToUpper());
            Reservation reservationToUse;
            if (client != null)
            {
                reservationToUse = client.Reservations.FirstOrDefault(r => r.StartDate.Date == DateTime.Now.Date);
                if (reservationToUse != null)
                {
                    reservationToUse.Current = true;
                    reservationToUse.Room.Occupied = true;
                }
            }
        }

        public static void CheckoutRoom(string clientName)
        {
            Client client = Clients.FirstOrDefault(c => c.Name.ToUpper() == clientName.ToUpper());
            Reservation reservationToCheckOut;
            if (client != null)
            {
                reservationToCheckOut = client.Reservations.FirstOrDefault(r => r.Current == true);
                if (reservationToCheckOut != null)
                {
                    reservationToCheckOut.Current = false;
                    reservationToCheckOut.Room.Occupied = false;
                }
               
            }
        }

        public static int TotalHotelCapacity()
        {
            int totalCapacity = 0;
            foreach (Room r in Rooms)
            {
                totalCapacity += r.Capacity;
            }
            return totalCapacity;
        }

        public static int GetTotalOccupants(Room room)//helper function for TotalCapacityRemaining();
        {
            int totalOccupants = 0;
            foreach (Reservation r in room.Reservations)
            {
                if (r.Current == true)
                {
                    totalOccupants += r.Occupants;
                }
            }
            return totalOccupants;
        }
        public static int TotalCapacityRemaining()
        {
            int totalCapacity = 0;
            foreach (Room r in Rooms)
            {
                totalCapacity += r.Capacity - GetTotalOccupants(r);
            }
            return totalCapacity;
        }

       



        static Hotel()
        {
            Room room1 = CreateRoom(2);
            Room room2 = CreateRoom(2);
            Room room3 = CreateRoom(2);
            Room room4 = CreateRoom(4);
            Room room5 = CreateRoom(4);
            Room room6 = CreateRoom(4);
            Room room7 = CreateRoom(4);

            Client Jamie = RegisterClient("Jamie", 1111222233334444);
            Client Ramsey = RegisterClient("Ramsey", 1111222233334445);
            Client Tyrion = RegisterClient("Tyrion", 1111222233334446);
            Client Ronnie = RegisterClient("Ronnie", 1111222233334447);
            Client Gareth = RegisterClient("Gareth", 1111222233334448);
            Client Memphis = RegisterClient("Memphis", 1111222233334449);
            Client Claude = RegisterClient("Claude", 1111222233334450);

            Reservation reservation1 = ReserveRoom(2, 1, room1, DateTime.Now); //I set these dates to the present date to always validate the Checkin Below and occupy rooms.
            Reservation reservation2 = ReserveRoom(2, 5, room2, DateTime.Now);
            Reservation reservation3 = ReserveRoom(3, 3, room6, new DateTime(2022,7,7));

            Checkin("Jamie");
            Checkin("Gareth");

            CheckoutRoom("Gareth"); //if this is uncommented, room number 2 becomes unoccupied.

        }
    }
}
