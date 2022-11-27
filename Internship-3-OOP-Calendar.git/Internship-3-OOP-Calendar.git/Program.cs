using ConsoleApp3;
using ConsoleApp3.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;

namespace ConsoleApp3
{
    internal class Program
    {
        static bool Potvrda()
        {
            Console.WriteLine("Želite li spremiti spremit promjene (Da/Ne)");
            var choice = Console.ReadLine();
            if (choice == "Da")
                return true;
            else
                return false;
        }
        static void Main(string[] args)
        {
            var listOfPersons = new List<Person>();
            listOfPersons.Add(new Person("Ana", "Anic", "ana.anic01@anici.al", new Dictionary<Guid, Boolean>()));
            listOfPersons.Add(new Person("Bruno", "Brušić", "bruno.brusic02@brusici.ba", new Dictionary<Guid, Boolean>()));
            listOfPersons.Add(new Person("Cvita", "Cvitić", "cvita.cvitic03@cvitici.ca", new Dictionary<Guid, Boolean>()));
            listOfPersons.Add(new Person("Duje", "Dujić", "duje.dujic04@dujici.dk", new Dictionary<Guid, Boolean>()));
            listOfPersons.Add(new Person("Ema", "Emić", "ema.emic05@emici.eg", new Dictionary<Guid, Boolean>()));
            listOfPersons.Add(new Person("Filip", "Filipić", "filip.filipic06@filipici.fi", new Dictionary<Guid, Boolean>()));
            listOfPersons.Add(new Person("Goran", "Goranić", "goran.goranic017@goranici.ge", new Dictionary<Guid, Boolean>()));
            listOfPersons.Add(new Person("Hrvoje", "Hrvatić", "hrvoje.hrvatis08@hrvatici.hr", new Dictionary<Guid, Boolean>()));
            listOfPersons.Add(new Person("Ivan", "Ivić", "ivan.ivic.09@ivici.il", new Dictionary<Guid, Boolean>()));
            listOfPersons.Add(new Person("Josipa", "Šaravanja", "josipa.saravanja10@gmail.com", new Dictionary<Guid, Boolean>()));


            var listOfEvents = new List<Event>();
            listOfEvents.Add(new Event("Prvi događaj", "Prva lokacija", new DateTime(2020, 01, 01, 01, 01, 01), new DateTime(2020, 01, 02, 02, 02, 02), new List<string> { "ana.anic01@anici.al", "bruno.brusic02@brusici.ba" }, new List<string> { "ana.anic01@anici.al", "bruno.brusic02@brusici.ba" }));
            listOfEvents.Add(new Event("Drugi događaj", "Druga lokacija", new DateTime(2021, 02, 02, 02, 02, 02), new DateTime(2021, 04, 04, 04, 04, 04), new List<string> { "ana.anic01@anici.al", "bruno.brusic02@brusici.ba" }, new List<string> { "ana.anic01@anici.al", "bruno.brusic02@brusici.ba" }));
            listOfEvents.Add(new Event("Treći događaj", "Treća lokacija", new DateTime(2022, 03, 03, 03, 03, 03), new DateTime(2023, 06, 06, 06, 06, 06), new List<string> { "cvita.cvitic03@cvitici.ca", "ema.emić05@emici.eg", "filip.filipić06@filipici.fi", "goran.goranic017@goranici.ge" }, new List<string> { "cvita.cvitic03@cvitici.ca", "ema.emić05@emici.eg", "filip.filipić06@filipici.fi", "goran.goranic017@goranici.ge" }));
            listOfEvents.Add(new Event("Četvrti događaj", "Četvrta lokacija", new DateTime(2023, 04, 04, 04, 04, 04), new DateTime(2023, 08, 08, 08, 08, 08), new List<string> { "Ivan", "Ivić", "ivan.ivic.09@ivici.il" }, new List<string> { "Ivan", "Ivić", "ivan.ivic.09@ivici.il" }));
            listOfEvents.Add(new Event("Peti događaj", "Peta lokacija", new DateTime(2024, 05, 05, 05, 05, 05), new DateTime(2024, 10, 10, 10, 10, 10), new List<string> { "ana.anic01@anici.al", "ivan.ivic.09@ivici.il" }, new List<string> { "ana.anic01@anici.al", "ivan.ivic.09@ivici.il" }));
            

            foreach (var person in listOfPersons) //ubacuje ove prve namještene podatke
            {
                foreach (var Event in listOfEvents)
                {
                    foreach (var email in Event.Participants)
                    {
                        if (person.Email == email) person.Presence.Add(Event.Id, true);
                    }
                }
            }


            var loop = 1;
            do
            {
                Console.WriteLine("1 - Aktivni eventi \n"
                                + "2 - Nadolazeći eventi\n"
                                + "3 - Eventi koji su završili\n"
                                + "4 - Kreiraj event\n"
                                + "0 - Izađi iz programa\n");
                var choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "0":
                        loop = 0; //izlazi iz programa
                        break;
                    case "1":
                        Console.WriteLine("Aktivni event: ");
                        foreach (var el in listOfEvents)
                        {
                            if (el.StartingDate < DateTime.Now && el.EndingDate > DateTime.Now)//Ispisuje sve aktivne evente
                            {
                                Console.WriteLine($"id: {el.Id} \nNaziv: {el.Name} | Lokacija: {el.Location} | Ends in: {Math.Round(el.EndingDate.Subtract(DateTime.Now).TotalHours * 10) / 10}h");
                                Console.WriteLine("Popis sudionika:");
                                foreach (var person in el.Participants)
                                {
                                    Console.Write(person+ ", ");
                                }
                                Console.WriteLine($"\n\n");
                            }
                        }
                        Console.WriteLine("1 - Zabilježi neprisutnosti\n"
                                        + "0 - Povratak na glavni izbornik\n");
                        choice = Console.ReadLine();

                        //inače bih napravila Console.Clear() radi urednosti pri novom upisu ali mislim da je korisniku lakše copy-paste id koji mu se ispisan naviše u konzoli
                        switch (choice)
                        {
                            case "0":
                                break;
                            case "1":
                                Console.WriteLine("Unesite id elementa");
                                var id = Console.ReadLine();
                                var ev = listOfEvents.Find(item => item.Id.ToString() == id);
                                if (ev == null)
                                {
                                    Console.WriteLine("Unjeli ste netočan id");
                                    break;
                                }
                                if (ev.StartingDate > DateTime.Now)
                                {
                                    Console.WriteLine("Ne možete zapisivat prisutnost za buduće evente");
                                    break;
                                }
                                else if (ev.EndingDate < DateTime.Now)
                                {
                                    Console.WriteLine("Ne možete zapisivat prisutnost za završene evente");
                                    break;
                                }

                                Console.WriteLine("Unesite mail osoba zarezom");
                                string[] listOfNotAttending = Console.ReadLine().Split(", ");
                                if (listOfNotAttending.Count() < 1)
                                {
                                    Console.WriteLine("Niste unjeli dovoljno osoba");
                                    break;
                                }
                                bool vazece = true;
                                foreach (var notAttended in listOfNotAttending)
                                {
                                    if (ev.TrueParticipants.Find(el => el == notAttended) == null)
                                    {
                                        Console.WriteLine("Unjeli ste nevažeće ime");
                                        vazece = false;
                                        break;
                                    }
                                }
                                if (vazece == false) break;

                                if (Potvrda() == false)
                                {
                                    break;
                                }
                                foreach (var notAttended in listOfNotAttending)
                                {
                                    ev.FalseParticipants(notAttended);
                                    listOfPersons.Find(el => el.Email.ToString() == notAttended).FalsePresence(ev.Id);
                                }
                                break;
                            default:
                                Console.WriteLine($"{choice} nije valjani unos. Pritisnite enter za povratak na glavni izbornik.");
                                break;
                        }
                        break;
                    case "2":
                        foreach (var el in listOfEvents)
                        {
                            if (el.StartingDate > DateTime.Now)
                            {
                                Console.WriteLine($"id: {el.Id} \nNaziv: {el.Name} | Lokacija: {el.Location} | Počinje za: {Math.Round(el.StartingDate.Subtract(DateTime.Now).TotalDays * 10) / 10} dana | Trajanje {Math.Round(el.EndingDate.Subtract(el.StartingDate).TotalHours * 10) / 10}");
                                Console.WriteLine("Popis sudionika:");
                                foreach (var person in listOfPersons)
                                {
                                    if (person.Presence.ContainsKey(el.Id) && person.Presence[el.Id])
                                    {
                                        Console.Write(person.Email+", ");
                                    }
                                }
                                Console.WriteLine("\n \n");
                            }
                        }

                        Console.WriteLine("1 - Izbriši event \n"
                                         +"2 - Ukloni osobe s eventa\n"
                                         +"0 - Povratak na glavni menu\n");
                        choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "0":
                                break;
                            case "1":
                                Console.WriteLine("Unesite id eventa kojeg želite izbrisati");
                                choice = Console.ReadLine();
                                var ev = listOfEvents.Where(item => item.Id.ToString() == choice);
                                if (ev == null)
                                {
                                    Console.WriteLine("Unjeli ste netočan id.");
                                    break;
                                }
                                if (Potvrda() == false)
                                {
                                    break;
                                }
                                foreach (var eve in listOfEvents)
                                {
                                    if (eve.Id.ToString() == choice)
                                    {
                                        listOfEvents.Remove(eve);
                                        break;
                                    }
                                }
                                break;
                            case "2":

                                Console.WriteLine("Unesite id Eventa s kojeg želite ukloniti određene osobe");
                                var id = Console.ReadLine();
                                ev = listOfEvents.Where(item => item.Id.ToString() == id);
                                if (ev == null)
                                {
                                    Console.WriteLine("Unjeli ste netočan id.");
                                    break;
                                }
                                Console.WriteLine("Unesite osobe koje želite izbrisat ze ih odvojite `, `");
                                string[] listOfPersonsToRemove = Console.ReadLine().Split(", ");

                                var remove = new List<string>();

                                foreach (var person in listOfPersons)
                                {
                                    if (listOfPersonsToRemove.Contains(person.Email) == true)
                                    {
                                        bool rem = true;
                                        foreach (var personsIds in person.Presence)
                                        {

                                            if (id == personsIds.Key.ToString())
                                            {
                                                rem = false;
                                                remove.Add(person.Email);

                                            }
                                        }
                                        if (rem == true)
                                        {
                                            Console.WriteLine($"{person.Name} {person.Surname} nije dio ovog eventa)");
                                        }
                                    }
                                }
                                if (Potvrda() == false)
                                {
                                    break;
                                }
                                foreach (var person in listOfPersons)
                                {
                                    foreach (var email in remove)
                                    {
                                        if (person.Email == email)
                                        {
                                            person.RemovePresence(Guid.Parse(id));
                                            listOfEvents.Find(item => item.Id.ToString() == id).RemovePerson(person.Email);
                                        }
                                    }
                                }
                                break;
                            default:
                                Console.WriteLine($"{choice} nije valjani unos. Pritisnite enter za povratak na glavni izbornik.");
                                break;
                        }
                        break;
                    case "3":
                        foreach (var el in listOfEvents)
                        {
                            if (el.EndingDate < DateTime.Now)//izdvaja one koji su završili
                            {
                                Console.WriteLine($"Naziv: {el.Name} | Lokacija: {el.Location} | Završio prije: {Math.Round(DateTime.Now.Subtract(el.EndingDate).TotalDays * 10) / 10} dana | Trajao {Math.Round(el.StartingDate.Subtract(el.EndingDate).TotalHours * 10) / 10}");

                                Console.WriteLine($"Popis prisutnih sudionika:");
                                foreach (var person in listOfPersons)
                                {
                                    if (person.Presence.ContainsKey(el.Id) && person.Presence[el.Id])
                                        Console.Write(person.Email+", ");;
                                }

                                Console.WriteLine($"\nPopis ne prisutnih sudionika: ");
                                foreach (var person in listOfPersons)
                                {
                                    if (person.Presence.ContainsKey(el.Id) && person.Presence[el.Id] == false)
                                        Console.Write(person.Email + ", "); ;
                                }
                                Console.WriteLine($"\n\n");
                            }
                        }

                        Console.WriteLine("Pritisnite bilo koju tipku za izlaz");
                        Console.ReadLine();

                        break;
                    case "4"://gotovo
                        Console.WriteLine("Unesite podatke za event. \n"
                                        + "Naziv:");
                        var name = Console.ReadLine();
                        if (name == "")
                        {
                            Console.WriteLine("Unesite valjano ime");
                            break;
                        }

                        Console.WriteLine("Lokaciju:");
                        var location = Console.ReadLine();
                        if (location == "")
                        {
                            Console.WriteLine("Unesite valjanu lokaciju");
                            break;
                        }

                        Console.WriteLine("Datum i vrijeme početka (yyyy-MM-dd HH:mm:ss):");
                        DateTime start = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss",
                                               System.Globalization.CultureInfo.InvariantCulture);
                        if (start < DateTime.Now)
                        {
                            Console.WriteLine("Event mora biti u budućnosti");
                            break;
                        }

