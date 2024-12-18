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

## Архітектура

Проєкт складається з таких основних компонентів:

- **Worker** — клас для представлення робітника.
- **BrigadeCommander** — клас для представлення бригадира.
- **Brigade** — клас для представлення будівельної бригади.
- **Company** — клас для представлення компанії.
- **IPerson** — інтерфейс для спільних характеристик людей (робітників і бригадирів).
- **IPrintable** — інтерфейс для друку інформації.
- **Specialization** — перелік можливих спеціалізацій робітників.

![image](https://github.com/user-attachments/assets/1b09748f-6887-41f1-9d39-f2a963b92bf7)


---

## Класи та інтерфейси

### 1. **Worker**

```csharp
public class Worker : IPerson
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public Specialization Specialization { get; set; }

    public void Promote(Specialization newSpecialization);
    public string GetFullName();
}
```

### 2. **BrigadeCommander**

```csharp
public class BrigadeCommander : IPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public string GetFullName();
}
```

### 3. **Brigade**

```csharp
public class Brigade
{
    public string Name { get; set; }
    public BrigadeCommander BrigadeCommander { get; set; }
    public List<Worker> Workers { get; set; }
    public string Location;

    public void AddWorker(Worker worker);
    public bool RemoveWorker(int workerId);
    public int GetWorkerCount();
}
```

### 4. **Company**

```csharp
public class Company : IPrintable
{
    public DateTime Founded;
    public string Name;
    public List<Brigade> Brigades;

    public void AddBrigade(Brigade brigade);
    public void DeleteBrigade(Brigade brigade);
    public int GetTotalWorkers();
    public void PrintToDisplay();
}
```

### 5. **Інтерфейси**

***IPerson***

```csharp
public interface IPerson
{
    string FirstName { get; set; }
    string LastName { get; set; }
    int Age { get; set; }
    string GetFullName();
}
```

***IPrintable***

```csharp
public interface IPrintable
{
    void PrintToDisplay();
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
