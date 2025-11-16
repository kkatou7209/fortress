use core::str;
use std::hash::Hash;

use anyhow::{Ok, Result, anyhow};

use crate::MemberId;

/// Project ticket which represents a task in project.
/// 
/// A ticket ID is unique in a project.
#[derive(Debug, Clone, Eq)]
pub struct Ticket {
    id:       TicketId,
    title:    TicketTitle,
    priority: Option<TicketPriority>,
    state:    Option<TicketState>,
    deadline: Option<TicketDeadline>,
    comments: Vec<TicketComment>,
    assignee: Option<MemberId>,
    schedule: Option<TicketSchedule>,
}

impl Ticket {

    pub fn new(id: TicketId, title: TicketTitle) -> Self {
        Self {
            id,
            title,
            priority: None,
            state: None,
            deadline: None,
            comments: Vec::new(),
            assignee: None,
            schedule: None,
        }
    }

    /// Restore existing ticket.
    pub fn restore(
        id:       TicketId,
        title:    TicketTitle,
        priority: Option<TicketPriority>,
        state:    Option<TicketState>,
        deadline: Option<TicketDeadline>,
        comments: impl IntoIterator<Item = TicketComment>,
        assignee: Option<MemberId>,
        schedule: Option<TicketSchedule>,
    ) -> Self {
        Self {
            id,
            title,
            priority,
            state,
            deadline,
            comments: Vec::from_iter(comments),
            assignee,
            schedule,
        }
    }
    
    /// Get ID of the ticket.
    pub fn id(&self) -> &TicketId {
        &self.id
    }

    /// Get title of the ticket.
    pub fn title(&self) -> &TicketTitle {
        &self.title
    }

    /// Get deadline of the ticket.
    pub fn deadline(&self) -> Option<&TicketDeadline> {
        self.deadline.as_ref()
    }

    /// Get comments of the ticket.
    pub fn comments(&self) -> Vec<&TicketComment> {
        self.comments.iter().collect()
    }

    /// Get assignee of the ticket.
    pub fn assignee(&self) -> Option<&MemberId> {
        self.assignee.as_ref()
    }

    /// Get schedule of the ticket.
    pub fn schedule(&self) -> Option<&TicketSchedule> {
        self.schedule.as_ref()
    }

    /// Retitle ticket.
    pub fn retitle(&mut self, title: TicketTitle) {
        self.title = title;
    }

    /// Assign member to ticket.
    pub fn assign(&mut self, member: MemberId) {
        self.assignee = Some(member);
    }

    /// Prioritize ticket.
    pub fn prioritize(&mut self, priority: TicketPriority) {
        self.priority = Some(priority);
    }

    /// Change state of ticket.
    pub fn change_state(&mut self, state: TicketState) {
        self.state = Some(state);
    }
}

impl PartialEq for Ticket {
    
    fn eq(&self, other: &Self) -> bool {
        self.id == other.id
    }
}

impl Hash for Ticket {
    
    fn hash<H: std::hash::Hasher>(&self, state: &mut H) {
        self.id.hash(state);
    }
}

/// ID of a ticket
#[derive(Debug, Clone, PartialEq, Eq, Hash)]
pub struct TicketId(u64);

impl TicketId {
    
    /// Create ticket ID.
    pub fn of(value: u64) -> Self {
        Self(value)
    }

    /// Get string value
    pub fn as_str(&self) -> &u64 {
        &self.0
    }
}

/// Title of a ticket.
#[derive(Debug, Clone, PartialEq, Eq)]
pub struct TicketTitle(String);

impl TicketTitle {
    
    /// Create title of ticket.
    pub fn of(value: impl Into<String>) -> Result<Self> {
        
        let value = value.into().trim().to_string();

        if value.is_empty() {
            return Err(anyhow!("A title of ticket cannot be empty."));
        }

        Ok(TicketTitle(value))
    }

    /// Get string value.
    pub fn as_str(&self) -> &str {
        &self.0
    }
}

/// Priority of tickets.
/// 
/// A priority becomes heigher in proportion to its order.
#[derive(Debug, Clone, PartialEq, Eq)]
pub struct TicketPriority {
    title: String,
    order: u32,
}

impl TicketPriority {
    
    /// Define new priority category.
    pub fn of(title: impl Into<String>, order: impl Into<u32>) -> Self {
        Self {
            title: title.into(),
            order: order.into()
        }
    }

    /// Get title of the priority.
    pub fn title(&self) -> &str {
        &self.title
    }

    /// Priority order.
    pub fn order(&self) -> &u32 {
        &self.order
    }
}

/// State of tickets.
#[derive(Debug, Clone, PartialEq, Eq)]
pub struct TicketState(String);

impl TicketState {
    
    pub fn of(value: impl Into<String>) -> Result<Self> {
        let value = value.into().trim().to_string();
        if value.is_empty() {
            return Err(anyhow!("A title of ticket state connot be empty."));
        }
        Ok(Self(value))
    }

    pub fn as_str(&self) -> &str {
        &self.0
    }
}

/// Ticket deadline that holds timestamp of the limit date.
#[derive(Debug, Clone, PartialEq, Eq, Hash)]
pub struct TicketDeadline(u64);

impl TicketDeadline {
    
    pub fn of(value: u64) -> Self {
        Self(value)
    }

    pub fn as_u64(&self) -> &u64 {
        &self.0
    }
}

/// Comment of a ticket.
#[derive(Debug, Clone, PartialEq, Eq)]
pub struct TicketComment {
    commenter: MemberId,
    content: String,
    commented_at: u64,
}

impl TicketComment {
    
    pub fn of(
        commenter: MemberId,
        content: impl Into<String>,
        commented_at: u64
    ) -> Result<Self> {
        
        let content = content.into().trim().to_string();

        if content.is_empty() {
            return Err(anyhow!("A comment cannnot be empty."));
        }
        
        let comment = Self {
            commenter,
            content,
            commented_at,
        };

        Ok(comment)
    }

    pub fn commenter(&self) -> &MemberId {
        &self.commenter
    }

    pub fn content(&self) -> &str {
        &self.content
    }

    pub fn commented_at(&self) -> &u64 {
        &self.commented_at
    }
}

/// Schedule of ticket.
#[derive(Debug, Clone, PartialEq, Eq)]
pub struct TicketSchedule {
    start: u64,
    end: u64,
}

impl TicketSchedule {
    
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