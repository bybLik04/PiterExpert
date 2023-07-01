using System;
using System.Collections.Generic;

#nullable disable

namespace PiterExp
{
    public partial class СистемаНалогообложения
    {
        public СистемаНалогообложения()
        {
            СистемаНалогооблаженияКлиентs = new HashSet<СистемаНалогооблаженияКлиент>();
        }

        public int IdСистемы { get; set; }
        public string НазваниеСистемы { get; set; }

        public virtual ICollection<СистемаНалогооблаженияКлиент> СистемаНалогооблаженияКлиентs { get; set; }
    }
}
