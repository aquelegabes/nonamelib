﻿using NoNameLib.Domain.Enums;

namespace NoNameLib.Domain.Tests.Mock;

public class AuditsRepository : IRepository<Auditable<TestDomain>>
{
    private readonly List<Auditable<TestDomain>> _auditable;
    internal AuditsRepository(
        DomainTestingObject testList)
    {
        _auditable = testList.AuditableList;
    }

    public void Delete(Auditable<TestDomain> domain)
    {
        _auditable.Remove(domain);
    }

    public void Dispose()
    {
    }

    public int SaveChanges(Auditable<TestDomain> domain, TransactionType eventType)
    {
        _auditable.Add(domain);
        return 1;
    }
}
