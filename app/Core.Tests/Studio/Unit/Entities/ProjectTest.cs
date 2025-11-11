using System;
using Core.Domains.Studio.Entities;
using Core.Domains.Studio.Shared;
using NFluent;

namespace Core.Tests.Studio.Unit.Entities;

public class ProjectTest
{
    [Fact(DisplayName = "Project should rename")]
    public void Should_Rename()
    {
        // Given
        Project project = new(ProjectId.Of("1"), ProjectName.Of("test"));

        // When
        project.Rename("test2");

        // Then
        Check.That(project.Name).IsEqualTo(ProjectName.Of("test2"));
    }

    [Fact(DisplayName = "Project should assign members")]
    public void Should_Assign_Members()
    {
        // Given
        Project project = new(ProjectId.Of("1"), ProjectName.Of("test"));

        // When
        project.Assign(MemberId.Of("1"), MemberId.Of("2"));

        // Then
        Check.That(project.Members).Contains(MemberId.Of("1"), MemberId.Of("2"));
    }

    [Fact(DisplayName = "Project should unassign members")]
    public void Should_Unassign_Members()
    {
        // Given
        Project project = new(ProjectId.Of("1"), ProjectName.Of("test"), [MemberId.Of("1"), MemberId.Of("2"), MemberId.Of("3")]);

        // When
        project.Unassign(MemberId.Of("2"));

        // Then
        Check.That(project.Members).Contains(MemberId.Of("1"), MemberId.Of("3"))
            .And.Not.Contains(MemberId.Of("2"));
    }

    [Fact(DisplayName = "IsAssign should return true if the member is assigned")]
    public void IsAssigned_Should_Return_True_If_Member_Assigned()
    {
        // Given
        Project project = new(ProjectId.Of("1"), ProjectName.Of("test"));

        // When
        project.Assign(MemberId.Of("2"));
        project.Assign(MemberId.Of("4"));

        // Then
        Check.That(project.IsAssigned(MemberId.Of("2"))).IsTrue();
        Check.That(project.IsAssigned(MemberId.Of("3"))).IsFalse();
    }
}
