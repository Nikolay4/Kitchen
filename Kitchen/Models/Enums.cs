using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchen.Models
{
    public class Enums
    {
        public enum category
        {
            Умные_кухни = 1,
            Кухни_Модерн = 2,
            Кухни_Hi_Tech = 3,
            Кухни_Массив = 4,
            Столешницы_Акрил = 5,
            Столешницы_Кварц = 6,
            Столешницы_Пластик = 7,
            Бытовая_техника_Вытяжки = 8,
            Бытовая_техника_Духовые_шкафы = 9,
            Бытовая_техника_Варочные_поверхности = 10,
            Бытовая_техника_Встроенные_холодильники = 11,
            Бытовая_техника_Посудомоечные_машины = 12,
            Бытовая_техника_Измельчители = 13,
            Мойки_смесители = 14,
        }

        public enum brand
        {          
            Samsung = 1,
            LG_Hi_Maks = 2,
            CORIAN = 3,
            HANEX = 4,
            TriStone = 5,
            MONTELLI = 6,
            Bien_Stone = 7,
            NEOMARM = 8,
            Kerrock = 9,
            Plaza_Stone = 10,
            HanStone = 11,
            Arpa = 12,
            Resopal = 13,
            Formica = 14,
            Faber = 15,
            Kuppersberg = 16,
            Krona = 17,
            Shindo = 18,
            AEG = 19,
            Elicor = 20,
            Electrolux = 21,
            Airforce = 22,
            Cyclone = 23,
            Teka = 24,
            Vialona = 25,
            Forest = 26,
            SMEG = 27,
            NEFF = 28,
            NEFF_PLUS = 29,
            Zanussi = 30,
            Nardi = 31,
            Fornelli = 32,
            Gorenje = 33,
            Gorenje_Plus = 34,
            Miele = 35,
            Kuppersbusch = 36,
            Kaiser = 37,
            Zigmund_Shtain = 38,
            ASKO = 39,
            Liebherr = 40,
            Flavia = 41, 
            Bone_Crusher = 42, 
            IN_SINK = 43,
            Erator = 44,
            Blanco = 45, 
            Franke = 46,
            Polygran = 47,
            Schock = 48 
        }

        public static string categoryName(int cat)
        {
            switch (cat)
            {
                case 1: return "Умные кухни";
                case 2: return "Кухни Модерн";
                case 3: return "Кухни Hi_Tech";
                case 4: return "Кухни Массив";
                default: return "";
            }
        }

        
        public static string brandName(int brand)
        {
            switch (brand)
            {
                case 0: return "LG";
                case 1: return "Electrolux";
                default: return "Other";
            }
        }

        public static string GetZakazType(int type)
        {
            switch (type)
            {
                case 1: return "Zamer";
                case 2: return "Online_Serv";
                default: return "Zamer";
            }
        }

        public static string Status(int status)
        {
            switch (status)
            {
                case 1: return "Новый";
                case 2: return "В работе";
                case 3: return "Завершен";
                case 4: return "Отменен";
                case 5: return "Архив";
                default: return "";
            }
        }
    }
}
