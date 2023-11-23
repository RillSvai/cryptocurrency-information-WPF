using System;
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
            return $"At {ActionDate} | Details opened about asset with id:{AssetId}";
        }
    }
}
