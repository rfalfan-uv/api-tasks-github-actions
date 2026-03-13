using TaskApi.Repositories;
using TaskApi.Models;
using FluentAssertions;
using System.Runtime.CompilerServices;
namespace TaskApi.Tests.Repositories;
public class InMemoryTaskRepositoryTests {
    private readonly InMemoryTaskRepository _repo;
    public InMemoryTaskRepositoryTests(){
        _repo = new();
    }
    
    [Fact]
    public void Add_TareaValida_AsignaIdYRetornaTarea(){
        //Arrange        
        var tarea = new TaskItem {
            Title = "Comprar Guitarra",
            Description= "Comprar Guitarra para ser Feliz"
        };
        //Act
        var resultado = _repo.Add(tarea);
        //Arrange
        resultado.Id.Should().BeGreaterThan(0);
        resultado.Title.Should().Be("Comprar Guitarra");
    }

    public void Update_IdNoExistente_RetornaNull(){
        //Arrange
       
        //Act
        var resultado = _repo.Update(1000, new TaskItem {
            Title = "Comprar Guitarra",
 
        });
        //Assert
        resultado.Should().BeNull();
    }

    public AsyncVoidMethodBuilder Delete_TareaExistente_RetornaTrue(){
        //Arrange
       var tareaAgregada = _repo.Add(new TaskItem {
            Title = "Tarea a elominar",
   
        });
 
        //Assert
     _repo.Delete(tareaAgregada.Id).Should().BeTrue();
     return AsyncVoidMethodBuilder.Create();
    }
}