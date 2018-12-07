using System;
using System.Collections.Generic;
using System.Linq;

namespace Internship_1_ContactBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var adressBook = new List<Tuple<string, string, string, string>>() { };
            string input;

            //Added 8 names to speed up the proces
            adressBook = FillDummyList();

            do
            {
                Console.WriteLine("Opcije: \n" +
                                  "1. Dodavanje novog upisa \n" +
                                  "2. Promjena imena, adrese ili broja \n" +
                                  "3. Brisanje upisa \n" +
                                  "4. Pretraga po broju \n" +
                                  "5. Pretraga po imenu \n" +
                                  "6. Izlazak iz programa \n");

                Console.Write("Odabir opcije: ");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Odabrao si prvu opciju: Dodavanje novog upisa");
                        Console.Write("Unesi ime: ");
                        var name = Console.ReadLine();
                        Console.Write("Unesi prezime: ");
                        var surname = Console.ReadLine();
                        Console.Write("Unesi adresu: ");
                        var adress = Console.ReadLine();
                        Console.Write("Unesi broj: ");
                        var phoneNumber = Console.ReadLine();
                        Console.Write("Potvrdi unos broja: ");
                        var phoneNumberCheck = Console.ReadLine();

                        phoneNumber = CleanNumber(phoneNumber);
                        phoneNumberCheck = CleanNumber(phoneNumberCheck);

                        if (phoneNumber != phoneNumberCheck)
                        {
                            Console.WriteLine("Pogresna potvrda, povratak na opcije!");
                            Console.WriteLine();
                            break;
                        }

                        bool parsed = Int32.TryParse(phoneNumber, out int a);
                        while (!parsed)
                        {
                            Console.WriteLine("Pogresan unos, podatak mora biti broj!");
                            Console.Write("Unesi broj: ");
                            phoneNumber = Console.ReadLine();
                            parsed = Int32.TryParse(phoneNumber, out a);
                        }

                        adressBook = AddingNewEntry(name, surname, adress, phoneNumber, adressBook);

                        Console.WriteLine();

                        break;
                    case "2":
                        Console.WriteLine("Odabrao si drugu opciju: Promjena imena, adrese ili broja");
                        DisplayAdressBook(adressBook);
                        Console.Write("Unesi broj telefona osobe cije podatke zelis mjenjati: ");
                        var numberForChange = Console.ReadLine();

                        int itemIndex = -1;

                        numberForChange = CleanNumber(numberForChange);
                        for (var i = 0; i < adressBook.Count; i++)
                            if (numberForChange == adressBook[i].Item4)
                            {
                                itemIndex = i;
                                break;
                            }

                        if (itemIndex == -1)
                        {
                            Console.WriteLine("Trazeni broj ne postoji. Povratak na opcije!\n");
                            break;
                        }

                        Console.WriteLine("Odabrali ste promjenu upisa: " + adressBook[itemIndex]);

                        Console.Write("Unesi sto zelis mjenjati rijecima Ime, Adresa, Broj: ");
                        var change = Console.ReadLine();
                        if (change.ToLower() == "ime")
                        {
                            Console.Write("Odabrao si promjenu imena. Zelis li promjeniti ime ili prezime?: ");
                            change = Console.ReadLine();
                        }

                        string changeValue;

                        switch (change.ToLower())
                        {
                            case "ime":
                                Console.Write("Unesi novo ime: ");
                                changeValue = Console.ReadLine();
                                changeValue = changeValue.ToLower();
                                changeValue = char.ToUpper(changeValue[0]) + changeValue.Substring(1);
                                adressBook[itemIndex] = Tuple.Create(changeValue, adressBook[itemIndex].Item2, adressBook[itemIndex].Item3, adressBook[itemIndex].Item4);
                                Console.WriteLine("Promjena imena uspjesno izvrsena:\n");
                                Console.WriteLine(adressBook[itemIndex]+"\n");

                                break;
                            case "prezime":
                                Console.Write("Unesi novo prezime: ");
                                changeValue = Console.ReadLine();
                                changeValue = changeValue.ToLower();
                                changeValue = char.ToUpper(changeValue[0]) + changeValue.Substring(1);
                                adressBook[itemIndex] = Tuple.Create(adressBook[itemIndex].Item1, changeValue, adressBook[itemIndex].Item3, adressBook[itemIndex].Item4);
                                Console.WriteLine("Promjena prezimena uspjesno izvrsena:\n");
                                Console.WriteLine(adressBook[itemIndex] + "\n");

                                break;
                            case "adresa":
                                Console.Write("Unesi novu adresu: ");
                                changeValue = Console.ReadLine();
                                changeValue = changeValue.ToLower();
                                changeValue = char.ToUpper(changeValue[0]) + changeValue.Substring(1);
                                adressBook[itemIndex] = Tuple.Create(adressBook[itemIndex].Item1, adressBook[itemIndex].Item2, changeValue, adressBook[itemIndex].Item4);
                                Console.WriteLine("Promjena adrese uspjesno izvrsena:\n");
                                Console.WriteLine(adressBook[itemIndex] + "\n");

                                break;
                            case "broj":
                                Console.Write("Unesi novi broj: ");
                                changeValue = Console.ReadLine();
                                Console.Write("Potvrda novog broja: ");
                                var changeValueCheck = Console.ReadLine();

                                changeValue = CleanNumber(changeValue);
                                changeValueCheck = CleanNumber(changeValueCheck);

                                parsed = Int32.TryParse(changeValue, out a);
                                while (!parsed)
                                {
                                    Console.WriteLine("Pogresan unos, podatak mora biti broj!");
                                    Console.Write("Unesi broj: ");
                                    changeValue = Console.ReadLine();
                                    parsed = Int32.TryParse(changeValue, out a);
                                }

                                if (changeValue != changeValueCheck)
                                {
                                    Console.WriteLine("Pogresna potvrda, povratak na opcije!\n");
                                    break;
                                }

                                var existance=false;

                                for (var i = 0; i < adressBook.Count; i++)
                                    if (changeValue == adressBook[i].Item4)
                                    {
                                        existance = true;
                                        break;
                                    }
                                if (existance)
                                {
                                    Console.WriteLine("Uneseni broj vec postoji! Povratak na opcije.\n");
                                    break;
                                }

                                adressBook[itemIndex] = Tuple.Create(adressBook[itemIndex].Item1, adressBook[itemIndex].Item2, adressBook[itemIndex].Item3, changeValue);
                                Console.WriteLine("Promjena broja uspjesno izvrsena:\n");
                                Console.WriteLine(adressBook[itemIndex] + "\n");

                                break;
                            default:
                                Console.WriteLine("Krivi unos, povratak na opcije!\n");
                                break;
                        }

                        break;
                    case "3":
                        Console.WriteLine("Odabrao si trecu opciju: Brisanje upisa");

                        if (adressBook.Count == 0)
                        {
                            Console.WriteLine("Adresar je prazan. Odaberi opciju 1 kako bi dodao novi unos!\n");
                            break;
                        }

                        DisplayAdressBook(adressBook);

                        Console.Write("Unesi telefonski broj upisa koji zelis izbrisati: ");
                        phoneNumber = Console.ReadLine();
                        Console.Write("Potvrdi unos telefonskog broja: ");
                        phoneNumberCheck = Console.ReadLine();

                        phoneNumber = CleanNumber(phoneNumber);
                        phoneNumberCheck = CleanNumber(phoneNumberCheck);

                        if (phoneNumber != phoneNumberCheck)
                        {
                            Console.WriteLine("Pogresna potvrda, povratak na opcije!");
                            Console.WriteLine();
                            break;
                        }

                        parsed = Int32.TryParse(phoneNumber, out a);
                        while (!parsed)
                        {
                            Console.WriteLine("Pogresan unos, podatak mora biti broj!");
                            Console.Write("Unesi broj: ");
                            phoneNumber = Console.ReadLine();
                            parsed = Int32.TryParse(phoneNumber, out a);
                        }

                        adressBook = DeleteFromAdressBook(phoneNumber, adressBook);

                        DisplayAdressBook(adressBook);

                        break;
                    case "4":
                        Console.WriteLine("Odabrao si cetvrtu opciju: Pretraga po broju");
                        if (adressBook.Count == 0)
                        {
                            Console.WriteLine("Adresar je prazan. Odaberi opciju 1 kako bi dodao novi unos!\n");
                            break;
                        }
                        Console.Write("Unesi telefonski broj koji pretrazujes: ");
                        phoneNumber = Console.ReadLine();

                        phoneNumber = CleanNumber(phoneNumber);

                        parsed = Int32.TryParse(phoneNumber, out a);
                        while (!parsed)
                        {
                            Console.WriteLine("Pogresan unos, podatak mora biti broj!");
                            Console.Write("Unesi broj: ");
                            phoneNumber = Console.ReadLine();
                            parsed = Int32.TryParse(phoneNumber, out a);
                        }

                        DisplayEntryByNumber(adressBook, phoneNumber);

                        break;
                    case "5":
                        Console.WriteLine("Odabrao si petu opciju: Pretraga po imenu");
                        if (adressBook.Count == 0)
                        {
                            Console.WriteLine("Adresar je prazan. Odaberi opciju 1 kako bi dodao novi unos!\n");
                            break;
                        }
                        Console.Write("Unesi ime za pretragu: ");
                        var nameForSearch = Console.ReadLine();

                        DisplayEntryByName(adressBook, nameForSearch);

                        break;
                    case "6":
                        Console.WriteLine("Odabrao si sestu opciju: Izlazak iz programa");
                        break;
                    default:
                        Console.WriteLine("Opcija nepoznata, pokusaj ponovno!\n");
                        break;
                }
            }
            while (input != "6");
        }

        static List<Tuple<string, string, string, string>> AddingNewEntry(string name, string surname, string adress, string phoneNumber, List<Tuple<string, string, string, string>> aBook)
        {
            bool exists = false;

            name = name.ToLower();
            surname = surname.ToLower();
            adress = adress.ToLower();

            name = char.ToUpper(name[0]) + name.Substring(1);
            surname = char.ToUpper(surname[0]) + surname.Substring(1);
            adress = char.ToUpper(adress[0]) + adress.Substring(1);

            if (aBook.Count > 0)
            {
                for (var i = 0; i < aBook.Count; i++)
                    if (phoneNumber == aBook[i].Item4)
                    {
                        exists = true;
                        break;
                    }
                if (exists)
                    Console.WriteLine("Broj vec postoji, povratak na opcije!");
                else
                {
                    aBook.Add(new Tuple<string, string, string, string>(name, surname, adress, phoneNumber));
                    Console.WriteLine("Novi upis uspjesno dodan!");
                }
            }
            else
            {
                aBook.Add(new Tuple<string, string, string, string>(name, surname, adress, phoneNumber));
                Console.WriteLine("Novi upis uspjesno dodan!");
            }

            return aBook;
        }

        static string CleanNumber(string number)
        {
            number = number.Replace(" ", "");
            number = number.Replace("-", "");

            return number;
        }

        static List<Tuple<string, string, string, string>> DeleteFromAdressBook(string number, List<Tuple<string, string, string, string>> aBook)
        {
            bool exists = false;

            if (aBook.Count > 0)
            {
                for (var i = 0; i < aBook.Count; i++)
                    if (number == aBook[i].Item4)
                    {
                        exists = true;
                        aBook.RemoveAt(i);
                        break;
                    }
            }

            if (exists)
                Console.WriteLine("Uspjesno izbrisan upis!\n");
            else
                Console.WriteLine("Uneseni broj ne postoji, povratak na opcije\n");

            return aBook;
        }

        static void DisplayAdressBook(List<Tuple<string, string, string, string>> aBook)
        {
            if (aBook.Count > 0)
            {
                var sortedList = aBook.OrderBy(x => x.Item1).ToList();
                sortedList = sortedList.OrderBy(x => x.Item2).ToList();
                Console.WriteLine("Lista upisa: \n");
                for (var i = 0; i < sortedList.Count; i++)
                    Console.WriteLine(sortedList[i] + "\n");
            }
            else
                Console.WriteLine("Ne postoji niti jedan upis. Odabirom opcije 1 mozes dodati jednog");
        }

        static void DisplayEntryByNumber(List<Tuple<string, string, string, string>> aBook, string number)
        {
            bool exists = false;

            for (var i = 0; i < aBook.Count; i++)
                if (number == aBook[i].Item4)
                {
                    exists = true;
                    Console.WriteLine("Trazeni unos je: " + aBook[i] + "\n");
                    break;
                }

            if (!exists)
                Console.WriteLine("Trazeni broj ne postoji u adresaru! Povratak na opcije\n");
        }

        static void DisplayEntryByName(List<Tuple<string, string, string, string>> aBook, string search)
        {
            bool exists = false;

            var sortedList = aBook.OrderBy(x => x.Item1).ToList();
            sortedList = sortedList.OrderBy(x => x.Item2).ToList();

            Console.WriteLine("Rezultat pretrage:\n");
            var nameLength = 0;
            var surnameLength = 0;

            for (var i = 0; i < aBook.Count; i++)
            {
                if (search.Length <= sortedList[i].Item1.Length)
                    nameLength = search.Length;
                else
                    nameLength = sortedList[i].Item1.Length;

                if (search.Length <= sortedList[i].Item2.Length)
                    surnameLength = search.Length;
                else
                    surnameLength = sortedList[i].Item2.Length;

                if (search.ToLower() == sortedList[i].Item1.Substring(0, nameLength).ToLower() || search.ToLower() == sortedList[i].Item2.Substring(0, surnameLength).ToLower())
                {
                    Console.WriteLine(sortedList[i] + "\n");
                    exists = true;
                }
            }
                if(!exists)
                    Console.WriteLine("Trazeno ime ne postoji!\n");
        }

        static List<Tuple<string, string, string, string>> FillDummyList()
        {
            var dummyList = new List<Tuple<string, string, string, string>>() { };
            dummyList.Add(new Tuple<string, string, string, string>("Mate", "Matic", "Maticeva 1", "01231"));
            dummyList.Add(new Tuple<string, string, string, string>("Ivo", "Ivic", "Iviceva 2", "01232"));
            dummyList.Add(new Tuple<string, string, string, string>("Jure", "Juric", "Juriceva 3", "01233"));
            dummyList.Add(new Tuple<string, string, string, string>("Stipe", "Stipic", "Stipiceva 4", "01234"));
            dummyList.Add(new Tuple<string, string, string, string>("Pero", "Peric", "Periceva 5", "01235"));
            dummyList.Add(new Tuple<string, string, string, string>("Duje", "Matic", "Maticeva 6", "01236"));
            dummyList.Add(new Tuple<string, string, string, string>("Ante", "Matic", "Maticeva 7", "01237"));
            dummyList.Add(new Tuple<string, string, string, string>("Matan", "Matic", "Maticeva 8", "01238"));

            return dummyList;
        }
    }
}
