using System;
using Core.Domains.Studio.Shared;
using Core.Domains.Studio.Entities;

namespace Core.Domains.Studio.Persistence;

public interface IProjectRepository
{
    /// <summary>
    /// Get project form id
    /// </summary>
    Project? GetBy(ProjectId id);
}
