// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

abstract class EmergencyUnit
{
    public string Name { get; set; }
    public int Speed { get; set; }

    public EmergencyUnit(string name, int speed)
    {
        Name = name;
        Speed = speed;
    }

    public abstract bool CanHandle(string incidentType);
    public abstract void RespondToIncident(Incident incident);
}

class Police : EmergencyUnit
{
    public Police(string name, int speed) : base(name, speed) { }

    public override bool CanHandle(string incidentType) => incidentType == "Crime";

    public override void RespondToIncident(Incident incident)
    {
        Console.WriteLine($"{Name} is handling a crime at {incident.Location}.");
    }
}

class Firefighter : EmergencyUnit
{
    public Firefighter(string name, int speed) : base(name, speed) { }

    public override bool CanHandle(string incidentType) => incidentType == "Fire";

    public override void RespondToIncident(Incident incident)
    {
        Console.WriteLine($"{Name} is extinguishing fire at {incident.Location}.");
    }
}

class Ambulance : EmergencyUnit
{
    public Ambulance(string name, int speed) : base(name, speed) { }

    public override bool CanHandle(string incidentType) => incidentType == "Medical";

    public override void RespondToIncident(Incident incident)
    {
        Console.WriteLine($"{Name} is treating patients at {incident.Location}.");
    }
}

class Incident
{
    public string Type { get; set; }
    public string Location { get; set; }

    public Incident(string type, string location)
    {
        Type = type;
        Location = location;
    }
}

class Program
{
    static Random random = new Random();
    static string[] incidentTypes = { "Fire", "Crime", "Medical" };
    static string[] locations = { "City Hall", "Downtown", "Suburbs", "Mall", "Airport" };

    static void Main()
    {
        List<EmergencyUnit> units = new List<EmergencyUnit>
        {
            new Police("Police Unit 1", 100),
            new Firefighter("Firefighter Unit 1", 90),
            new Ambulance("Ambulance Unit 1", 80)
        };

        int score = 0;

        for (int round = 1; round <= 5; round++)
        {
            Console.WriteLine($"\n--- Turn {round} ---");
            string type = incidentTypes[random.Next(incidentTypes.Length)];
            string location = locations[random.Next(locations.Length)];
            Incident incident = new Incident(type, location);
            Console.WriteLine($"Incident: {type} at {location}");

            bool handled = false;

            foreach (var unit in units)
            {
                if (unit.CanHandle(type))
                {
                    unit.RespondToIncident(incident);
                    Console.WriteLine("+10 points");
                    score += 10;
                    handled = true;
                    break;
                }
            }

            if (!handled)
            {
                Console.WriteLine("No unit available to handle the incident.");
                Console.WriteLine("-5 points");
                score -= 5;
            }

            Console.WriteLine($"Current Score: {score}");
        }

        Console.WriteLine($"\nFinal Score: {score}");
    }
}

