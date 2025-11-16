use anyhow::{Ok, Result, anyhow};

#[derive(Debug, Clone)]
pub struct User {
    id: UserId,
    name: UserName,
}

impl User {
    
    pub fn restore(
        id: UserId,
        name: UserName,
    ) -> Self {
        Self {
            id,
            name,
        }
    }

    pub fn id(&self) -> &UserId {
        &self.id
    }

    pub fn name(&self) -> &UserName {
        &self.name
    }

    pub fn rename(&mut self, name: UserName) {
        self.name = name;
    }
}

impl PartialEq for User {
    
    fn eq(&self, other: &Self) -> bool {
        self.id == other.id
    }
}

#[derive(Debug, Clone, PartialEq, Eq, Hash)]
pub struct UserId(String);

impl UserId {
    
    pub fn of(value: impl Into<String>) -> Result<UserId> {
        
        let value = value.into().trim().to_string();

        if value.is_empty() {
            return Err(anyhow!("A user ID cannot be empty."));
        }

        Ok(UserId(value))
    }

    pub fn as_str(&self) -> &str {
        &self.0
    }
}

#[derive(Debug, Clone, PartialEq, Eq)]
pub struct UserName(String);

impl UserName {
    
    pub fn of(value: impl Into<String>) -> Result<UserName> {
        
        let value = value.into().trim().to_string();

        if value.is_empty() {
            return Err(anyhow!("A user ID cannot be empty."));
        }

        Ok(UserName(value))
    }

    pub fn as_str(&self) -> &str {
        &self.0
    }
}
