use std::collections::{HashMap, HashSet};

use anyhow::{Ok, Result, anyhow};

use crate::{
    Member,
    MemberId,
    ProjectFactory,
    Ticket,
    TicketId,
    TicketTitle,
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
    tickets:   HashMap<TicketId, Ticket>,
    schedule:  Option<ProjectSchedule>,
}

impl Project {

    /// Create new project.
    pub fn factory() -> ProjectFactory {
        ProjectFactory::new()
    }

    /// Restore existing project.
    pub fn restore(
        id:      ProjectId,
        name:    ProjectName,
        members: impl IntoIterator<Item = MemberId>,
        tickets: impl IntoIterator<Item = Ticket>,
        schedule: Option<ProjectSchedule>,
    ) -> Self {
        Self {
            id,
            name,
            members: HashSet::from_iter(members),
            tickets: HashMap::from_iter(tickets.into_iter().map(|t| (t.id().clone(), t))),
            schedule,
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
        self.tickets.iter().map(|t| t.1).collect()
    }

    /// Get schedule of the project.
    pub fn schedule(&self) -> Option<&ProjectSchedule> {
        self.schedule.as_ref()
    }

    /// Assign new member.
    pub fn assign_project(&mut self, member: &Member) {
        self.members.insert(member.id().clone());
    }

    /// Unassign a member from the project.
    pub fn unassign_project(&mut self, member: &Member) {
        self.members.remove(member.id());
    }

    /// Add a new ticket to the project.
    pub fn new_ticket(&mut self, title: TicketTitle, mutation: impl Fn(&mut Ticket) -> ()) -> Result<()>{
        
        let id = TicketId::of((self.tickets.len() + 1) as u64);
        let ticket = Ticket::new(id.clone(), title);
        self.tickets.insert(id.clone(), ticket);
        
        let ticket = self.tickets.get_mut(&id).unwrap();
        mutation(ticket);
        
        if self.schedule.is_none() { return Ok(()); }
        if ticket.schedule().is_none() { return Ok(()); }

        let schedule = self.schedule.as_ref().unwrap();
        let ticket_schedule = ticket.schedule().unwrap();

        if schedule.end() < ticket_schedule.end() {
            return Err(anyhow!("The ticket schedule ends before the project schedule."));
        }

        if schedule.start() > ticket_schedule.start() {
            return Err(anyhow!("The ticket schedule starts after the project schedule."));
        }

        Ok(())
    }

    /// Get a ticket to modify.
    pub fn modify_ticket(&mut self, ticket: &TicketId, mutation: impl Fn(&mut Ticket) -> ()) -> Result<()> {

        let ticket = self.tickets.get_mut(ticket).expect("Ticket was not found in this project.");

        mutation(ticket);

        if self.schedule.is_none() { return Ok(()); }
        if ticket.schedule().is_none() { return Ok(()); }

        let schedule = self.schedule.as_ref().unwrap();
        let ticket_schedule = ticket.schedule().unwrap();

        if schedule.end() < ticket_schedule.end() {
            return Err(anyhow!("The ticket schedule ends before the project schedule."));
        }

        if schedule.start() > ticket_schedule.start() {
            return Err(anyhow!("The ticket schedule starts after the project schedule."));
        }

        Ok(())
    }

    /// Assign member to ticket.
    pub fn assign_ticket(&mut self, member: &Member, ticket: &Ticket) -> Result<()> {
        let target = self.tickets.get_mut(ticket.id())
            .expect("This ticket does not belong to this project.");
        target.assign(member.id().clone());
        Ok(())
    }

    /// Delete ticket.
    pub fn delete_ticket(&mut self, ticket: &Ticket) {
        self.tickets.remove(ticket.id());
    }

    /// Set schedule of the project.
    pub fn make_schedule(&mut self, schedule: ProjectSchedule) {
        self.schedule = Some(schedule);
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
pub struct ProjectSchedule {
    start: u64,
    end: u64,
}

impl ProjectSchedule {
    
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