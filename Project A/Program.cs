using Project_A;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


public class Program
{
    static public bool RangeTest(string answer, float floor, float ceil)
    {
        if (int.Parse(answer) >= floor && int.Parse(answer) <= ceil)
        {
            return true;
        }
        else
        {
            return false;
        };
    }

    public static DateOnly toDate(string input)
    {
        DateOnly date;

        if (DateOnly.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            return date;
        }
        else
        {
            throw new Exception("Некоректний ввід дати");
        }
    }

    public static Brigade ChooseBrigade(Company company)
    {                
        string input;
        int choice;

        while (true)
        {
            try
            {
                Console.WriteLine("\nОберіть бригаду зі списку:");
                for (int i = 0; i < company.Brigades.Count; i++)
                {
                    Brigade brig = company.Brigades[i];
                    Console.WriteLine($"{i} - Назва: {brig.Name} - {brig.BrigadeCommander.GetFullInfo()} - кіл-ть робітників: {brig.GetWorkerCount()}");
                }

                Console.Write("\nВаш вибір: ");

                input = Console.ReadLine();

                if (!RangeTest(input, 0, company.Brigades.Count - 1)) { throw new Exception("Некоректний вибір"); }

                if (!int.TryParse(input, out choice)) { throw new Exception("Ввід має бути вказано числом"); }

                return company.Brigades[choice];
            }
            catch (Exception ex) { Console.WriteLine("\n" + ex.Message); }
        }
    }

