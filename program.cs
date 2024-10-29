using System;
using System.Collections.Generic;

public class Club_Role
{
    public string Name { get; set; }
    public string Role { get; set; }
    public string Contact { get; set; }
}

public class Event
{
    public string EventName { get; set; }
    public DateTime EventDate { get; set; }

    public Event(string eventName, DateTime eventDate)
    {
        EventName = eventName;
        EventDate = eventDate;
    }
}

public class Student_Club
{
    public double Budget { get; set; }
    public Club_Role President { get; set; }
    public Club_Role VicePresident { get; set; }
    public Club_Role GeneralSecretary { get; set; }
    public Club_Role FinanceSecretary { get; set; }

    public List<Society> Societies { get; set; } = new List<Society>();

    public Student_Club(double budget, Club_Role president, Club_Role vicePresident, Club_Role generalSecretary, Club_Role financeSecretary)
    {
        Budget = budget;
        President = president;
        VicePresident = vicePresident;
        GeneralSecretary = generalSecretary;
        FinanceSecretary = financeSecretary;
    }

    public void AddSociety(Society society)
    {
        Societies.Add(society);
        Console.WriteLine($"Society '{society.Name}' with contact '{society.Contact}' has been added.");
    }

    public void DisplayFundingInfo()
    {
        foreach (var society in Societies)
        {
            if (society is Funded_Society fundedSociety)
            {
                Console.WriteLine($"Funded Society: {fundedSociety.Name} - Funding Amount: {fundedSociety.FundingAmount}");
            }
            else
            {
                Console.WriteLine($"Non-Funded Society: {society.Name}");
            }
        }
    }
}

public class Society
{
    public string Name { get; set; }
    public string Contact { get; set; }
    public List<Event> Events { get; set; } = new List<Event>();

    public Society(string name, string contact)
    {
        Name = name;
        Contact = contact;
    }

    public void AddEvent(Event newEvent)
    {
        Events.Add(newEvent);
        Console.WriteLine($"Event '{newEvent.EventName}' scheduled for {newEvent.EventDate.ToShortDateString()} has been added to {Name}.");
    }

    public void DisplayEvents()
    {
        Console.WriteLine($"Events for {Name}:");
        foreach (var ev in Events)
        {
            Console.WriteLine($"- {ev.EventName} on {ev.EventDate.ToShortDateString()}");
        }
    }
}

public class Funded_Society : Society
{
    public double FundingAmount { get; set; }

    public Funded_Society(string name, string contact, double fundingAmount) : base(name, contact)
    {
        FundingAmount = fundingAmount;
    }
}

public class NonFunded_Society : Society
{
    public NonFunded_Society(string name, string contact) : base(name, contact) { }
}

class Program
{
    static void Main()
    {
        Club_Role president = new Club_Role { Name = "Alice", Role = "President", Contact = "alice@example.com" };
        Club_Role vicePresident = new Club_Role { Name = "Bob", Role = "Vice President", Contact = "bob@example.com" };
        Club_Role generalSecretary = new Club_Role { Name = "Charlie", Role = "General Secretary", Contact = "charlie@example.com" };
        Club_Role financeSecretary = new Club_Role { Name = "Diana", Role = "Finance Secretary", Contact = "diana@example.com" };

        Student_Club club = new Student_Club(5000, president, vicePresident, generalSecretary, financeSecretary);

        // Predefined societies
        Funded_Society sportsSociety = new Funded_Society("Sports Society", "sports@example.com", 1000);
        Funded_Society techSociety = new Funded_Society("Tech Society", "tech@example.com", 1000);
        NonFunded_Society literarySociety = new NonFunded_Society("Literary Society", "lit@example.com");

        club.AddSociety(sportsSociety);
        club.AddSociety(techSociety);
        club.AddSociety(literarySociety);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Register a New Event for a Society");
            Console.WriteLine("2. Display Society Funding Information");
            Console.WriteLine("3. Display Events for a Society");
            Console.WriteLine("4. Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    RegisterEvent(club);
                    break;
                case "2":
                    club.DisplayFundingInfo();
                    break;
                case "3":
                    DisplaySocietyEvents(club);
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void RegisterEvent(Student_Club club)
    {
        Console.WriteLine("Enter the society name to add an event (Sports Society, Tech Society, Literary Society):");
        string societyName = Console.ReadLine();

        Society society = club.Societies.Find(s => s.Name.Equals(societyName, StringComparison.OrdinalIgnoreCase));
        if (society != null)
        {
            Console.WriteLine("Enter the event name:");
            string eventName = Console.ReadLine();

            Console.WriteLine("Enter the event date (yyyy-mm-dd):");
            DateTime eventDate;
            while (!DateTime.TryParse(Console.ReadLine(), out eventDate))
            {
                Console.WriteLine("Invalid date format. Please enter in yyyy-mm-dd format:");
            }

            society.AddEvent(new Event(eventName, eventDate));
        }
        else
        {
            Console.WriteLine("Society not found.");
        }
    }

    static void DisplaySocietyEvents(Student_Club club)
    {
        Console.WriteLine("Enter the society name to display events:");
        string societyName = Console.ReadLine();

        Society society = club.Societies.Find(s => s.Name.Equals(societyName, StringComparison.OrdinalIgnoreCase));
        if (society != null)
        {
            society.DisplayEvents();
        }
        else
        {
            Console.WriteLine("Society not found.");
        }
    }
}
