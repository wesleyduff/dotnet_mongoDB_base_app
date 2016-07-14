using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ReceiptType
    {
        public string RtypeAsString
        {
            get
            {
                return this.RType.ToString();
            }
            set
            {
                RType = (RTypes)Enum.Parse( typeof(RTypes), value, true);
            }
        }

        public RTypes RType { get; set; }
        public enum RTypes {
            SummaryHtml = 0,
            FullHtml = 1,
            Text = 2
        }
    }
}