    public static Worker ChooseWorker(Brigade brigade) 
    {
        string input;
        int choice;

        while (true)
        {
            try
            {
                Console.WriteLine("\nОберіть робітника зі списку:");
                for (int i = 0; i < brigade.Workers.Count; i++)
                {
                    Worker worker = brigade.Workers[i];
                    Console.WriteLine($"{i} - {worker.GetFullInfo()}");
                }

                Console.Write("\nВаш вибір: ");

                input = Console.ReadLine();

                if (!RangeTest(input, 0, brigade.Workers.Count - 1)) { new Exception("Некоректний вибір"); }

                if (!int.TryParse(input, out choice)) { throw new Exception("Ввід має бути вказано числом"); }

                return brigade.Workers[choice];
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Company company;
        Brigade brigade;
        BrigadeCommander commander;
        bool running = true, companyChoose = true, brigadeChoose = true;
        string input;
        int choice;
        List<Company> companies = new List<Company>();
        while (running)
        {
            //Обрати пункт початкового меню
            while (true)
            {
                try
                {
                    Console.WriteLine("\n--- Меню ---");
                    Console.WriteLine(
                        "\n1 - Додати компанію" +
                        "\n2 - Обрати компанію");
                    Console.Write("\nВаш вибір: ");

                    input = Console.ReadLine();

                    if (!RangeTest(input, 1, 2))
                    {
                        throw new Exception("Некоректний вибір");
                    }

                    if (input == "2" && companies.Count == 0) { throw new Exception("Список компаній порожній, будь ласка, спочатку додайте компанію до списку"); }

                    break;
                }
                catch (Exception ex) { Console.WriteLine($"\n{ex.Message}"); }; 

            }

            choice = int.Parse(input);

            switch (choice)
            {
                //Створення компанії
                case 1:
                    while (true)
                    {   string[] compInfo;
                        Company newCompany;
                        try
                        {
                            Console.WriteLine("\nВведіть ЧЕРЕЗ ПРОБІЛ назву компанії(3-20)(без спеціальних символів) та дату заснування(yyyy-MM-dd)");
                            Console.Write("\nВаш ввід: ");
                            input = Console.ReadLine();

                            compInfo = input.Split(" ");

                            if (compInfo.Length != 2) { throw new Exception("Некоректна кіл-ть даних"); }

                            newCompany = new(compInfo[1], compInfo[0]);

                            companies.Add(newCompany);

                            Console.WriteLine("\nКомпанію успішно створено");

                            break;
                        }
                        catch (Exception ex) { Console.WriteLine($"\n{ex.Message}"); }

                    }
                    break;
                    

                case 2:

                    int companyChoice;

                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("\nОберіть компанію зі списку:");
                            for (int i = 0; i < companies.Count; i++)
                            {
                                Console.Write($"{i} - ");
                                companies[i].PrintToDisplay();
                            }

                            Console.Write("\nВаш вибір: ");

                            input = Console.ReadLine();

                            if (!RangeTest(input, 0, companies.Count - 1))
                            {
                                throw new Exception("Некоректний вибір");
                            }
                            break;

                        }
                        catch (Exception ex) { Console.WriteLine($"\n{ex.Message}"); };
                    }//обрати компанію

                    companyChoice = int.Parse(input);
                    company = companies[companyChoice];

                    companyChoose = true;

                    while (companyChoose)//Робота з обраною компанією
                    {
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("\n--- Меню ---");
                                Console.WriteLine(
                                    "\n0 - Назад" +
                                    "\n1 - Додати бригаду" +
                                    "\n2 - Видалити бригаду" +
                                    "\n3 - Вивести загальну кіл-ть працівників" +
                                    "\n4 - Вивести інформацію про компанію" +
                                    "\n5 - Обрати бригаду");
                                Console.Write("\nВаш вибір: ");

                                input = Console.ReadLine();

                                if (!RangeTest(input, 0, 5))
                                {
                                    throw new Exception("Некоректний вибір");
                                }

                                choice = int.Parse(input);

                                if ((choice == 2 || choice == 5) && company.Brigades.Count == 0) { throw new Exception("Список бригад порожній, будь ласка, спочатку додайте бригади до списку"); }

                                break;

                            }
                            catch (Exception ex) { Console.WriteLine("\n" + ex.Message); };
                        }//Меню

                        switch (choice)
                        {
                            case 0:
                                companyChoose = false;
                                break;//Назад

                            case 1:
                                while (true)
                                {
                                    try
                                    {
                                        string[] comInfo;
                                        int comAge;

                                        Console.WriteLine("\nВкажіть дані командира бригади(Ім'я(3-20), Прізвище(3-20) та Вік(18-70))");
                                        Console.Write("Через ПРОБІЛ: ");

                                        input = Console.ReadLine();

                                        comInfo = input.Split(" ");

                                        if (comInfo.Length != 3) { throw new Exception("Некоректна кіл-ть вказаних даних"); }

                                        if (!int.TryParse(comInfo[2], out comAge)) { throw new Exception("Вік має бути вказано числом"); }

                                        BrigadeCommander newBrigCom = new(comInfo[0], comInfo[1], comAge);

                                        commander = (BrigadeCommander)newBrigCom.Clone();

                                        Console.WriteLine("\nДані успішно вказано");

                                        break;

                                    }
                                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                                }

                                while (true)
                                {
                                    try
                                    {
                                        string[] brigInfo;

                                        Console.WriteLine("\nВведіть дані бригади(Назву(3-20) та та Місто роботи(3-20))");
                                        Console.Write("Через ПРОБІЛ: ");

                                        input = Console.ReadLine();

                                        brigInfo = input.Split(" ");

                                        if (brigInfo.Length != 2) { throw new Exception("Некоректна кіл-ть вказаних даних"); }

                                        Brigade brig = new(brigInfo[0], commander, brigInfo[1]);

                                        company.AddBrigade(brig);

                                        Console.WriteLine("\nБригаду успішно створено та додано");

                                        break;
                                    }
                                    catch (Exception ex) { Console.WriteLine(ex.Message); }

                                }

                                break;//Додати бригаду

                            case 2:
                                while (true)
                                {
                                    try
                                    {
                                        Brigade brig;

                                        brig = ChooseBrigade(company);

                                        company.DeleteBrigade(brig);

                                        Console.WriteLine("\nБригаду успішно видалено");

                                        break;
                                    }
                                    catch (Exception ex) { Console.WriteLine("\n" + ex.Message); }
                                }
                                break;//Видалити бригаду

                            case 3:
                                Console.WriteLine($"\nЗагальна кіл-ть робітників(без урахування бригадирів): {company.GetTotalWorkers()}");

                                break;//Вивести кіл-ть робітників

                            case 4:
                                Console.WriteLine("\n");
                                company.PrintToDisplay();
                                Console.WriteLine("Список бригад:");
                                for (int i = 0; i < company.Brigades.Count; i++)
                                {
                                    Brigade brig = company.Brigades[i];
                                    Console.WriteLine($"{i} - Назва: {brig.Name} - {brig.BrigadeCommander.GetFullInfo()} - кіл-ть робітників: {brig.GetWorkerCount()}");
                                }

                                break;//Вивести компанію на екран

                            case 5:
                                brigade = ChooseBrigade(company);

                                brigadeChoose = true;

                                while (brigadeChoose)
                                {
                                    //Меню
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.WriteLine("\n--- Меню ---");
                                            Console.WriteLine(
                                                "\n0 - Назад" +
                                                "\n1 - Додати робітника" +
                                                "\n2 - Видалити робітника" +
                                                "\n3 - Вивести кіл-ть робітників" +
                                                "\n4 - Вивести загальну інформацію про робітників" +
                                                "\n5 - Вивести загальну інформацію про бригадира" +
                                                "\n6 - Перекваліфікувати робітника");
                                            Console.Write("\nВаш вибір: ");

                                            input = Console.ReadLine();

                                            if (!RangeTest(input, 0, 6))
                                            {
                                                throw new Exception("Некоректний вибір");
                                            }

                                            choice = int.Parse(input);

                                            if ((choice == 2 || choice == 4 || choice == 6) && brigade.Workers.Count == 0) { throw new Exception("Список робітників порожній, будь ласка, спочатку додайте робітників до списку"); }

                                            break;

                                        }
                                        catch (Exception ex) { Console.WriteLine("\n" + ex.Message); };

                                    }//Меню

                                    switch (choice)
                                    {
                                        //Назад
                                        case 0:
                                            brigadeChoose = false;
                                            break;

                                        //Додати робітника
                                        case 1:
                                            while (true)
                                            {   
                                                try
                                                {
                                                    string[] workerInfo;
                                                    int workerAge, workerId;
                                                    Specialization workerSpecialization;

                                                    Console.WriteLine("\nВведіть дані робітника(Id(число), Ім'я(3-20), Прізвище(3-20), Вік(18-70), Спеіцалізація)");
                                                    Console.Write("Через пробіл: ");


                                                    input = Console.ReadLine();
                                                    workerInfo = input.Split(" ");

                                                    if (workerInfo.Length != 5) { throw new Exception("Некоректна кіл-ть вказаних даних"); }

                                                    if (!int.TryParse(workerInfo[0], out workerId)) { throw new Exception("Id має бути вказано числом"); }

                                                    if (!int.TryParse(workerInfo[3], out workerAge)) { throw new Exception("Вік має бути вказано числом"); }

                                                    if (!Enum.TryParse<Specialization>(workerInfo[4], out workerSpecialization)) { throw new ArgumentException("Некоректний ввід спеціалізації"); };

                                                    Worker newWorker = new(workerId, workerInfo[1], workerInfo[2], workerAge, workerSpecialization);

                                                    brigade.AddWorker(newWorker);
                                                    Console.WriteLine("\nРобітника успішно додано");

                                                    break;
                                                }
                                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                                            }
                                            break;

                                        //Видалити робітника
                                        case 2:
                                            while (true)
                                            {
                                                try
                                                {
                                                    Worker worker = ChooseWorker(brigade);
                                                    brigade.RemoveWorker(worker.Id);

                                                    Console.WriteLine("Робітника успішно видалено");

                                                    break;
                                                }
                                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                                            }

                                            break;

                                        //Кіл-ть робітників
                                        case 3:
                                            Console.WriteLine($"\nКіл-ть робітників: {brigade.GetWorkerCount()}");
                                            break;

                                        //Список робітників
                                        case 4:
                                            Console.WriteLine("Робітники:");
                                            for (int i = 0; i < brigade.Workers.Count; i++)
                                            {
                                                Worker worker = brigade.Workers[i];
                                                Console.WriteLine($"{i} - {worker.GetFullInfo()}");
                                            }
                                            break;

                                        //Інформація про бригадира
                                        case 5:
                                            Console.WriteLine(brigade.BrigadeCommander.GetFullInfo());
                                            break;

                                        //Перекваліфікувати робітника
                                        case 6:
                                            while (true)
                                            {
                                                try
                                                {
                                                    Specialization newSpecialization;
                                                    Worker worker = ChooseWorker(brigade);
                                                    Console.Write("\nВведіть нову спеціалізацію: ");

                                                    input = Console.ReadLine();

                                                    if (!Enum.TryParse<Specialization>(input, out newSpecialization)) { throw new ArgumentException("Некоректний ввід спеціалізації"); }

                                                    worker.Promote(newSpecialization);

                                                    break;
                                                }
                                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                                            }
                                            break;
                                    }
                                }//Робота з обраною бригадою

                                break;//Вибір бригади та робота з нею
                        }
                    }//Робота з обраною компанією
                    
                    //while (true)
                    //{
                    //    try
                    //    {

                    //        break;
                    //    }
                    //    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    //}

                    break;//Вибір компанії та робота з нею
            }


        }
    }
}