using System;

namespace UdemyInfrastructure.UnitOfWorkRepository.Interfaces;

public interface IDateUpdated
{
    DateTime LastUpdateDate { get; set; }
}