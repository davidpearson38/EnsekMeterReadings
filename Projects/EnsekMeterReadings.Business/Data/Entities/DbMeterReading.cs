namespace EnsekMeterReadings.Business.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MeterReading")]
    public class DbMeterReading
    {
        [Key]
        public int Id { get; set; }

        public int AccountId { get; set; }

        public DateTime MeterReadingDateTime { get; set; }

        [Range(0, 99999)]
        public int Value { get; set; }
    }
}
