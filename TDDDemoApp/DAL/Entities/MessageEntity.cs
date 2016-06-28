using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TDDDemoApp.DAL.Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDateTime { get; set; }
    }
}