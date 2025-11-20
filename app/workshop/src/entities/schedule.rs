use std::cmp::Ordering;

use anyhow::{Ok, Result, anyhow};

use crate::MemberId;

/// Schedule of a member.
#[derive(Debug, Clone)]
pub struct Schedule {
    id: ScheduleId,
    member: MemberId,
    span: ScheduleSpan,
    title: ScheduleTitle,
    description: Option<SchduleDescription>,
}

impl Schedule {

    /// Restore schedule
    pub fn restore(
        id: ScheduleId,
        member: MemberId,
        span: ScheduleSpan,
        title: ScheduleTitle,
        description: Option<SchduleDescription>,
    ) -> Self {
        Self {
            id,
            member,
            span,
            title,
            description,
        }
    }

    /// Get ID of the schedule.
    pub fn id(&self) -> &ScheduleId {
        &self.id
    }

    /// Get ID of the member schedule belongs to.
    pub fn member(&self) -> &MemberId {
        &self.member
    }

    /// Get schedule span
    pub fn span(&self) -> &ScheduleSpan {
        &self.span
    }

    /// Get title of the schdule.
    pub fn title(&self) -> &ScheduleTitle {
        &self.title
    }

    /// Get description of the schedule.
    pub fn description(&self) -> Option<&SchduleDescription> {
        self.description.as_ref()
    }

    /// Change title of the schedule.
    pub fn retitle(&mut self, title: ScheduleTitle) {
        self.title = title;
    }

    /// Change description of the schedule.
    pub fn describe(&mut self, description: SchduleDescription) {
        self.description = Some(description);
    }

    /// Change span of the schedule.
    pub fn reschedule(&mut self, span: ScheduleSpan) {
        self.span = span;
    }
}

#[derive(Debug, Clone, PartialEq, Eq, Hash)]
pub struct ScheduleId(String);

impl ScheduleId {
    
    /// Cretae schedule ID.
    pub fn of(value: impl Into<String>) -> Result<Self> {

        let value = value.into().trim().to_string();
        
        if value.is_empty() {
            return Err(anyhow!("A schedule id cannot be empty."));
        }
        
        Ok(Self(value))
    }

    pub fn as_str(&self) -> &str {
        &self.0
    }
}

/// Title of a schedule.
#[derive(Debug, Clone, PartialEq, Eq)]
pub struct ScheduleTitle(String);

impl ScheduleTitle {
    
    /// Create schedule title
    pub fn of(value: impl Into<String>) -> Result<Self> {
        
        let value = value.into().trim().to_string();

        if value.is_empty() {
            return Err(anyhow!("A schedule title cannot be empty."));
        }

        Ok(ScheduleTitle(value))
    }

    pub fn as_str(&self) -> &str {
        
        &self.0
    }
}

/// Description of schule.
#[derive(Debug, Clone, PartialEq, Eq)]
pub struct SchduleDescription(String);

#[derive(Debug, Clone, PartialEq, Eq)]
pub struct ScheduleSpan {
    start: u64,
    end: u64,
}

impl ScheduleSpan {
    
    pub fn of(start: u64, end: u64) -> Result<Self> {
        if start > end {
            return Err(anyhow!("A start time must be before the end time."));
        }
        Ok(Self { start, end })
    }

    pub fn start(&self) -> &u64 {
        &self.start
    }

    pub fn end(&self) -> &u64 {
        &self.end
    }
}

impl PartialOrd for ScheduleSpan {
    
    fn partial_cmp(&self, other: &Self) -> Option<std::cmp::Ordering> {
        Some(self.start.cmp(&other.start))
    }
}

impl Ord for ScheduleSpan {

    fn cmp(&self, other: &Self) -> std::cmp::Ordering {

        if self.start > other.start {
            return Ordering::Greater;
        }

        if self.start < other.start {
            return Ordering::Less;
        }

        if self.end > other.end {
            return Ordering::Greater;
        }

        if self.end < other.end {
            return Ordering::Less;
        }

        Ordering::Equal
    }
}

#[cfg(test)]
mod tests {
    use crate::ScheduleSpan;

    #[test]
    fn span_can_be_ordered() {
        
        let earlier = ScheduleSpan::of(590_000, 700_000).unwrap();
        let middle = ScheduleSpan::of(600_000, 900_000).unwrap();
        let later = ScheduleSpan::of(600_000, 1_000_000).unwrap();

        let mut  spans = Vec::from([middle, later, earlier]);

        spans.sort();

        assert_eq!(spans[0], ScheduleSpan::of(590_000, 700_000).unwrap());
        assert_eq!(spans[1], ScheduleSpan::of(600_000, 900_000).unwrap());
        assert_eq!(spans[2], ScheduleSpan::of(600_000, 1_000_000).unwrap());
    }
}