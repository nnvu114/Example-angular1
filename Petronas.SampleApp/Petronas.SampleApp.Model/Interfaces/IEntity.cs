using System;
using System.ComponentModel.DataAnnotations;

namespace Petronas.SampleApp.Model.Interfaces
{
    public interface IEntity
    {
        [Key]
        Guid Id { get; set; }

        DateTime? InsertedAt { get; set; }

        string InsertedBy { get; set; }

        DateTime? UpdatedAt { get; set; }

        string UpdatedBy { get; set; }

        bool IsDeleted { get; set; }
    }
}