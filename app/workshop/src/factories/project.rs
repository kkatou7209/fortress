use std::collections::HashSet;

use crate::{MemberId, Project, ProjectId, ProjectName, Ticket, ProjectSchedule};

/// A factory of projects.
#[derive(Debug, Clone)]
pub struct ProjectFactory {
    id: Option<ProjectId>,
    name: Option<ProjectName>,
    members: HashSet<MemberId>,
    tickets: HashSet<Ticket>,
    schedule: Option<ProjectSchedule>,
}

impl ProjectFactory {
    
    /// Get new instance.
    pub fn new() -> Self {
        Self {
            id: None,
            name: None,
            members: HashSet::new(),
            tickets: HashSet::new(),
            schedule: None,
        }
    }

    /// Set members of the project.
    pub fn members(mut self, members: impl IntoIterator<Item = MemberId>) -> Self {
        self.members = HashSet::from_iter(members);
        self
    }

    /// Set tickets of the project.
    pub fn tickets(mut self, tickets: impl IntoIterator<Item = Ticket>) -> Self {
        self.tickets = HashSet::from_iter(tickets);
        self
    }

    /// Set schedule of the project.
    pub fn schedule(mut self, schedule: ProjectSchedule) -> Self {
        self.schedule = Some(schedule);
        self
    }

    /// Create a new project.
    pub fn create(self) -> Project {

        let id = self.id.expect("The project ID not provided.");

        let name = self.name.expect("The name of project not provided.");

        Project::restore(
            id,
            name,
            self.members,
            self.tickets,
            self.schedule,
        )
    }
}