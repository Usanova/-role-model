using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ролевая_модель
{
    [Serializable]
    public class Roles
    {
        public static List<string> roles = new List<string>() { "Guest", "Director", "DeputyDirector", "HeadOfSales", "HeadOfHumanResources", "HeadOfTechnicalDepartment", "MainAccountant", "Accountant", "Secretary" };
        public static Dictionary<string, List<string>> Heirs = new Dictionary<string, List<string>>()
        {
            { "Guest", new List<string>() { "Secretary", "Accountant"} },
            { "Director", new List<string>() },
            { "DeputyDirector", new List<string>() {"Director" }},
            { "HeadOfSales", new List<string>() {"DeputyDirector" }},
            { "HeadOfHumanResources", new List<string>() {"DeputyDirector" }},
            { "HeadOfTechnicalDepartment", new List<string>() {"DeputyDirector" }},
            { "MainAccountant", new List<string>() {"DeputyDirector" } },
            { "Accountant", new List<string>() {"MainAccountant" }},
            { "Secretary", new List<string>() {"HeadOfTechnicalDepartment", "HeadOfHumanResources", "HeadOfSales" } },
        };
        public static Dictionary<string, List<string>> Parents = new Dictionary<string, List<string>>()
        {
            { "Guest", new List<string>() {} },
            { "Director", new List<string>() { "DeputyDirector"} },
            { "DeputyDirector", new List<string>() {"HeadOfSales", "HeadOfHumanResources", "HeadOfTechnicalDepartment", "MainAccountant" }},
            { "HeadOfSales", new List<string>() {"Secretary" }},
            { "HeadOfHumanResources", new List<string>() {"Secretary" }},
            { "HeadOfTechnicalDepartment", new List<string>() {"Secretary" }},
            { "MainAccountant", new List<string>() { "Accountant" } },
            { "Accountant", new List<string>() {"Guest"}},
            { "Secretary", new List<string>() { "Guest" } },
        };
        public static string RoleNameToEnglish(string RoleName)
        {
            switch(RoleName)
            {
                case "Гость":
                    return "Guest";
                case "Директор":
                    return "Director";
                case "Заместитель директора":
                    return "DeputyDirector";
                case "Начальник отдела продаж":
                    return "HeadOfSales";
                case "Начальник отдела кадров":
                    return "HeadOfHumanResources";
                case "Начальник технического отдела":
                    return "HeadOfTechnicalDepartment";
                case "Бухгалтер":
                    return "Accountant";
                case "Главный бухгалтер":
                    return "MainAccountant";
                case "Секретарь":
                    return "Secretary";
                default:
                    throw new Exception();
            }
        }
        public static string RoleNameToRussian(string RoleName)
        {
            switch (RoleName)
            {
                case "Guest":
                    return "Гость";
                case "Director":
                    return "Директор";
                case "DeputyDirector":
                    return "Заместитель директора";
                case "HeadOfSales":
                    return "Начальник отдела продаж";
                case "HeadOfHumanResources":
                    return "Начальник отдела кадров";
                case "HeadOfTechnicalDepartment":
                    return "Начальник технического отдела";
                case "MainAccountant":
                    return "Главный бухгалтер";
                case "Accountant":
                    return "Бухгалтер";
                case "Secretary":
                    return "Секретарь";
                default:
                    throw new Exception();
            }
        }
        public bool Guest { get; set; }
        public bool Director { get; set; }
        public bool DeputyDirector { get; set; }
        public bool HeadOfSales { get; set; }
        public bool HeadOfHumanResources { get; set; }
        public bool HeadOfTechnicalDepartment { get; set; }
        public bool MainAccountant { get; set; }
        public bool Accountant { get; set; }
        public bool Secretary { get; set; }
        
    }
}
