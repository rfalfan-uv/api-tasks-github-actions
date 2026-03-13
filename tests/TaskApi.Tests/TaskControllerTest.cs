using TaskApi.Controllers;
using TaskApi.Models;
using TaskApi.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace TaskApi.Tests.Controllers;
 
public class TaskControllerTest
{
     private readonly TaskController _controller;
    private readonly Mock<ITaskRepository> _mockRepo;

    public TaskControllerTest()
    {
        _mockRepo = new Mock<ITaskRepository>();
        _controller = new TaskController(_mockRepo.Object);
    }

    [Fact]
    public void GetAll_HayTareas_RetornaOkConListaDeTareas()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetAll()).Returns(new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "Tarea 1" },
            new TaskItem { Id = 2, Title = "Tarea 2" }
        });

        // Act
        var result = _controller.GetAll()
        .witch.value.Should().BeAssignableTo<IEnumerable<TaskItem>>()
        .witch.value.Should().HaveCount(2);
    }

    [Fact]
    public void GetById_IdExistente_RetornaOkConTarea()
    {
        _mockRepo.Setup(repo => repo.GetById(1)).Returns(new TaskItem { Id = 1, Title = "Tarea 1" });
        _controller.GetById(1)
        .should().BeOfType<OkObjectResult>()
        .witch.value.Should().BeofType<TaskItem>()
        .witch.value.title.Should().Be("Tarea 1");
    }

    [Fact]
    public void GetById_IdNoExistente_RetornaNotFound()
    {
        _mockRepo.Setup(repo => repo.GetById(100)).Returns((TaskItem)null);
        _controller.GetById(100).Should().BeOfType<NotFoundResult>();
    }

}