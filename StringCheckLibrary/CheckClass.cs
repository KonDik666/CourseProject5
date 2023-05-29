using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace StringCheckLibrary
{
    public class CheckClass
    {
        /// <summary>
        /// метод проверки строк с именами
        /// </summary>
        
        public static bool CheckNameString(string text)
        {
            string regexAmountOfSymbols = @"^[а-яА-Я]{2,20}$";
            string regexDigits = @"\d";
                     
            if (!Regex.IsMatch(text, regexAmountOfSymbols, RegexOptions.IgnoreCase))
            {
                return false;
            }
            if (Regex.IsMatch(text, regexDigits, RegexOptions.IgnoreCase))
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// метод проверки строк с номерами теелфонов
        /// </summary>
        public static bool CheckPhoneNumberString(string text)
        {
            string regexPhoneNumber1 = @"^8[0-9]{10}$";
            string regexPhoneNumber2 = @"^[+]{1}7[0-9]{10}$";

            if (Regex.IsMatch(text, regexPhoneNumber1, RegexOptions.IgnoreCase) || Regex.IsMatch(text, regexPhoneNumber2, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
                                              
        }


        /// <summary>
        /// метод проверки строк с электронной почтой
        /// </summary>
       
        public static bool CheckEmailString(string text)
        {
            string regexMail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            if (Regex.IsMatch(text, regexMail, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }

           
        }

        /// <summary>
        /// метод проверки строк с паролями(пароль должен содержать как минимум одну: маленькую латинскую букву, загланую латинскую букву, цифру, символ, а так же иметь длинну не менее 8)
        /// </summary>
       
        public static bool CheckPasswordString(string text)
        {
            string regexPass = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

            if (Regex.IsMatch(text, regexPass, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        /// <summary>
        /// метод проверки логина(имеет схожую структуру что и метод проверки имен, с заменой русских букв на английские)
        /// </summary>
      
        public static bool CheckLoginString(string text)
        {
            string regexAmountOfSymbols = @"^[a-zA-Z]{2,20}$";
            string regexDigits = @"\d";

            if (!Regex.IsMatch(text, regexAmountOfSymbols, RegexOptions.IgnoreCase))
            {
                return false;
            }
            if (Regex.IsMatch(text, regexDigits, RegexOptions.IgnoreCase))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// метод для проверки медицинского кода АТХ классификации
        /// </summary>
       
        public static bool CheckMedecineCode(string text)
        {
            string regexCode = @"^[abcdghjlmnprsv]$";

            if (!Regex.IsMatch(text, regexCode, RegexOptions.IgnoreCase))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// метод для проверки строки на наличие только цифр
        /// </summary>
       
        public static bool OnlyDigits(string text)
        {
            string regexDigits = @"\d";
            if (!Regex.IsMatch(text, regexDigits, RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;

        }



    }
}
