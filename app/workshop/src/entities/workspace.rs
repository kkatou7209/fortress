use std::collections::HashSet;

use crate::{MemberId, Project, ProjectId};

/// A worksspace which is project group.
#[derive(Debug, Clone, Eq)]
pub struct Workspace {
    id:       WorkspaceId,
    name:     WorkspaceName,
    projects: HashSet<ProjectId>,
    members:  HashSet<MemberId>,
}

impl Workspace {
    
    pub fn restore(
        id:       WorkspaceId,
        name:     WorkspaceName,
        projects: impl IntoIterator<Item = ProjectId>,
        members:  impl IntoIterator<Item = MemberId>,
    ) -> Self {
        Self {
            id,
            name,
            projects: HashSet::from_iter(projects),
            members: HashSet::from_iter(members),
        }
    }

    /// Get ID of the workspace.
    pub fn id(&self) -> &WorkspaceId {
        &self.id
    }

    /// Get name of the workspace.
    pub fn name(&self) -> &WorkspaceName {
        &self.name
    }
    
    /// Get projects belongs to the workspace.
    pub fn projects(&self) -> Vec<&ProjectId> {
        self.projects.iter().collect()
    }

    /// Get members of the workspace.
    pub fn members(&self) -> Vec<&MemberId> {
        self.members.iter().collect()
    }

    /// Rename workspace.
    pub fn rename(&mut self, name: WorkspaceName) {
        self.name = name;
    }

    /// Add new project.
    pub fn add_project(&mut self, project: &Project) {
        self.projects.insert(project.id().clone());
    }
}

impl PartialEq for Workspace {
    
    fn eq(&self, other: &Self) -> bool {
        self.id == other.id
    }
}

/// ID of a workspace.
#[derive(Debug, Clone, PartialEq, Eq, Hash)]
pub struct WorkspaceId(String);

/// Name of a workspace.
#[derive(Debug, Clone, PartialEq, Eq, Hash)]
pub struct WorkspaceName(String);