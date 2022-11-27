using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Classes
{
    public class Person
    {
        public string Name;
        public string Surname { get; }
        public string Email { get; }
        public Dictionary<Guid, Boolean> Presence { get; private set; }

        public Person(string name, string surname, string email, Dictionary<Guid, Boolean> presence)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Presence = presence;
        }

        public void FalsePresence(Guid ev)
        {
            Presence[ev] = false;
        }
        public void RemovePresence(Guid ev)
        {
            Presence.Remove(ev);
        }
        public void AddPresence(Guid ev)
        {
            Presence.Add(ev, true);
        }
    }
}
