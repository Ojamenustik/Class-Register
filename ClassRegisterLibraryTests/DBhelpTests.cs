using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister.Tests
{
    [TestClass()]
    public class DBhelpTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            string user = "user1";
            string password = "user1";
            ClassRegisterLibrary.User user_expected = new ClassRegisterLibrary.User();
            user_expected.imie = "Krzysztof";
            user_expected.nazwisko = "Piątek";
            user_expected.typ = "nauczyciel";

            ClassRegisterLibrary.User user_actual = DBhelp.Login(user, password);
            Assert.AreEqual(user_actual.nazwisko, user_expected.nazwisko);
            Assert.AreEqual(user_actual.imie, user_expected.imie);
            Assert.AreEqual(user_actual.typ, user_expected.typ);
        }

        [TestMethod()]
        public void OcenyUczniaTest()
        {
            Dictionary<int, int> test_pair = new Dictionary<int, int>();
            test_pair.Add(3, 1);

            Dictionary<int, int> actual_pair = DBhelp.OcenyUcznia(2);
            Assert.IsTrue(actual_pair.TryGetValue(3, out int val) == test_pair.TryGetValue(3, out int val1));
        }

        [TestMethod()]
        public void OcenyPrzedmiotyTest()
        {
            //to do
        }

        [TestMethod()]
        public void PrzedmiotyTest()
        {
            Dictionary<int, string> test_pair = new Dictionary<int, string>();
            test_pair.Add(5, "Chemia");

            Dictionary<int, string> actual_pair = DBhelp.Przedmioty();
            Assert.IsTrue(actual_pair.TryGetValue(5, out string val) == test_pair.TryGetValue(5, out string val1));
        }

        [TestMethod()]
        public void UczniowieTest()
        {
            ClassRegisterLibrary.Uzytkownik test_user = new ClassRegisterLibrary.Uzytkownik() { Imie = "Krzysztof", Nazwisko = "Piątek", id = 13 };

            List<ClassRegisterLibrary.Uzytkownik> test_list = new List<ClassRegisterLibrary.Uzytkownik>();
            test_list.Add(test_user);

            List<ClassRegisterLibrary.Uzytkownik> actual_list = DBhelp.Uczniowie();
            Assert.IsFalse(actual_list.Contains(test_user));
        }

        [TestMethod()]
        public void DodajoceneTest()
        {
            int test_reader = 3;

            Assert.IsTrue(test_reader == DBhelp.Dodajocene(2, 8, 5, "2019-02-10") + DBhelp.Dodajocene(4, 6, 4, "2019-02-10") + DBhelp.Dodajocene(7, 3, 5, "2019-02-09"));
        }

        [TestMethod()]
        public void DodajObecnoscTest()
        {
            int test_reader = 2;

            Assert.IsTrue(test_reader == DBhelp.DodajObecnosc(3, 1, "2019-02-10") + DBhelp.DodajObecnosc(3, 1, "2019-02-09"));
        }

        
    }
}