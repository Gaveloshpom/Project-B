using Project_A;

namespace Test
{
    [TestClass]
    public class CompanyTest
    {
        [TestMethod]
        [DataRow("2024-06-01", "CompanyOne")]
        [DataRow("2023-12-31", "Company-Two")]
        [DataRow("2020-02-29", "Company Three")]
        public void Constructor_ValidInput(string dateStr, string name)
        {
            DateTime founded = DateTime.Parse(dateStr);
            Company company = new(founded, name);

            Assert.AreEqual(founded, company.Founded);
            Assert.AreEqual(name, company.Name);
        }

        [TestMethod]
        [DataRow("2025-06-01", "CompanyOne")]
        [DataRow("2023-13-31", "Company-Two")]
        [DataRow("2020-02-44", "Company Three")]
        [ExpectedException(typeof(Exception), "Некоректний ввід дати")]
        public void Constructor_InvalidDateInput_ThrowsException(string dateStr, string name) 
        {
            DateTime founded = DateTime.Parse(dateStr);
            Company company = new(founded, name);
        }

        [TestMethod]
        [DataRow("2024-06-01", "Company1")]
        [DataRow("2023-12-31", "Company!")]
        [DataRow("2020-02-20", "Co")]
        [DataRow("2020-02-20", "OneTwoThreeFourFiveSix")]
        [ExpectedException(typeof(Exception), "Некоректний ввід назви")]
        public void Constructor_InvalidNameInput_ThrowsException(string dateStr, string name)
        {
            DateTime founded = DateTime.Parse(dateStr);
            Company company = new(founded, name);
        }

        [TestMethod]
        [DataRow("Brigade", "Kyiv", "Andrii", "Koval", 30)]
        [DataRow("Brig", "Kharkiv", "Oleksandr", "Halushenko", 28)]
        [DataRow("Kvitka", "Odesa", "Serhiy", "Pachov", 53)]
        public void AddBrigade_ValidInput(string brigName, string location, string comFirstName, string comLastName, int comAge)
        {
            DateTime founded = DateTime.Parse("2024-06-01");

            Company company = new(founded, "Company");
            BrigadeCommander brigadeCommander = new(comFirstName, comLastName, comAge);
            Brigade brigade = new(brigName, brigadeCommander, location);

            company.AddBrigade(brigade);

            Assert.AreEqual(company.Brigades[0].Name, brigName);
            Assert.AreEqual(company.Brigades[0].BrigadeCommander.FirstName, comFirstName);
            Assert.AreEqual(company.Brigades[0].BrigadeCommander.LastName, comLastName);
            Assert.AreEqual(company.Brigades[0].BrigadeCommander.Age, comAge);
            Assert.AreEqual(company.Brigades[0].Location, location);
        }

        [TestMethod]
        [DataRow("Brigade", "Kyiv", "Andrii", "Koval", 30)]
        [DataRow("Brig", "Kharkiv", "Oleksandr", "Halushenko", 28)]
        [DataRow("Kvitka", "Odesa", "Serhiy", "Pachov", 53)]
        public void DeleteBrigade_BrigadeDeleted(string brigName, string location, string comFirstName, string comLastName, int comAge)
        {
            // Arrange
            DateTime founded = DateTime.Parse("2024-06-01");
            string companyName = "Company";
            Company company = new(founded, companyName);
            BrigadeCommander brigadeCommander = new(comFirstName, comLastName, comAge);
            Brigade brigade = new(brigName, brigadeCommander, location);

            company.AddBrigade(brigade);

            // Act
            company.DeleteBrigade(brigade);

            // Assert: перевірка, що бригади більше немає в списку
            Assert.IsFalse(company.Brigades.Contains(brigade), "Бригаду не було видалено");
        }

        [TestMethod]
        [DataRow("Brigade", "Kyiv", "Andrii", "Koval", 30)]
        [DataRow("Brig", "Kharkiv", "Oleksandr", "Halushenko", 28)]
        [DataRow("Kvitka", "Odesa", "Serhiy", "Pachov", 53)]
        public void PrintToDisplay_PrintsCorrectOutput(string brigName, string location, string comFirstName, string comLastName, int comAge)
        {
            // Arrange
            DateTime founded = DateTime.Parse("2024-06-01");
            string companyName = "Company";
            Company company = new(founded, companyName);
            BrigadeCommander brigadeCommander = new(comFirstName, comLastName, comAge);
            Brigade brigade = new(brigName, brigadeCommander, location);

            company.AddBrigade(brigade);

            using StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            company.PrintToDisplay();

            // Expected
            string brigNames = "";
            foreach (var brig in company.Brigades) { brigNames += brig.Name + " "; }
            string expectedOutput =
                $"Company: {companyName} | Founded: {founded} | Brigades: {brigNames}";

            // Assert
            string actualOutput = sw.ToString();
            Assert.AreEqual(expectedOutput, actualOutput);
        }


    }
}