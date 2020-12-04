using Roommates.Models;
using System;
using Roommates.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository mateRepo = new RoommateRepository(CONNECTION_STRING);
            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach(Chore c in chores)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for chore"):
                        Console.Write("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());
                        Chore chore = choreRepo.GetById(choreId);
                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a chore"):
                        Console.Write("Chore Name: ");
                        string choreName = Console.ReadLine();
                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName
                        };
                        choreRepo.Insert(choreToAdd);
                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for roommate"):
                        Console.Write("Roommate Id: ");
                        int mateId = int.Parse(Console.ReadLine());
                        Roommate roommate = mateRepo.GetById(mateId);
                        Console.WriteLine($"{roommate.Id} - {roommate.Firstname}");
                        Console.WriteLine($"Rent portion: { roommate.RentPortion}");
                        Console.WriteLine($"Room: {roommate.Room.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all unassigned chores"):
                        List<Chore> unassignedChores = choreRepo.GetUnassignedChores();
                        foreach (Chore c in unassignedChores)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Assign chore"):
                        List<Chore> choresToAssign = choreRepo.GetAll();
                        foreach (Chore c in choresToAssign)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.Write("Chore to Assign: ");
                        int choreIdToAssign = int.Parse(Console.ReadLine());
                        List<Roommate> roommatesToAssign = mateRepo.GetAll();
                        foreach (Roommate r in roommatesToAssign)
                        {
                            Console.WriteLine($"{r.Id} - {r.Firstname}");
                        }
                        Console.Write("Roommate to Assign: ");
                        int roommateIdToAssign = int.Parse(Console.ReadLine());
                        choreRepo.AssignChore(roommateIdToAssign, choreIdToAssign);
                        Console.WriteLine($"chore {choreIdToAssign} has been added and assigned to {roommateIdToAssign}");

                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine($"{selectedRoom.Name} has been successfully updated");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Delete a room"):
                        List<Room> roomsToDelete = roomRepo.GetAll();
                        foreach (Room r in roomsToDelete)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }
                        Console.Write("Which room would you like to delete? ");
                        int roomToDeleteId = int.Parse(Console.ReadLine());
                        roomRepo.Delete(roomToDeleteId);
                        Console.WriteLine("Room has been successfully deleted");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Update a chore"):
                        List<Chore> choreOptions = choreRepo.GetAll();
                        foreach(Chore c in choreOptions)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.Write("Which chore would you like to update? ");
                        int selectedChoreId = int.Parse(Console.ReadLine());
                        Chore selectedChore = choreOptions.FirstOrDefault(c => c.Id == selectedChoreId);
                        Console.Write("New Name: ");
                        selectedChore.Name = Console.ReadLine();
                        choreRepo.Update(selectedChore);
                        Console.WriteLine($"{selectedChore.Name} has been successfully updated");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Delete a chore"):
                        List<Chore> choresToDelete = choreRepo.GetAll();
                        foreach (Chore c in choresToDelete)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.Write("Which chore would you like to delete? ");
                        int choreToDeleteId = int.Parse(Console.ReadLine());
                        choreRepo.Delete(choreToDeleteId);
                        Console.WriteLine("Chore has been successfully deleted");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Get chore counts"):
                        List<ChoreCount> choreCounts = choreRepo.GetChoreCounts();
                        foreach(ChoreCount cc in choreCounts)
                        {
                            Console.WriteLine($"{cc.Firstname}: {cc.Count}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
        {
            "Show all rooms",
            "Search for room",
            "Add a room",
            "Show all chores",
            "Search for chore",
            "Add a chore",
            "Search for roommate",
            "Show all unassigned chores",
            "Assign chore",
            "Update a room",
            "Delete a room",
            "Update a chore",
            "Delete a chore",
            "Get chore counts",
            "Exit"
        };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }

        }
    }
}