                        Console.WriteLine("Datum i vrijeme kraja (yyyy-MM-dd HH:mm):"); // i dodat HH:MinMin:SS
                        DateTime end = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm:ss",
                                               System.Globalization.CultureInfo.InvariantCulture);
                        if (end < start)
                        {
                            Console.WriteLine("Kraj eventa ne može biti prije početka");
                            break;
                        }

                        Console.WriteLine("popis pozvanih");
                        string[] emails = Console.ReadLine().Split(", ");

                        var invited = new List<string>();
                        foreach (var person in listOfPersons)
                        {
                            var dost = true;
                            if (emails.Contains(person.Email) == true)
                            {
                                bool add = true;
                                foreach (var personsIds in person.Presence.Keys)
                                {
                                    foreach (var ev in listOfEvents)
                                    {
                                        if (ev.Id == personsIds && dost==true)
                                        {
                                            if (ev.StartingDate < end && start < ev.EndingDate || ev.StartingDate > end && start > ev.EndingDate)
                                            {
                                                add = false;
                                                Console.WriteLine($"{person.Name} {person.Surname} nije dostupna u to vrijeme");
                                                dost = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (add == true)
                                {
                                    invited.Add(person.Email);
                                }
                            }
                        }
                        var newEvent = new Event(name, location, start, end, invited, invited);
                        foreach (var person in listOfPersons)
                        {
                            foreach (var email in invited)
                            {
                                if (person.Email == email)
                                {
                                    person.AddPresence(newEvent.Id);
                                }
                            }
                        }
                        newEvent.ChangeEmails(invited, invited);
                        if (Potvrda() == false)
                        {
                            break;
                        }
                        listOfEvents.Add(newEvent);


                        Console.WriteLine("Pritisnite bilo koju tipku za izlaz");
                        Console.ReadLine();
                        break;

                    default:
                        break;//samo korisnika vraća natrag u loop
                }
                Console.Clear();
            } while (loop == 1);
        }
    }
}