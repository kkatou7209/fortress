use std::collections::{HashSet};

use anyhow::{Ok, Result, anyhow};

use crate::{
    Member,
    MemberId,
    Ticket, TicketId,
};

/// Entity of project.
/// 
/// A project holds members, tickets and reference to a workspace
/// it belongs to.
/// 
/// But the affiliation to workspace is optional.
#[derive(Debug, Clone)]
pub struct Project {
    id:        ProjectId,
    name:      ProjectName,
    members:   HashSet<MemberId>,
    tickets:   HashSet<Ticket>,
    span:  Option<ProjectSpan>,
}

impl Project {

    /// Restore existing project.
    pub fn restore(
        id:      ProjectId,
        name:    ProjectName,
        members: impl IntoIterator<Item = MemberId>,
        tickets: impl IntoIterator<Item = Ticket>,
        span: Option<ProjectSpan>,
    ) -> Self {
        Self {
            id,
            name,
            members: HashSet::from_iter(members),
            tickets: HashSet::from_iter(tickets),
            span,
        }
    }

    /// Get ID of the project.
    pub fn id(&self) -> &ProjectId {
        &self.id
    }

    /// Get name of the project.
    pub fn name(&self) -> &ProjectName {
        &self.name
    }

    /// Rename project.
    pub fn rename(&mut self, name: impl Into<String>) -> Result<()> {
        self.name = ProjectName::of(name)?;
        Ok(())
    }

    /// Get members of the project.
    pub fn members(&self) -> Vec<&MemberId> {
        self.members.iter().collect()
    }

    /// Get tickets of the project.
    pub fn tickets(&self) -> Vec<&Ticket> {
        self.tickets.iter().collect()
    }

    pub fn ticket_of(&self, id: &TicketId) -> Option<&Ticket> {
        self.tickets.iter().find(|t| t.id() == id)
    }

    /// Get schedule of the project.
    pub fn span(&self) -> Option<&ProjectSpan> {
        self.span.as_ref()
    }

    /// Assign new member.
    pub fn assign_project(&mut self, member: &Member) {
        self.members.insert(member.id().clone());
    }

    /// Unassign a member from the project.
    pub fn unassign_project(&mut self, member: &Member) {
        self.members.remove(member.id());
    }

    /// Assign member to ticket.
    pub fn assign_ticket(&mut self, member: &Member, ticket: &Ticket) -> Result<()> {
        
        let mut target = self.tickets.take(ticket)
            .expect("This ticket does not belong to this project.");

        target.assign(member.id().clone());

        self.tickets.insert(target);
        
        Ok(())
    }

    /// Delete ticket.
    pub fn delete_ticket(&mut self, ticket: &Ticket) {
        self.tickets.remove(ticket);
    }

    /// Set schedule of the project.
    pub fn make_schedule(&mut self, schedule: ProjectSpan) {
        self.span = Some(schedule);
    }
}

impl PartialEq for Project {
    fn eq(&self, other: &Self) -> bool {
        &self.id == &other.id
    }
}

/// ID of a project.
#[derive(Debug, Clone, PartialEq, Eq, Hash)]
pub struct ProjectId(String);

impl ProjectId {
    
    /// Create project ID
    pub fn of(value: impl Into<String>) -> Result<Self> {
        let value = value.into().trim().to_string();
        if value.is_empty() {
            return Err(anyhow!("A project ID cannot be empty"));
        }
        Ok(ProjectId(value))
    }

    /// Get string of ID
    pub fn as_str(&self) -> &str {
        &self.0
    }
}

impl Into<ProjectId> for &ProjectId {
    fn into(self) -> ProjectId {
        self.clone()
    }
}

/// Name of a project
#[derive(Debug, Clone, PartialEq, Eq)]
pub struct ProjectName(String);

impl ProjectName {
    
    /// Create project name.
    pub fn of(value: impl Into<String>) -> Result<Self> {
        
        let value = value.into().trim().to_string();

        if value.is_empty() {
            return Err(anyhow!("A project name cannot be empty."));
        }

        Ok(ProjectName(value))
    }

    /// Get string value.
    pub fn as_str(&self) -> &str {
        &self.0
    }
}

#[derive(Debug, Clone, PartialEq, Eq)]
pub struct ProjectSpan {
    start: u64,
    end: u64,
}

impl ProjectSpan {
    
    pub fn of(start: u64, end: u64) -> Result<Self> {
        if start > end {
            return Err(anyhow!("End time must be after the start time."));
        }
        Ok(Self { start, end })
    }

    pub fn start(&self) -> &u64 {
        &self.start
    }

    pub fn end(&self) -> &u64 {
        &self.end
    }

    pub fn span(&self) -> u64 {
        self.end - self.start
    }
}