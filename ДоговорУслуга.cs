using System;
using System.Collections.Generic;

#nullable disable

namespace PiterExp
{
    public partial class ДоговорУслуга
    {
        public int IdДоговора { get; set; }
        public int IdУслуга { get; set; }

        public virtual Договор IdДоговораNavigation { get; set; }
        public virtual Услуги IdУслугаNavigation { get; set; }
    }
}
