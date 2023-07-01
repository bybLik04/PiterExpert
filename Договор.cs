using System;
using System.Collections.Generic;

#nullable disable

namespace PiterExp
{
    public partial class Договор
    {
        public Договор()
        {
            ДоговорУслугаs = new HashSet<ДоговорУслуга>();
            УслугиВэдs = new HashSet<УслугиВэд>();
        }

        public int IdКлиента { get; set; }
        public int НомерДоговора { get; set; }
        public DateTime ДатаДоговора { get; set; }
        public decimal Сумма { get; set; }
        public DateTime? ДатаВыполненияДоговора { get; set; }
        public int IdСотрудника { get; set; }

        public virtual Клиенты IdКлиентаNavigation { get; set; }
        public virtual Сотрудники IdСотрудникаNavigation { get; set; }
        public virtual ICollection<ДоговорУслуга> ДоговорУслугаs { get; set; }
        public virtual ICollection<УслугиВэд> УслугиВэдs { get; set; }
    }
}
