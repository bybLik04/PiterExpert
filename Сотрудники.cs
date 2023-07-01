using System;
using System.Collections.Generic;

#nullable disable

namespace PiterExp
{
    public partial class Сотрудники
    {
        public Сотрудники()
        {
            Договорs = new HashSet<Договор>();
        }

        public int IdСотрудника { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }

        public virtual ICollection<Договор> Договорs { get; set; }
    }
}
