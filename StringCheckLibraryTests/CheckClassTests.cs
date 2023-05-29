using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using StringCheckLibrary;

namespace StringCheckLibraryTests
{
    [TestClass]
    public class CheckClassTests
    {
        /// <summary>
        /// проверка подходящей строки имени, возврат true
        /// </summary>
        [TestMethod]
        public void CheckClass_NormalString_TrueReturned()
        {
            //arrange
            string text = "Алексей";
            bool expected = true;
            //act
            bool value = CheckClass.CheckNameString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка пустой строки имени, возврат false
        /// </summary>
        [TestMethod]
        public void CheckClass_EmptyString_FalseReturned()
        {
            //arrange
            string text = "";
            bool expected = false;
            //act
            bool value = CheckClass.CheckNameString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка строки имени содержащей цифры, возврат false
        /// </summary>
        [TestMethod]
        public void CheckClass_DigitsString_FalseReturned()
        {
            //arrange
            string text = "Алексей28";
            bool expected = false;
            //act
            bool value = CheckClass.CheckNameString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка строки имени содержащей английские буквы, возврат false
        /// </summary>
        [TestMethod]
        public void CheckClass_LatinLettersContainString_FalseReturned()
        {
            //arrange
            string text = "Alexей";
            bool expected = false;
            //act
            bool value = CheckClass.CheckNameString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка строки имени содержащей специальные символы, возврат false
        /// </summary>
        [TestMethod]
        public void CheckClass_SpecialSymbolsContainString_FalseReturned()
        {
            //arrange
            string text = "@лек6ей";
            bool expected = false;
            //act
            bool value = CheckClass.CheckNameString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка слишком большой строки, возврат false
        /// </summary>
        [TestMethod]
        public void CheckClass_TooManySymbolsContainString_FalseReturned()
        {
            //arrange
            string text = "Алексеееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееееей";
            bool expected = false;
            //act
            bool value = CheckClass.CheckNameString(text);
            //assert
            Assert.AreEqual(expected, value);
        }






        /// <summary>
        /// проверка корректной строки с номером телефона, насинающейся с 8,  возврат true
        /// </summary>
        [TestMethod]
        public void CheckPhoneNumber_CorrectPhoneStringStartingFrom_8_TrueReturned()
        {
            //arrange
            string text = "89025553535";
            bool expected = true;
            //act
            bool value = CheckClass.CheckPhoneNumberString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка корректной строки с номером телефона, насинающейся с +7,  возврат true
        /// </summary>
        [TestMethod]
        public void CheckPhoneNumber_CorrectPhoneStringStartingFrom_plus7_TrueReturned()
        {
            //arrange
            string text = "+79025553535";
            bool expected = true;
            //act
            bool value = CheckClass.CheckPhoneNumberString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка корректной строки с номером телефона, в которой слишком мало цифр,  возврат false
        /// </summary>
        [TestMethod]
        public void CheckPhoneNumber_LowNumbers_FalseReturned()
        {
            //arrange
            string text = "+79025";
            bool expected = false;
            //act
            bool value = CheckClass.CheckPhoneNumberString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка корректной строки с номером телефона, в которой содержатся буквы,  возврат false
        /// </summary>
        [TestMethod]
        public void CheckPhoneNumber_LettersInString_FalseReturned()
        {
            //arrange
            string text = "+7902абвг09";
            bool expected = false;
            //act
            bool value = CheckClass.CheckPhoneNumberString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка на пустую строку, возврат false
        /// </summary>
        [TestMethod]
        public void CheckPhoneNumber_EmptyString_FalseReturned()
        {
            //arrange
            string text = "";
            bool expected = false;
            //act
            bool value = CheckClass.CheckPhoneNumberString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка корректной строки с номером телефона, с пробелами,  возврат false
        /// </summary>
        [TestMethod]
        public void CheckPhoneNumber_SpacesInString_TrueReturned()
        {
            //arrange
            string text = "+7902      555  3535";
            bool expected = false;
            //act
            bool value = CheckClass.CheckPhoneNumberString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка корректной строки с номером телефона, в которой слишком много цифр,  возврат false
        /// </summary>
        [TestMethod]
        public void CheckPhoneNumber_HighNumbers_FalseReturned()
        {
            //arrange
            string text = "+79025553535533552353";
            bool expected = false;
            //act
            bool value = CheckClass.CheckPhoneNumberString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка корректной строки с номером телефона, насинающейся с неверного числа(кода страны),  возврат false
        /// </summary>
        [TestMethod]
        public void CheckPhoneNumber_CorrectPhoneStringStartingFrom_IncorrectNumber_TrueReturned()
        {
            //arrange
            string text = "+59025553535";
            bool expected = false;
            //act
            bool value = CheckClass.CheckPhoneNumberString(text);
            //assert
            Assert.AreEqual(expected, value);
        }







        /// <summary>
        /// проверка корректной строки с электронной почтой, возврат true
        /// </summary>
        [TestMethod]
        public void CheckEmail_CorrectEmailString_TrueReturned()
        {
            //arrange
            string text = "lel@mail.ru";
            bool expected = true;
            //act
            bool value = CheckClass.CheckEmailString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка корректной строки с содержанием цифр и точек, возврат true
        /// </summary>
        [TestMethod]
        public void CheckEmail_CorrectEmailStringWithDigitsAndPoints_TrueReturned()
        {
            //arrange
            string text = "lel12.cool@gmail.com";
            bool expected = true;
            //act
            bool value = CheckClass.CheckEmailString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка email неверного формата(без текста до @), возврат false
        /// </summary>
        [TestMethod]
        public void CheckEmail_IncorrectEmailStringWithoutMailName_FalseReturned()
        {
            //arrange
            string text = "@gmail.com";
            bool expected = false;
            //act
            bool value = CheckClass.CheckEmailString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка email неверного формата(без @), возврат false
        /// </summary>
        [TestMethod]
        public void CheckEmail_IncorrectEmailStringWithoutAtSymbol_FalseReturned()
        {
            //arrange
            string text = "lel mail.ru";
            bool expected = false;
            //act
            bool value = CheckClass.CheckEmailString(text);
            //assert
            Assert.AreEqual(expected, value);
        }


        /// <summary>
        /// проверка email неверного формата(без .*domain*), возврат false
        /// </summary>
        [TestMethod]
        public void CheckEmail_IncorrectEmailStringWithoutPointDomain_FalseReturned()
        {
            //arrange
            string text = "lel@mail";
            bool expected = false;
            //act
            bool value = CheckClass.CheckEmailString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка email неверного формата(без .*domain*), возврат false
        /// </summary>
        [TestMethod]
        public void CheckEmail_EmptyString_FalseReturned()
        {
            //arrange
            string text = "";
            bool expected = false;
            //act
            bool value = CheckClass.CheckEmailString(text);
            //assert
            Assert.AreEqual(expected, value);
        }






        /// <summary>
        /// проверка корректной строки с паролем(содержит как минимум одну: маленькую латинскую букву, загланую латинскую букву, цифру, символ, а так же имеет длинну не менее 8), возврат true
        /// </summary>
        [TestMethod]
        public void CheckPassword_CorrectPasswordString_TrueReturned()
        {
            //arrange
            string text = "q2WEr*ty08";
            bool expected = true;
            //act
            bool value = CheckClass.CheckPasswordString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка email неверного формата(без .*domain*), возврат false
        /// </summary>
        [TestMethod]
        public void CheckPassword_EmptyString_FalseReturned()
        {
            //arrange
            string text = "";
            bool expected = false;
            //act
            bool value = CheckClass.CheckPasswordString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка пароля без спец символов, возврат false
        /// </summary>
        [TestMethod]
        public void CheckPassword_PasswordWithoutSpecialSymbols_FalseReturned()
        {
            //arrange
            string text = "24ReRtttt";
            bool expected = false;
            //act
            bool value = CheckClass.CheckPasswordString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка пароля без цифр, возврат false
        /// </summary>
        [TestMethod]
        public void CheckPassword_PasswordWithoutDigits_FalseReturned()
        {
            //arrange
            string text = "@#ReRtttt";
            bool expected = false;
            //act
            bool value = CheckClass.CheckPasswordString(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка пароля без букв, возврат false
        /// </summary>
        [TestMethod]
        public void CheckPassword_PasswordWithoutLowercaseLetters_FalseReturned()
        {
            //arrange
            string text = "@##7$4545";
            bool expected = false;
            //act
            bool value = CheckClass.CheckPasswordString(text);
            //assert
            Assert.AreEqual(expected, value);
        }


        /// <summary>
        /// проверка слишком маленького пароля, возврат false
        /// </summary>
        [TestMethod]
        public void CheckPassword_TooLowPasssword_FalseReturned()
        {
            //arrange
            string text = "rR#5";
            bool expected = false;
            //act
            bool value = CheckClass.CheckPasswordString(text);
            //assert
            Assert.AreEqual(expected, value);
        }




        /// <summary>
        /// проверка праввиильной строки кода препарата, возврат true
        /// </summary>

        [TestMethod]
        public void CheckCode_CorrectMedecineCodeString_TrueReturned()
        {
            //arrange
            string text = "a";
            bool expected = true;
            //act
            bool value = CheckClass.CheckMedecineCode(text);
            //assert
            Assert.AreEqual(expected, value);
        }

        /// <summary>
        /// проверка пустой строки кода препарата, возврат false
        /// </summary>

        [TestMethod]
        public void CheckCode_EmptyMedecineCodeString_FalseReturned()
        {
            //arrange
            string text = "";
            bool expected = false;
            //act
            bool value = CheckClass.CheckMedecineCode(text);
            //assert
            Assert.AreEqual(expected, value);
        }
        /// <summary>
        /// проверка неправильной строки кода препарата, возврат false
        /// </summary>

        [TestMethod]
        public void CheckCode_IncorrectMedecineCodeString_FalseReturned()
        {
            //arrange
            string text = "xИ";
            bool expected = false;
            //act
            bool value = CheckClass.CheckMedecineCode(text);
            //assert
            Assert.AreEqual(expected, value);
        }
    }


}
