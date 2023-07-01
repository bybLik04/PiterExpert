using System;
using System.Collections.Generic;

#nullable disable

namespace PiterExp
{
    public partial class УслугиВэд
    {
        public int Id { get; set; }
        public bool? ИмпортЕаэс { get; set; }
        public bool? ЭкспортЕаэс { get; set; }
        public bool? ИмпортКромеЕаэс { get; set; }
        public bool? ЭкспортКромеЕаэс { get; set; }
        public bool? СтатотчетностьВФтсПоЕаэс { get; set; }
        public bool? УслугиВэд1 { get; set; }
        public int IdДоговора { get; set; }

        public virtual Договор IdДоговораNavigation { get; set; }
    }
}
