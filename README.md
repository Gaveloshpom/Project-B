# Project_B

**Project_B** — Цей проект моделює систему управління компанією, де компанія організована у бригади, очолювані командирами бригад. Кожна бригада складається з робітників, що мають різні спеціалізації.

## Зміст

1. [Функціональність](#функціональність)
2. [Архітектура](#архітектура)
3. [Класи та інтерфейси](#класи-та-інтерфейси)
4. [Спеціалізації](#спеціалізації)
5. [Використання](#використання)

---

## Функціональність

- **Управління робітниками**: додавання, видалення та підвищення робітників.
- **Формування бригад**: створення бригад і призначення бригадирів.
- **Управління компанією**: керування бригадами та підрахунок загальної кількості робітників.
- **Друк інформації**: виведення інформації про компанію на екран.

---

## Архітектура проекту

Проект реалізовано за допомогою таких основних компонентів:

1. **Інтерфейси:**
   - `IPerson`: визначає основні властивості людини, такі як `FirstName`, `LastName`, `Age` та метод `GetFullInfo()`.
   - `ICloneable`: дозволяє клонувати об'єкти класу.
   - `IPrintable`: забезпечує функцію друку інформації для відображення.

2. **Абстрактний клас:**
   - `PersonBase`: є базовим класом для всіх людей у системі. Містить базові властивості та конструктор.

3. **Класи:**
   - `Worker`: представляє робітника компанії, успадковує `PersonBase`. Містить властивості:
     - `Id` — ідентифікатор робітника.
     - `Specialization` — спеціалізація робітника (використовує перелік `Specialization`).
     - Методи:
       - `Promote(newSpecialization)` — змінює спеціалізацію робітника.
       - `Clone()` — реалізує інтерфейс `ICloneable`.
   - `BrigadeCommander`: є командиром бригади, успадковує `PersonBase`.
   - `Brigade`: представляє бригаду робітників. Властивості:
     - `Name` — назва бригади.
     - `Location` — місце розташування.
     - Методи:
       - `AddWorker(worker)` — додає робітника до бригади.
       - `RemoveWorker(workerId)` — видаляє робітника за ID.
       - `GetWorkerCount()` — повертає кількість робітників.
   - `Company`: головний клас, що управляє кількома бригадами. Властивості:
     - `Name` — назва компанії.
     - `Founded` — дата заснування.
     - Методи:
       - `AddBrigade(brigade)` — додає бригаду.
       - `RemoveBrigade(brigadeName)` — видаляє бригаду.
       - `GetTotalWorkers()` — повертає кількість робітників в компанії.
       - `PrintToDisplay()` — виводить інформацію про компанію.
       - `toDate(string input)` — перетворює string в DateOnly.


![image](https://github.com/user-attachments/assets/991c4805-7ff0-4cf7-a09f-d0f29c892c2f)


---

## Класи та інтерфейси

### 1. **Worker**

```csharp
    public class Worker : PersonBase, ICloneable
    {
        private int id;
        private Specialization specialization;


        public int Id 
        { 
            get { return  id; }
            set
            {
                string input = value.ToString();
                int val;
                if (!int.TryParse(input, out val))
                {
                    throw new ArgumentException("Ввід має бути числом");
                };
                id = value;
            }
        }

        public Specialization Specialization 
        { 
            get { return specialization; }
            set 
            {
                string input = value.ToString();
                if(!Enum.TryParse<Specialization>(input, out specialization)) { throw new ArgumentException("Некоректний ввід спеціалізації"); };
            } 
        }


        public Worker(int id, string firstName, string lastName, int age, Specialization specialization) : base(firstName, lastName, age)
        {
            Id = id;
            Specialization = specialization;
        }

        public void Promote(Specialization newSpecialization) 
        {
            if (Specialization == newSpecialization) { throw new ArgumentException("Помилка! Спеціалізація вже присвоєна"); }
            Specialization = newSpecialization;
        }

        public override string GetFullInfo() 
        {
            return $"Робітник: {FirstName} {LastName} {Age} - {Specialization}";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
```

### 2. **BrigadeCommander**

```csharp
    public class BrigadeCommander : PersonBase, ICloneable
    {
        public override string GetFullInfo()
        {
            return $"Бригадний командир: {FirstName} {LastName} {Age}";
        }

        public BrigadeCommander(string firstName, string lastName, int age) : base(firstName, lastName, age) {}

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
```

### 3. **Brigade**

```csharp
    public class Brigade
    {
        private string name;
        private BrigadeCommander brigadeCommander;
        private List<Worker> workers = new List<Worker> { };
        private string location;

        public string Name
        {
            get { return name; }
            set
            {
                Regex regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁїЇіІєЄґҐ]{3,20}$");
                if (regex.IsMatch(value)) { this.name = value; }
                else { throw new ArgumentException("Некоректний ввід назви бригади"); };
            }
        }
        public BrigadeCommander BrigadeCommander { get; set; }
        public List<Worker> Workers { get { return workers; } }
        public string Location
        {
            get { return location; }
            set
            {
                Regex regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁїЇіІєЄґҐ]{3,20}$");
                if (regex.IsMatch(value)) { this.location = value; }
                else { throw new ArgumentException("Некоректний ввід локації"); };
            }
        }

        public Brigade(string name, BrigadeCommander brigadeCommander, string location) 
        {
            Name = name;
            BrigadeCommander = brigadeCommander;
            Location = location;
            workers = new List<Worker>();
        }

        public void AddWorker(Worker worker) 
        {
            workers.Add(worker);
        }

        public bool RemoveWorker(int workerId) 
        {
            try
            {
                int index = workers.FindIndex(worker => worker.Id == workerId);
                workers.RemoveAt(index);
                return true;
            }
            catch { return false; }
        }

        public int GetWorkerCount() 
        {
            return workers.Count;
        }
    }
```

### 4. **Company**

```csharp
    public class Company: IPrintable
    {
        private DateOnly founded;
        private string name;
        private List<Brigade> brigades;

        public DateOnly Founded 
        {
            get { return founded; }
            set 
            {
                if (value > DateOnly.FromDateTime(DateTime.Now)) { throw new ArgumentException("Некоректний ввід дати(Дата з майбутнього)"); }
                
                founded = value;
            } 
        }
        public string Name 
        {
            get {return name;}
            set
            {
                Regex regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁіІїЇєЄґҐ0-9\s\-\']{3,20}$");
                if (regex.IsMatch(value)) { this.name = value; }
                else { throw new ArgumentException("Некоректний ввід назви компанії"); };
            }
        }

        public List<Brigade> Brigades { get { return brigades; } }

        public Company(string founded, string name) 
        {
            Founded = toDate(founded);
            Name = name;
            brigades = new List<Brigade>();
        }

        public void AddBrigade(Brigade brigade) 
        {
            brigades.Add(brigade);
        }

        public void DeleteBrigade(Brigade brigade)
        {
            brigades.Remove(brigade);
        }

        public int GetTotalWorkers() 
        {
            int result = 0;
            foreach (var brig in Brigades) 
            {
                result += brig.GetWorkerCount();
            };
            return result;
        }

        public void PrintToDisplay() 
        {
            Console.WriteLine($"Компанія: {Name} | Дата заснування: {Founded} | Кіл-ть бригад: {brigades.Count} | Кіл-ть робітників: {GetTotalWorkers()}");
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
    }
```

### 5. **Інтерфейси та абстрактні класи**

***IPerson***

```csharp
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int Age { get; set; }
        string FullName { get; }
        string GetFullInfo();
    }
```

***IPrintable***

```csharp
public interface IPrintable
{
    void PrintToDisplay();
}
```

***Personbase***

```csharp
    public abstract class PersonBase : IPerson
    {

        private string firstName;
        private string lastName;
        private int age;


        public string FirstName
        {
            get { return firstName; }
            set
            {
                Regex regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁїЇіІєЄґҐ]{3,20}$");
                if (regex.IsMatch(value)) { this.firstName = value; }
                else { throw new ArgumentException("Некоректний ввід імені"); }
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                Regex regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁїЇіІєЄґҐ]{3,20}$");
                if (regex.IsMatch(value)) { this.lastName = value; }
                else { throw new ArgumentException("Некоректний ввід прізвища"); }
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 18 || value > 70)
                    throw new ArgumentException("Вік має бути в межах від 18 до 70");
                age = value;
            }
        }

        public string FullName { get { return $"{FirstName} {LastName}"; } }

        protected PersonBase(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public abstract string GetFullInfo();
    }
```

---
## Спеціалізації
**Доступні наступні спеціалізації робітників:**

* Bricklayer
* Carpenter
* Drywaller
* Electrician
* Glazier
* Insulator
* Ironworker
* Landscaper
* Painter
* Pipefitter
---
## Використання

### Як запустити проєкт?

1. **Клонувати репозиторій:**
   ```bash
   git clone https://github.com/Gaveloshpom/Project-A.git
2. **Відкрити рішення у Visual Studio.**

3. **Запустити проєкт**
---
## Контакти

**Автор:** Руслан Скицко 
**Email:** gaveloshpom@gmail.com
---
