using Core.Domains.Studio.Entities;
using Core.Domains.Studio.Shared;

namespace Core.Ports.Out.Studio;

public interface IWorkspaceRepository
{
    /// <summary>
    /// Get workspace from ID.
    /// </summary>
    Workspace? GetBy(WorkspaceId id);

    /// <summary>
    /// Get all workspaces.
    /// </summary>
    IEnumerable<Workspace> ListOf(MemberId member);

    /// <summary>
    /// Save state of workspace
    /// </summary>
    void Save(Workspace workspace);

    /// <summary>
    /// Add a new workspace.
    /// </summary>
    /// <param name="workspace"></param>
    void Add(Workspace workspace);

    /// <summary>
    /// Delete workspace.
    /// </summary>
    void Remove(Workspace workspace);

    /// <summary>
    /// Get next workspace ID.
    /// </summary>
    WorkspaceId NextWorkspaceId();
}
