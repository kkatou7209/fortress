/// This file defines use cases of Workshop.

use std::error::Error;

pub struct Span {
    pub start: u64,
    pub end: u64,
}

pub struct ProjectDTO {
    pub id: String,
    pub name: String,
    pub member_ids: Vec<String>,
    pub ticket_ids: Vec<String>,
    pub span: Option<Span>,
}

pub struct GetProjectByIdQuery {
    pub actor_id: String,
    pub project_id: String,
}

pub struct ListProjectOfMemberQuery {
    pub actor_id: String,
    pub member_id: String,
}

pub trait GetProjectUsecase {
    
    fn get_by_id(query: GetProjectByIdQuery) -> Result<Option<ProjectDTO>, impl Error>;

    fn list_of_member(query: ListProjectOfMemberQuery) -> Result<Vec<ProjectDTO>, impl Error>;
}

/// Command for creating project.
#[derive(Debug, Clone)]
pub struct CreateProjectCommand {
    /// Project name.
    pub name: String,
    /// IDs of initial project members.
    pub member_ids: Vec<String>,
    /// user of who creates project.
    pub creater_id: String,
}

/// Use case for creating project.
pub trait CreateProjectUsecase {
    /// Create a new project
    fn create(command: CreateProjectCommand) -> Result<(), impl Error>;
}

/// Command for assigning member to project.
#[derive(Debug, Clone)]
pub struct AssignMemberCommmand {
    /// User ID of who will be asssigned to project.
    pub member_id: String,
    /// Prject ID which assign member to.
    pub project_id: String,
    /// User ID who assigns member to project.
    pub actor_id: String,
}

/// Use case for assigning member to prject.
pub trait AssifnMemberUsecase {
    /// Assign a member to project.
    fn assign(command: AssignMemberCommmand) -> Result<(), impl Error>;
}

/// Command for unassigning member from project.
#[derive(Debug, Clone)]
pub struct UnassignMemberCommand {
    /// ID of who will be unassigned.
    pub member_id: String,
    /// ID of traget project
    pub project_id: String,
    /// ID of who will unassign member.
    pub actor_id: String,
}

/// Usecase for 
pub trait UnassignMmeberUsecase {
    fn unassign(command: UnassignMemberCommand) -> Result<(), impl Error>;
}

/// Command for opening ticket.
#[derive(Debug, Clone)]
pub struct OpenTicketCommand {
    /// User ID who opens ticket.
    pub opener_id: String,
    /// Project ID opening ticket belongs to.
    pub project_id: String,
    /// Title of ticket.
    pub title: String,
    /// Ticket priority.
    pub priority: Option<i32>,
    /// Ticket deadline.
    pub deadline: Option<u64>,
    /// Ticket state.
    pub state: String,
    /// Assigning member ID.
    pub asignee_id: Option<String>,
}

/// Use case for opening new ticket.
pub trait OpenTicketUsecase {
    /// Open a new ticket.
    fn open(command: OpenTicketCommand);
}