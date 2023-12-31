﻿#pragma warning disable CS8602, CS8600

using System.Reflection;

namespace NoNameLib.Domain.Tests.Mock;

public class DomainTestingObject
{
    public static string GetConnectionString()
    {
        const string dbFileName = "db.sqlite3";
        string entryPoint = Assembly.GetEntryAssembly().Location;
        string path = Path.GetDirectoryName(entryPoint);
        string fullPath = Path.Combine(path!, dbFileName);

        return $"Data Source={fullPath};";
    }
    public List<TestDomain> TestDomainList;
    public List<Auditable<TestDomain>> AuditableList;
    public List<NotificationObject> NotificationObjectsList;

    public DomainTestingObject()
    {
        TestDomainList = new()
            {
                new TestDomain("Gabriel Santos", new DateTime(year: 1998, month: 9, day: 4))
                {
                    Id = Guid.NewGuid().ToString(),
                    ContractDate = DateTime.Now,
                    BeginDate = DateTime.Now.AddDays(1),
                    CPF = "31146807040",
                    IntValue = 10,
                },
                new TestDomain("Alex Mujica", new DateTime(year: 1992, month: 5, day: 13))
                {
                    Id = "6909c4c9-5ebc-4d34-960a-b78dc91263e6",
                    ContractDate = DateTime.Now,
                    BeginDate = DateTime.Now.AddDays(1),
                },
                new TestDomain("Teodoro Santos", new DateTime(year: 1977, month: 1, day: 22))
                {
                    ContractDate = DateTime.Now,
                    BeginDate = DateTime.Now.AddDays(1),
                },
                new TestDomain("Vinicius Fonseca", new DateTime(year: 2004, month: 12, day: 13))
                {
                    ContractDate = DateTime.Now,
                    BeginDate = DateTime.Now.AddDays(1),
                },
            };
        AuditableList = new();
        NotificationObjectsList = new();
    }
}