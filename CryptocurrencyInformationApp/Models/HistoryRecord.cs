using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CryptocurrencyInformationApp.Models
{
    [Serializable]
    public class HistoryRecord
    {
        public required string AssetId {  get; set; }
        [XmlElement("DetailsOpened")]
        public DateTimeOffset ActionDate { get; set; }

        public HistoryRecord()
        {
            
        }
        public override string ToString()
        {
            return $"At {ActionDate} | About asset with id: {AssetId}";
        }
    }
}
