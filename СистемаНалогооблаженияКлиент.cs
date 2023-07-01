using System;
using System.Collections.Generic;

#nullable disable

namespace PiterExp
{
    public partial class СистемаНалогооблаженияКлиент
    {
        public int IdСистемы { get; set; }
        public int IdКлиента { get; set; }

        public virtual Клиенты IdКлиентаNavigation { get; set; }
        public virtual СистемаНалогообложения IdСистемыNavigation { get; set; }
    }
}
