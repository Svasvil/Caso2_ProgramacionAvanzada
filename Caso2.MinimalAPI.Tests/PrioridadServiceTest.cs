using Caso2.Minimal_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Caso2.Minimal_API.Services;

namespace Caso2.MinimalAPI.Tests;

public class PrioridadServiceTests
{
    private readonly TestService _service;

    public PrioridadServiceTests()
    {
     
        _service = new TestService();
    }

    [Theory]
    [InlineData("El sistema tiene un error crítico", "Alta")]
    [InlineData("Servidor caído", "Alta")]
    [InlineData("La app no funciona", "Alta")]
    [InlineData("Está muy lento", "Media")]
    [InlineData("Conexión intermitente", "Media")]
    [InlineData("Tengo una consulta", "Baja")]
    [InlineData("Tengo una duda", "Baja")]
    [InlineData("Texto normal", "Media")]
    public void ObtenerPrioridad_DebeRetornarValorCorrecto(string descripcion, string esperado)
    {
      
        var resultado = _service.ObtenerPrioridad(descripcion);

      
        Assert.Equal(esperado, resultado);
    }

    [Fact]
    public void ObtenerPrioridad_MultiplesPalabras_DebePriorizarAlta()
    {
        var resultado = _service.ObtenerPrioridad("Hay un error pero va lento");
        Assert.Equal("Alta", resultado);
    }
}