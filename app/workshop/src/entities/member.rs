use std::hash::Hash;

use anyhow::{Result, anyhow};

/// A member of the application.
#[derive(Debug, Clone, Eq)]
pub struct Member {
    id: MemberId,
}

impl Member {

    /// Restore existing member.
    pub fn restore(
        id: MemberId,
    ) -> Self {
        Self { 
            id,
        }
    }

    /// Get ID of the member.
    pub fn id(&self) -> &MemberId {
        &self.id
    }
}

impl PartialEq for Member {
    
    fn eq(&self, other: &Self) -> bool {
        &self.id == &other.id
    }
}

impl Hash for Member {
    
    fn hash<H: std::hash::Hasher>(&self, state: &mut H) {
        self.id.hash(state);
    }
}

/// ID of a member.
#[derive(Debug, Clone, PartialEq, Eq, Hash)]
pub struct MemberId(String);

impl MemberId {
    
    /// Create member ID.
    pub fn of(value: impl Into<String>) -> Result<Self> {
        
        let value = value.into().trim().to_string();

        if value.is_empty() {
            return Err(anyhow!("A member ID cannot be empty."));
        }

        return Ok(MemberId(value));
    }

    /// Get string value.
    pub fn as_str(&self) -> &str {
        &self.0
    }
}