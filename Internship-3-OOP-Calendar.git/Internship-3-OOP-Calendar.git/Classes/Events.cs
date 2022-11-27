using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp3
{
    internal class Event
    {

        public Guid Id { get; }//mjenja se samo u konstruktoru (linija ei), priate znaci unutar klase pormjkena npr metodom za specificnu promjenu
        public string Name;
        public string Location;
        public DateTime StartingDate;
        public DateTime EndingDate;
        public List<string> Participants { get; private set; }
        public List<string> TrueParticipants { get; private set; }

        public Event(string name, string location, DateTime startingDate, DateTime endingDate, List<string> participants, List<string> trueParticipants)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = location;
            StartingDate = startingDate;
            EndingDate = endingDate;
            Participants = participants;
            TrueParticipants = trueParticipants;
        }
        public void FalseParticipants(string osoba)
        {
            TrueParticipants.Remove(osoba);
        }
        public void ChangeEmails(List<string> trueParticipant, List<string> participant)
        {
            TrueParticipants = trueParticipant;
            Participants = participant;
        }
        public void RemovePerson(string osoba)
        {
            Participants.Remove(osoba);
            if (TrueParticipants.Contains(osoba))
            {
                TrueParticipants.Remove(osoba);
            }
        }
    }
}