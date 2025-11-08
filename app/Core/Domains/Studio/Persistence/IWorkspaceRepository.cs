using Core.Domains.Studio.Entities;
using Core.Domains.Studio.Shared;

namespace Core.Domains.Studio.Persistence;

public interface IWorkspaceRepository
{
    /// <summary>
    /// Get workspace from ID.
    /// </summary>
    Workspace? GetBy(WorkspaceId id);

    /// <summary>
    /// Save state of workspace
    /// </summary>
    void Save(Workspace workspace);
}
