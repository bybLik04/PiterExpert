using System;
using System.Collections.Generic;

#nullable disable

namespace PiterExp
{
    public partial class Услуги
    {
        public Услуги()
        {
            ДоговорУслугаs = new HashSet<ДоговорУслуга>();
        }

        public int IdУслуги { get; set; }
        public string Название { get; set; }

        public virtual ICollection<ДоговорУслуга> ДоговорУслугаs { get; set; }
    }
}
